using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using OursGalaxy.UI;
namespace OursGalaxy;

/// <summary>
/// 运行时飞船类
/// </summary>
internal partial class Spaceship : CharacterBody3D, IBattleEntity {
	public Node3D AsNode3D() => this;
	public T ToNode<T>() where T : Node => (this as T)!;

	public enum ScaleSizeEnum { FF = 0, DD = 1, CL = 2, CA = 3, BB = 4 }

	[Export]
	public ScaleSizeEnum ScaleSize { get; set; } = ScaleSizeEnum.FF;

	/// <summary>
	/// 活动总开关
	/// </summary>
	public bool Active {
		get => active;
		set {
			active = value;
			if (Active) {
				ProbeTarget();
			}
		}
	}
	private bool active;

	[ExportGroup("Outlets Setup")]
	[Export]
	public Godot.Collections.Dictionary<OutletSizeType, NodePath> Outlets { get; set; } = [];


	public GUID ID { get; private set; }

	public string TypeName { get; set; }
	public string? CustomName { get; set; }

	protected Spaceship() {
		ID = GUID.Empty;
		TypeName = string.Empty;
		CurrentHealth = 0;
	}

	public static Spaceship Load(SpaceshipData data, TacticsSet? tactics) {
		var ship = (Spaceship)ResourceLoader.Load<PackedScene>($"res://Script/Csharp/Ships/{data.GetShipTypeName()}.tscn").Instantiate();
		ship.ID = data.ID;
		ship.TypeName = data.GetShipTypeName();
		ship.CustomName = data.CustomName;
		ship.CurrentHealth = data.CurrentHP;
		ship.Range = data.Range;
		ship.Tactics = tactics ?? new();
		ship.AttackableArea.AreaEntered += ship.OnAttakableAreaAreaEntered;
		return ship;
	}

	public SpaceshipData Save() {
		return new SpaceshipData(TypeName, ID);
	}

	#region Battle
	[ExportGroup("BattleEntity")]
	[Export]
	public Area3D AttackableArea { get; set; } = null!;

	public CombatStage.CampEnum Camp { get; set; } = CombatStage.CampEnum.None;

	public bool IsFlagShip { get; set; }

	public IBattleEntity? TargetEntity { get; set; }

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

	private void OnAttakableAreaAreaEntered(Area3D area) {
		if (area is BulletBase bullet) {
			var originalDamage = bullet.Damage;
			var damage = new DamageSet() {
				Impact = Math.Max(originalDamage.Impact - ImpactTolerance, 0),
				Penetrate = Math.Max(originalDamage.Penetrate - PenetrateTolerance, 0),
				Photothermal = Math.Max(originalDamage.Photothermal - PhotothermalTolerance, 0),
				Corrosion = Math.Max(originalDamage.Corrosion - CorrosionTolerance, 0),
				Electromagnetic = Math.Max(originalDamage.Electromagnetic - ElectromagneticTolerance, 0)
			};
			CurrentHealth -= damage.Sum();
			if (CurrentHealth <= 0) {
				OnDeath?.Invoke(new(this));
				QueueFree();
			}
		}
	}

	/// <summary>
	/// 被攻击
	/// </summary>
	/// <param name="target">目标实体</param>
	[Obsolete]
	public void Attacked(DamageSet damageSet) {

	}

	#endregion Battle

	#region Tactics
	public TacticsSet Tactics { get; set; } = new();

	[ExportGroup("Tactics")]
	[Export]
	protected Timer TacticsTargetProberTimer { get; set; } = null!;

	private void ProbeTarget() {
		var targetsLs = CombatStage.ScanShips(Camp);
		GD.Print($"try spaceship probe target: {targetsLs.Count}");
		switch (Tactics.Targeting) {
			case TacticsSet.TargetingEnum.Closest:
				targetsLs.Sort((left, right) => Mathf.RoundToInt(left.Transform.Origin.DistanceTo(GlobalTransform.Origin) - right.Transform.Origin.DistanceTo(GlobalTransform.Origin)));
				break;
			case TacticsSet.TargetingEnum.Biggest:
				targetsLs.Sort((left, right) => (int)left.ScaleSize - (int)right.ScaleSize);
				break;
			case TacticsSet.TargetingEnum.Smallest:
				targetsLs.Sort((left, right) => (int)right.ScaleSize - (int)left.ScaleSize);
				break;
			case TacticsSet.TargetingEnum.MiddleThenLargest:
				var targetSplitLs_MiddleScaleSize = targetsLs.Where(shipshot => shipshot.ScaleSize == ScaleSizeEnum.CL);
				if (targetSplitLs_MiddleScaleSize.Count() > 0) {
					targetsLs = [.. targetSplitLs_MiddleScaleSize];
				}
				else {
					targetsLs.Sort((left, right) => (int)left.ScaleSize - (int)right.ScaleSize);
				}
				break;
			case TacticsSet.TargetingEnum.MiddleThenSmallest:
				var targetSplitLs_MiddleScaleSize_2 = targetsLs.Where(shipshot => shipshot.ScaleSize == ScaleSizeEnum.CL);
				if (targetSplitLs_MiddleScaleSize_2.Count() > 0) {
					targetsLs = [.. targetSplitLs_MiddleScaleSize_2];
				}
				else {
					targetsLs.Sort((left, right) => (int)right.ScaleSize - (int)left.ScaleSize);
				}
				break;
			case TacticsSet.TargetingEnum.KillFlagship:
				var target = targetsLs.Find(shipshot => shipshot.IsFlagShip);
				if (target is not null) {
					targetsLs = [target];
				}
				else {
					targetsLs = [];
				}
				break;
			default:
				break;
		}
		if (targetsLs.Count > 0) {
			TargetEntity = CombatStage.GetShip(targetsLs[0].UniqueID);
		}
	}
	#endregion Tactics

