using Godot;
using System;
using System.Collections.Generic;
namespace OursGalaxy;

internal partial class PhotonTurret : Node3D, ITurret {
	public static PhotonTurret Load() => (PhotonTurret)ResourceLoader.Load<PackedScene>("res://Scene/PhotonTurret.tscn").Instantiate();

	[Export]
	protected Node3D RotatableComponent { get; set; } = null!;

	[Export]
	protected Area3D Trajectory { get; set; } = null!;
	[Export]
	protected CollisionShape3D TrajectoryShape { get; set; } = null!;

	public OutletSizeType SizeType { get; protected set; } = new(OutletSizeType.SizeTypeEnum.S);

	public ulong CDMisc { get; set; } = 3000;

	public IReadOnlyCollection<IPromote> Promotes => promotes.AsReadOnly();

	public float RotateAngleVelocity { get; set; } = 45;

	public float LeftYawClampAngle { get; set; } = 180;
	public float RightYawClampAngle { get; set; } = 180;

	public Node3D? TargetEntity { get; set; }

	public ITurret.StateEnum State { get; protected set; } = ITurret.StateEnum.Ready;

	public bool IsAimmed => GodotObject.IsInstanceValid(TargetEntity) && Trajectory.OverlapsBody(TargetEntity);

	public float Range {
		get => ((CylinderShape3D)TrajectoryShape.Shape).Height;
		set {
			var pos = TrajectoryShape.Position;
			pos.Z = value / 2;
			TrajectoryShape.Position = pos;
			((CylinderShape3D)TrajectoryShape.Shape).Height = value;
		}
	}

	public PackedScene? BulletRes { get; set; }

	public void Install(Outlet outlet) {
		LeftYawClampAngle = outlet.LeftClampAngle;
		RightYawClampAngle = outlet.RightClampAngle;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
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
				BulletServer.Add(null, (IBullet)BulletRes!.Instantiate(), new Transform3D(RotatableComponent.GlobalBasis, Trajectory.GlobalPosition), RotatableComponent.GlobalBasis.Z * 5);
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
	private ulong last_damage_time;
}
