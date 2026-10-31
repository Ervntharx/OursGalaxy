using Godot;
using System.Collections.Generic;
using System.Linq;
namespace OursGalaxy;

internal partial class LaserTurret : TurretBase {
	public static LaserTurret Load() => (LaserTurret)ResourceLoader.Load<PackedScene>("res://Scene/LaserTurret.tscn").Instantiate();

	// [Export]
	// public Node3D RotatableComponent { get; set; } = null!;

	// [Export]
	// protected Area3D DamageArea { get; set; } = null!;
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

	// public OutletSizeType SizeType { get; protected set; } = new(OutletSizeType.SizeTypeEnum.S);

	// public ulong CDMisc { get; set; } = 1500;

	// public IReadOnlyCollection<IPromote> Promotes => promotes.AsReadOnly();

	// public float RotateAngleVelocity { get; set; } = 45;

	// public ITurret.StateEnum State { get; protected set; } = ITurret.StateEnum.Ready;

	// public float LeftYawClampAngle { get; set; } = 180;
	// public float RightYawClampAngle { get; set; } = 180;

	// public bool IsAimmed => IsInstanceValid(TargetEntity) && DamageArea.OverlapsBody(TargetEntity);

	// public float Range {
	// 	get {
	// 		return ((CylinderShape3D)(DamageAreaShape.Shape)).Height;
	// 	}
	// 	set {
	// 		((CylinderShape3D)(DamageAreaShape.Shape)).Height = Range;
	// 		var pos = Position;
	// 		pos.Z = -value / 2;
	// 	}
	// }

	// public Node3D? TargetEntity { get; set; }

	// public ulong last_damage_time = 0;

	// public void Install(Outlet outlet) {
	// 	LeftYawClampAngle = outlet.LeftClampAngle;
	// 	RightYawClampAngle = outlet.RightClampAngle;
	// }

	// public override void _Ready() {
	// 	base._Ready();
	// 	// var shape = ((CylinderShape3D)(RayShape.Shape));
	// }

	public override void DriveState(double delta, IBattleEntity from, IBattleEntity? battleEntity) {
		switch (State) {
			case TurretBase.StateEnum.IsReady:
				if (GodotObject.IsInstanceValid(battleEntity?.AsNode3D()) && IsEntityInRange(battleEntity)) {
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
						entity.Attacked(Damage);
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
	}

	// public override void _PhysicsProcess(double delta) {
	// 	// RotateTurret(delta);
	// 	base._PhysicsProcess(delta);
	// }

	// private void RotateTurret(double delta) {
	// 	var currentLocalBasis = RotatableComponent.Basis;
	// 	if (GodotObject.IsInstanceValid(TargetEntity)) {
	// 		var localDir = (GlobalTransform.Inverse() * TargetEntity.GlobalPosition);
	// 		localDir.Y = 0;
	// 		var targetLocalBasis = Basis.LookingAt(localDir.Normalized(), Vector3.Up);
	// 		var currentLocalQ = new Quaternion(currentLocalBasis);
	// 		var targetLocalQ = new Quaternion(targetLocalBasis);

	// 		targetLocalQ = ClampAngle(targetLocalQ, LeftYawClampAngle, RightYawClampAngle);

	// 		if (currentLocalQ.AngleTo(targetLocalQ) > float.DegreesToRadians(RotateAngleVelocity) * (float)delta) {
	// 			currentLocalQ = currentLocalQ.Slerp(targetLocalQ, float.DegreesToRadians(RotateAngleVelocity) * (float)delta / currentLocalQ.AngleTo(targetLocalQ));
	// 		}
	// 		else {
	// 			currentLocalQ = targetLocalQ;
	// 		}
	// 		RotatableComponent.Basis = new Basis(currentLocalQ);
	// 	}
	// 	Quaternion ClampAngle(Quaternion dir, float leftClampAngle, float rightClampAngle) {
	// 		var euler = dir.GetEuler(EulerOrder.Yxz);
	// 		euler.Y = Mathf.Clamp(euler.Y, float.DegreesToRadians(-leftClampAngle), float.DegreesToRadians(rightClampAngle));
	// 		return Quaternion.FromEuler(euler);
	// 	}
	// }

	// private List<IPromote> promotes = [];
}