	#region Movement
	// 移动能力原本参数
	[Export]
	public int RotateCapability { get; set; } = 15;
	[Export]
	public int PowerCapability { get; set; } = 5;
	[Export]
	public int BackPowerCapability { get; set; } = 2;
	[Export]
	public int MicroPowrCapability { get; set; } = 1;

	public float RotateAngleVelocity => (float)RotateCapability / 10f;
	public float PowerVelocity => (float)PowerCapability / 10f;
	public float BackPowerVelocity => (float)BackPowerCapability / 10f;
	public float MicroPowerVelocity => (float)MicroPowrCapability / 10f;

	/// <summary>
	/// 船头椎体角度
	/// </summary>
	public float ShipHeadConeAngle { get; set; } = 25;

	// 保持的距离
	// TODO:	使用射程动态计算
	public float KeepDistanceMax { get; set; } = 15;
	public float KeepDistanceMin { get; set; } = 5;

	public AccelerationMap AccelMap { get; set; } = new();
	public AccelerationMap BrakeMap { get; set; } = new();

	public enum MovePowEnum { Stay, MicroMove, Forward, Backward }
	public MovePowEnum MovePow { get; set; } = MovePowEnum.Stay;

	public enum RotateModeEnum { Stay, Rest, RotateToTarget }
	public RotateModeEnum RotateMode { get; set; } = RotateModeEnum.Stay;

	private void HandleShipMvementState(double delta, Spaceship ship, IBattleEntity? target) {
		switch (MovePow) {
			case MovePowEnum.Forward:
				ship.Velocity = ship.Velocity.Lerp(ship.GlobalBasis.Z * ship.PowerVelocity, Mathf.Clamp(0.5f, 0f, 1f));
				break;
			case MovePowEnum.Backward:
				ship.Velocity = ship.Velocity.Lerp(ship.GlobalBasis.Z * ship.BackPowerVelocity, Mathf.Clamp(0.5f, 0f, 1f));
				break;
			case MovePowEnum.Stay:
				ship.Velocity = ship.Velocity.Lerp(new Vector3(), Mathf.Clamp(0.5f, 0f, 1f));
				break;
			default:
				break;
		}

		switch (RotateMode) {
			case RotateModeEnum.RotateToTarget:
				if (GodotObject.IsInstanceValid(target?.AsNode3D())) {
					var targetOffPos = target.AsNode3D().GlobalTransform.Origin - ship.GlobalTransform.Origin;
					var targetQ = new Quaternion(Vector3.Forward, targetOffPos.Normalized());
					var currentQ = ship.GlobalBasis.Orthonormalized().GetRotationQuaternion().Normalized();

					if (currentQ.AngleTo(targetQ) >= float.DegreesToRadians(0.5f)) {
						ship.GlobalBasis = new(currentQ.Slerp(targetQ, Math.Clamp(ship.RotateAngleVelocity * (float)delta / currentQ.AngleTo(targetQ), 0f, 1f)));
					}
					else {
						ship.GlobalBasis = new(currentQ);
					}
				}
				break;
			case RotateModeEnum.Rest:
				var currentDir = ship.GlobalBasis.Z;
				currentDir.Y = 0;
				if (!currentDir.IsZeroApprox()) {
					ship.GlobalBasis = ship.GlobalBasis.Slerp(Basis.LookingAt(currentDir, Vector3.Up), Mathf.Clamp(ship.RotateAngleVelocity * (float)delta / currentDir.AngleTo(ship.GlobalBasis.Z), 0f, 1f));
				}
				else {
					ship.GlobalBasis = Basis.LookingAt(currentDir, Vector3.Up);
				}
				break;
			case RotateModeEnum.Stay:
			default:
				break;
		}
	}

