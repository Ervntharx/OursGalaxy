using Godot;
using OursGalaxy;
using System;
namespace OursGalaxy.Test;

internal partial class TestTurret : Node3D {
	[Export]
	protected LaserTurret Laser { get; set; } = null!;
	[Export]
	protected Node3D? CameraBase { get; set; }
	[Export]
	protected Node3D? CamerArm { get; set; }

	[Export]
	protected Camera3D Camera { get; set; } = null!;

	// [Export]
	// protected Node3D TurretBase { get; set; } = null!;
	// [Export]
	// protected Node3D Turret { get; set; } = null!;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		// ProcessRotate(delta);
		// Laser.TargetBasis = Camera.GlobalBasis;
		_delta = delta;
	}

	public override void _UnhandledInput(InputEvent @event) {
		if (@event is InputEventMouseButton mmb && mmb.ButtonIndex == MouseButton.Left) {
			touching = mmb.IsPressed();
		}
		if (touching && @event is InputEventMouseMotion mmo) {
			CameraBase!.RotateY(mmo.Relative.X * (float)_delta * 0.5f);
			CamerArm!.RotateX(-mmo.Relative.Y * (float)_delta * 0.5f);
		}
		base._UnhandledInput(@event);
	}

	// private void ProcessRotate(double delta) {
	// 	var rotateAngleVelocity = 45;
	// 	// Turret.Basis = (TurretBase.GlobalBasis.Inverse() * Camera.GlobalBasis);

	// 	var currentLocalBasis = Turret.Basis;
	// 	var localDir = (TurretBase.GlobalBasis.Inverse() * Camera.GlobalBasis).Z;
	// 	localDir.Y = 0;
	// 	if (localDir.Length() <= 0.005f) {
	// 		return;
	// 	}
	// 	var targetLocalBasis = Basis.LookingAt(localDir, Vector3.Up);
	// 	var currentLocalQ = new Quaternion(currentLocalBasis);
	// 	var targetLocalQ = new Quaternion(targetLocalBasis);

	// 	targetLocalQ = Clamp(targetLocalQ, 45, 45);

	// 	if (currentLocalQ.AngleTo(targetLocalQ) > float.DegreesToRadians(rotateAngleVelocity) * (float)delta) {
	// 		currentLocalQ = currentLocalQ.Slerp(targetLocalQ, float.DegreesToRadians(rotateAngleVelocity) * (float)delta / currentLocalQ.AngleTo(targetLocalQ));
	// 	}
	// 	else {
	// 		currentLocalQ = targetLocalQ;
	// 	}
	// 	Turret.Basis = new Basis(currentLocalQ);
	// }

	// private Quaternion Clamp(Quaternion dir, float leftClampAngle, float rightClampAngle) {
	// 	var euler = dir.GetEuler(EulerOrder.Yxz);
	// 	euler.Y = Mathf.Clamp(euler.Y, float.DegreesToRadians(-leftClampAngle), float.DegreesToRadians(rightClampAngle));
	// 	return Quaternion.FromEuler(euler);
	// }

	private bool touching = false;
	private double _delta;
}
