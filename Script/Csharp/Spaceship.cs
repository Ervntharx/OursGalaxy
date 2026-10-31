using System;
using Godot;
using OursGalaxy.UI;
namespace OursGalaxy;

/// <summary>
/// 运行时飞船类
/// </summary>
internal partial class Spaceship : Node3D, ISpaceEntity, IBattleEntity {
	public enum ScaleSizeEnum { FF, DD, CL, CA, BB }

	public T ToNode<T>() where T : Node => (this as T)!;
	public Node3D ToNode3D() => this;

	[Export]
	protected CombatBehaviorStateMachine CombatStateMachine { get; set; } = null!;

	[Export]
	public ScaleSizeEnum ScaleSize { get; set; } = ScaleSizeEnum.FF;

	[ExportGroup("Outlets Setup")]
	[Export]
	public Godot.Collections.Dictionary<OutletSizeType, NodePath> Outlets { get; set; } = [];


	public GUID ID { get; init; }

	public string TypeName { get; set; }
	public string? CustomName { get; set; }

	#region Battle

	public CombatStage.CampEnum Camp { get; set; } = CombatStage.CampEnum.None;

	public int Health { get; set; }

	public int CurrentHealth { get; set; }

	public int Range { get; set; }

	/// <summary>
	/// 回避性能
	/// </summary>
	public int PreventValue { get; set; }

	/// <summary>
	/// 冲击伤害耐性
	/// </summary>
	public int ImpactTolerance { get; set; }

	/// <summary>
	/// 穿透伤害耐性
	/// </summary>
	public int PenetrateTolerance { get; set; }

	/// <summary>
	/// 光热伤害耐性
	/// </summary>
	public int PhotothermalTolerance { get; set; }

	/// <summary>
	/// 酸蚀伤害耐性
	/// </summary>
	public int CorrosionTolerance { get; set; }

	/// <summary>
	/// 电磁伤害耐性
	/// </summary>
	public int ElectromagneticTolerance { get; set; }

	/// <summary>
	/// 死亡事件
	/// </summary>
	public event Action<SpaceshipSnapshot>? OnDeath;

	/// <summary>
	/// 被攻击
	/// </summary>
	/// <param name="target">目标实体</param>
	public void Attacked(DamageSet damageSet) {
		var damage = new DamageSet() {
			Impact = Math.Max(damageSet.Impact - ImpactTolerance, 0),
			Penetrate = Math.Max(damageSet.Penetrate - PenetrateTolerance, 0),
			Photothermal = Math.Max(damageSet.Photothermal - PhotothermalTolerance, 0),
			Corrosion = Math.Max(damageSet.Corrosion - CorrosionTolerance, 0),
			Electromagnetic = Math.Max(damageSet.Electromagnetic - ElectromagneticTolerance, 0)
		};
		CurrentHealth -= damage.Sum();
		if (CurrentHealth <= 0) {
			OnDeath?.Invoke(new(this));
			QueueFree();
		}
	}

	#endregion Battle

	public Spaceship() {
		ID = GUID.Empty;
		TypeName = string.Empty;
		CurrentHealth = 0;
	}

	public Spaceship(SpaceshipData data) {
		ID = data.ID;
		TypeName = data.GetShipTypeName();
		CustomName = data.CustomName;
		CurrentHealth = data.CurrentHP;
		Range = data.Range;
	}

	public SpaceshipData Save() {
		return new SpaceshipData(TypeName, ID);
	}

	#region Movement

	public int RotateCapability { get; set; } = 15;
	public int PowerCapability { get; set; } = 5;
	public int MicroPowrCapability { get; set; } = 1;

	/// <summary>
	/// 船头椎体角度
	/// </summary>
	public float BowAngle { get; set; } = 15;

	public AccelerationMap AccelMap { get; set; } = new();
	public AccelerationMap BrakeMap { get; set; } = new();
	public int RotateAngleVelocity { get; set; } = 15;

	public Vector3 Velocity { get; set; } = new();

	#endregion Movement

	public bool Active { get => CombatStateMachine.Paused; set => CombatStateMachine.Paused = value; }

	public override void _Ready() {
		base._Ready();
		// TODO:	RotateCapability, PowerCapability换算为物理值
		AccelMap.MaxVelocity = PowerCapability;
		RotateAngleVelocity = RotateCapability;
		// 依靠Timer驱动行为状态机的Ship初始置paused
		CombatStateMachine.Paused = true;
		CombatStateMachine.Start();
	}


	public override void _Process(double delta) {
		// 计算目标角速度
		if (GodotObject.IsInstanceValid(CombatStateMachine.Target?.ToNode3D())) {
			if (targetPrePos is not null) {
				targetAngleVelocityCache = targetPrePos.Value.DistanceTo(CombatStateMachine.Target.ToNode3D().Position);
			}
			targetPrePos = CombatStateMachine.Target.ToNode3D().Position;
		}
		else {
			targetAngleVelocityCache = null;
			targetPrePos = null;
		}

		// 计算目标是否在射程内
		if (GodotObject.IsInstanceValid(CombatStateMachine.Target?.ToNode3D())) {
			var distance = (CombatStateMachine.Target.ToNode3D().Transform.Origin - Transform.Origin).Length();
			isInRange = distance <= Range;
		}

		// 如果目标角速度值小于自身能达到的旋转角速度,且目标在射程外时，期望接近目标
		intendApporach = GodotObject.IsInstanceValid(CombatStateMachine.Target?.ToNode3D()) && targetAngleVelocityCache is not null
			? targetAngleVelocityCache <= RotateAngleVelocity && !isInRange
			: false;

		// 目标Basis传递到炮塔
		if (GodotObject.IsInstanceValid(CombatStateMachine.Target?.ToNode3D())) {
			var targetBasis = CombatStateMachine.Target.ToNode3D().Basis * Basis.Inverse();
			foreach (var outlet in Outlets) {
				var equip = GetNodeOrNull<Node?>(outlet.Value);
				if (equip is not null && equip is ITurret turret) {
					// turret.TargetBasis = targetBasis;
				}
			}
		}

		base._Process(delta);
	}

	public override void _PhysicsProcess(double delta) {
		// 方向控制
		if (GodotObject.IsInstanceValid(CombatStateMachine.Target?.ToNode3D())) {
			// 四元数匀速差值转向目标
			var currentQ = new Quaternion(Basis);
			var targetQ = new Quaternion((CombatStateMachine.Target.ToNode3D().Transform * Transform.Inverse()).Basis);
			var angleDiff = currentQ.AngleTo(targetQ);
			var maxAngle = RotateAngleVelocity * (float)delta;
			if (angleDiff > maxAngle) {
				Basis = new(currentQ.Slerp(targetQ, (float)(maxAngle / angleDiff)));
			}
			else {
				Basis = new(targetQ);
			}
		}
		// 速度控制
		if (intendApporach) {
			var v = Velocity;
			var linearV = v.Length();
			linearV += AccelMap.GetAcceleration(linearV, (float)delta);
			v = v.Normalized() * linearV;
			Velocity = v;
		}
		else {
			var v = Velocity;
			var linearV = v.Length();
			linearV -= BrakeMap.GetAcceleration(linearV, (float)delta);
			v = v.Normalized() * linearV;
			Velocity = v;
		}

		// 移动
		var pos = Position;
		pos += Velocity * (float)delta;
		Position = pos;
		base._PhysicsProcess(delta);
	}

	private double? targetAngleVelocityCache;
	private Vector3? targetPrePos;

	private bool isInRange;
	private bool intendApporach;
}