	private void AutoSetShipMovementState(Transform3D selfTrans, Transform3D? targetTrans, float coneAngle, float keepDistanceMin, float keepDistanceMax) {
		if (targetTrans is not null) {
			var targetOffPos = targetTrans.Value.Origin - selfTrans.Origin; // 目标相对位置
			if (!targetOffPos.IsZeroApprox()) {
				if (IsInClone(selfTrans, targetTrans.Value, coneAngle)) {
					// 在船头椎内就停止旋转
					RotateMode = RotateModeEnum.Stay;
					if (selfTrans.Origin.DistanceTo(targetTrans.Value.Origin) > keepDistanceMax) {
						MovePow = MovePowEnum.Forward;  // 在最大距离之外选择靠近
					}
					else if (selfTrans.Origin.DistanceTo(targetTrans.Value.Origin) > keepDistanceMin) {
						MovePow = MovePowEnum.Stay; // 在最大距离和最小距离之间停机
					}
					else {
						MovePow = MovePowEnum.Backward; // 在最小距离之内后退
					}
				}
				else {
					// 不在船头椎内，停止推进，先旋转
					MovePow = MovePowEnum.Stay;
					RotateMode = RotateModeEnum.RotateToTarget;
				}
			}
			else {
				// 目标重叠，全部停止
				MovePow = MovePowEnum.Stay;
				RotateMode = RotateModeEnum.Stay;
			}
		}

		// 判定是否有在船头椎内
		bool IsInClone(Transform3D self, Transform3D target, float coneAngle) => (self.AffineInverse() * target.Origin).AngleTo(Vector3.Forward) <= float.DegreesToRadians(coneAngle);
	}

	#endregion Movement

	#region Turret

	private readonly List<TurretBase> Turrets = [];


	// 探测船上可用的Turret
	private void ProbeTurret() {
		foreach (var turret in Outlets.Select(outlet => GetNodeOrNull<Node?>(outlet.Value)?.GetChildOrNull<Node?>(0)).OfType<TurretBase>()) {
			Turrets.Add(turret);
		}
	}

	// 代理炮台旋转算法
	private static void HandleTurretRotation(double delta, TurretBase turretBase, IBattleEntity? targetEntity) {
		if (turretBase.IsRotatble) {
			var currentLocalBasis = turretBase.RotatableNode!.Basis;
			if (GodotObject.IsInstanceValid(targetEntity?.AsNode3D())) {
				var localDir = turretBase.GlobalTransform.Inverse() * targetEntity.AsNode3D().GlobalPosition;
				localDir.Y = 0;
				var targetLocalBasis = Basis.LookingAt(localDir.Normalized(), Vector3.Up);
				var currentLocalQ = new Quaternion(currentLocalBasis);
				var targetLocalQ = new Quaternion(targetLocalBasis);

				targetLocalQ = ClampAngle(targetLocalQ, turretBase.LeftClampAngle, turretBase.RightClampAngle);

				if (currentLocalQ.AngleTo(targetLocalQ) > float.DegreesToRadians(turretBase.RotateAngleVelocity) * (float)delta) {
					currentLocalQ = currentLocalQ.Slerp(targetLocalQ, float.DegreesToRadians(turretBase.RotateAngleVelocity) * (float)delta / currentLocalQ.AngleTo(targetLocalQ));
				}
				else {
					currentLocalQ = targetLocalQ;
				}
				turretBase.RotatableNode.Basis = new Basis(currentLocalQ);
			}
			else {
				var currentLocalQ = new Quaternion(currentLocalBasis);
				var targetLocalQ = new Quaternion(Basis.Identity);
				if (currentLocalQ.AngleTo(targetLocalQ) > float.DegreesToRadians(turretBase.RotateAngleVelocity) * (float)delta) {
					currentLocalQ = currentLocalQ.Slerp(targetLocalQ, float.DegreesToRadians(turretBase.RotateAngleVelocity) * (float)delta / currentLocalQ.AngleTo(targetLocalQ));
				}
				else {
					currentLocalQ = targetLocalQ;
				}
				turretBase.RotatableNode.Basis = new Basis(currentLocalQ);
			}
		}

		Quaternion ClampAngle(Quaternion dir, float leftClampAngle, float rightClampAngle) {
			var euler = dir.GetEuler(EulerOrder.Yxz);
			euler.Y = Mathf.Clamp(euler.Y, float.DegreesToRadians(-leftClampAngle), float.DegreesToRadians(rightClampAngle));
			return Quaternion.FromEuler(euler);
		}
	}

	#endregion Turret

	public override void _Ready() {
		base._Ready();
		Name = $"{TypeName}_{CustomName}";
		GD.Print($"Set name as {$"{TypeName}_{CustomName}"}, but {Name} instead?");
		TacticsTargetProberTimer.OneShot = false;
		TacticsTargetProberTimer.Start();
		TacticsTargetProberTimer.Timeout += ProbeTarget;
		// TODO:	RotateCapability, PowerCapability换算为物理值
		AccelMap.MaxVelocity = PowerCapability;
	}

	public override void _Process(double delta) {
		base._Process(delta);
		AutoSetShipMovementState(GlobalTransform, TargetEntity?.AsNode3D().GlobalTransform, ShipHeadConeAngle, KeepDistanceMin, KeepDistanceMax);
	}

	public override void _PhysicsProcess(double delta) {
		// 炮台朝向和状态机控制
		foreach (var turretBase in Turrets) {
			HandleTurretRotation(delta, turretBase, TargetEntity);
			turretBase.DriveState(delta, this, TargetEntity);
		}
		HandleShipMvementState(delta, this, TargetEntity);
	}
}
