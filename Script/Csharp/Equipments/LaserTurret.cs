using Godot;
using System.Collections.Generic;
namespace OursGalaxy;

internal partial class LaserTurret : Node3D, ITurret {
	public static LaserTurret Load() => (LaserTurret)ResourceLoader.Load<PackedScene>("res://Scene/LaserTurret.tscn").Instantiate();

	[Export]
	public Node3D RotatableComponent { get; set; } = null!;

	[Export]
	protected Area3D DamageArea { get; set; } = null!;
	[Export]
	protected CollisionShape3D DamageAreaShape { get; set; } = null!;

	public OutletSizeType SizeType { get; protected set; } = new(OutletSizeType.SizeTypeEnum.S);

	public ulong CDMisc { get; set; } = 1500;

	public IReadOnlyCollection<IPromote> Promotes => promotes.AsReadOnly();

	public float RotateAngleVelocity { get; set; } = 45;

	public ITurret.StateEnum State { get; protected set; } = ITurret.StateEnum.Ready;

	public float LeftYawClampAngle { get; set; } = 180;
	public float RightYawClampAngle { get; set; } = 180;

	public bool IsAimmed => IsInstanceValid(TargetEntity) && DamageArea.OverlapsBody(TargetEntity);

	public float Range {
		get {
			return ((CylinderShape3D)(DamageAreaShape.Shape)).Height;
		}
		set {
			((CylinderShape3D)(DamageAreaShape.Shape)).Height = Range;
			var pos = Position;
			pos.Z = -value / 2;
		}
	}

	public Node3D? TargetEntity { get; set; }

	public ulong last_damage_time = 0;

	public void Install(Outlet outlet) {
		LeftYawClampAngle = outlet.LimitYawLeft;
		RightYawClampAngle = outlet.LimitYawRight;
	}

	public override void _Ready() {
		base._Ready();
		// var shape = ((CylinderShape3D)(RayShape.Shape));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		// fire
		switch (State) {
			case ITurret.StateEnum.Ready:
				if (IsAimmed) {
					State = ITurret.StateEnum.Fire;
				}
				break;
			case ITurret.StateEnum.Fire:
				var damage_target_ls = DamageArea.GetOverlappingAreas();
				if (damage_target_ls.Count != 0) {
					GD.Print(damage_target_ls);
				}
				State = ITurret.StateEnum.CoolDown;
				last_damage_time = Time.GetTicksMsec();
				break;
			case ITurret.StateEnum.CoolDown:
				if (Time.GetTicksMsec() - last_damage_time >= CDMisc) {
					State = ITurret.StateEnum.Ready;
				}
				break;
			default:
				break;
		}
	}

	public override void _PhysicsProcess(double delta) {
		RotateTurret(delta);
		base._PhysicsProcess(delta);
	}

	private void RotateTurret(double delta) {
		var currentLocalBasis = RotatableComponent.Basis;
		if (GodotObject.IsInstanceValid(TargetEntity)) {
			var localDir = (GlobalTransform.Inverse() * TargetEntity.GlobalPosition);
			localDir.Y = 0;
			var targetLocalBasis = Basis.LookingAt(localDir.Normalized(), Vector3.Up);
			var currentLocalQ = new Quaternion(currentLocalBasis);
			var targetLocalQ = new Quaternion(targetLocalBasis);

			targetLocalQ = ClampAngle(targetLocalQ, LeftYawClampAngle, RightYawClampAngle);

			if (currentLocalQ.AngleTo(targetLocalQ) > float.DegreesToRadians(RotateAngleVelocity) * (float)delta) {
				currentLocalQ = currentLocalQ.Slerp(targetLocalQ, float.DegreesToRadians(RotateAngleVelocity) * (float)delta / currentLocalQ.AngleTo(targetLocalQ));
			}
			else {
				currentLocalQ = targetLocalQ;
			}
			RotatableComponent.Basis = new Basis(currentLocalQ);
		}
		Quaternion ClampAngle(Quaternion dir, float leftClampAngle, float rightClampAngle) {
			var euler = dir.GetEuler(EulerOrder.Yxz);
			euler.Y = Mathf.Clamp(euler.Y, float.DegreesToRadians(-leftClampAngle), float.DegreesToRadians(rightClampAngle));
			return Quaternion.FromEuler(euler);
		}
	}

	private List<IPromote> promotes = [];
}


