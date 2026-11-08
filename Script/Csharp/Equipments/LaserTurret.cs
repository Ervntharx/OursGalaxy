using Godot;
using OursGalaxy.ResourceConfig;
using System.Collections.Generic;
using System.Linq;
namespace OursGalaxy;

internal partial class LaserTurret : TurretBase {
	public static LaserTurret Load() => (LaserTurret)ResourceLoader.Load<PackedScene>("res://Scene/LaserTurret.tscn").Instantiate();

	// [Export]
	// public Node3D RotatableComponent { get; set; } = null!;

	[Export]
	protected Area3D DamageArea { get; set; } = null!;

	[Export]
	protected CollisionShape3D DamageAreaShape { get; set; } = null!;

	public override float Range {
		get {
			return ((CylinderShape3D)DamageAreaShape.Shape).Height;
		}
		set {
			((CylinderShape3D)DamageAreaShape.Shape).Height = Mathf.Max(0, value);
		}
	}

	[Export] protected Node3D RotatableNode { get; set; } = null!;

	public float RotateAngleVelocity { get; set; } = TurretConfig.DEFAULT_ROTATE_ANGLE_VELOCITY;

	public override bool IsTargetInRange() => GodotObject.IsInstanceValid(Target?.AsNode3D()) && DamageArea.OverlapsArea(Target.AttackableArea);

	/// <summary>
	/// 进入CD的时间
	/// </summary>
	public ulong EnterCDTime { get; set; }

	/// <summary>
	/// CD时长
	/// </summary>
	public ulong CDMisc { get; set; }

	/// <summary>
	/// 多段伤害段时间间隙
	/// </summary>
	public ulong MakeDamageTimeGapMisc { get; set; } = 200;

	/// <summary>
	/// 总伤害段数。对应激光的连续伤害判定次数和射弹武器的连续射击次数
	/// </summary>
	public int MakeDamageCount { get; set; } = 1;

	/// <summary>
	/// 已造成伤害段数
	/// </summary>
	public int HasMadeDamageCount { get; set; }

	/// <summary>
	/// 上次造成伤害时间
	/// </summary>
	public ulong LastMakeDamageTime { get; set; }

	public override void _PhysicsProcess(double delta) {
		switch (State) {
			case TurretBase.StateEnum.IsReady:
				if (GodotObject.IsInstanceValid(Target?.AsNode3D()) && IsTargetInRange()) {
					State = TurretBase.StateEnum.OnFire;
				}
				break;
			case TurretBase.StateEnum.OnFire:
				// 已打出所有伤害段，进入冷却
				if (HasMadeDamageCount >= MakeDamageCount) {
					State = TurretBase.StateEnum.CoolDown;
					break;
				}
				// 继续打出伤害段
				if (Time.GetTicksMsec() - LastMakeDamageTime >= MakeDamageTimeGapMisc) {
					foreach (var entity in DamageArea.GetOverlappingAreas().OfType<IBattleEntity>()) {
						entity.Attacked(Damage, From);
					}
					LastMakeDamageTime = Time.GetTicksMsec();
					HasMadeDamageCount += 1;
					break;
				}
				break;
			case TurretBase.StateEnum.CoolDown:
				if (Time.GetTicksMsec() - EnterCDTime >= CDMisc) {
					State = TurretBase.StateEnum.IsReady;
				}
				break;
			default:
				break;
		}

		// rotation

		// 构造目标旋转：先算目标 Quaternion
		if (GodotObject.IsInstanceValid(Target?.AsNode3D())) {
			var lookQ = new Quaternion(Basis.LookingAt(Target.AsNode3D().GlobalTransform.Inverse() * Target.AsNode3D().GlobalPosition, Vector3.Up));

			// 限制俯仰：把目标四元数转欧拉角，夹住 Pitch
			Vector3 euler = lookQ.GetEuler();
			euler.X = Mathf.Clamp(euler.X, -Mathf.DegToRad(DownClampAngle), Mathf.DegToRad(UpClampAngle));
			euler.Y = Mathf.Clamp(euler.Y, -Mathf.DegToRad(LeftClampAngle), Mathf.DegToRad(RightClampAngle));
			euler.Z = 0; // 不滚转

			var clampedQ = Quaternion.FromEuler(euler);

			// 固定转速 Slerp
			var angleDiff = new Quaternion(RotatableNode!.Basis).AngleTo(clampedQ);
			var maxAngle = Mathf.DegToRad(RotateAngleVelocity) * (float)delta;

			RotatableNode.Basis = new Basis(new Quaternion(RotatableNode!.Basis).Slerp(clampedQ, (float)(angleDiff > 0.001f ? Mathf.Min(maxAngle / angleDiff, 1.0f) : 1.0f)));
		}

	}


	// private List<IPromote> promotes = [];
}


