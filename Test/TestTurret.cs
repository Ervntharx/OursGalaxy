using Godot;
using OursGalaxy;
using System;
namespace OursGalaxy.Test;

internal partial class TestTurret : Node3D {
	[Export] protected Node3D? CameraBase { get; set; }
	[Export] protected Node3D? CamerArm { get; set; }
	[Export] protected Camera3D Camera { get; set; } = null!;

	[Export] protected MeshInstance3D TurretBaseNode { get; set; } = null!;
	[Export] protected MeshInstance3D TurretRotatableNode { get; set; } = null!;

	[Export] public float RotateAngleVelocity { get; set; } = 35;
	[Export] public float MinYaw { get; set; } = 30;
	[Export] public float MaxYaw { get; set; } = 30;
	[Export] public float MinPitch { get; set; } = 15;
	[Export] public float MaxPitch { get; set; } = 15;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		_delta = delta;
	}

	public override void _PhysicsProcess(double delta) {
		// 构造目标旋转：先算目标 Quaternion
		var lookQ = new Quaternion(Basis.LookingAt(-(TurretBaseNode.GlobalTransform.Inverse() * Camera.GlobalPosition), Vector3.Up));

		// 限制俯仰：把目标四元数转欧拉角，夹住 Pitch
		Vector3 euler = lookQ.GetEuler();
		euler.X = Mathf.Clamp(euler.X, -Mathf.DegToRad(MaxPitch), Mathf.DegToRad(MaxPitch));
		euler.Y = Mathf.Clamp(euler.Y, -Mathf.DegToRad(MinYaw), Mathf.DegToRad(MaxYaw));
		euler.Z = 0; // 不滚转

		var clampedQ = Quaternion.FromEuler(euler);

		// 固定转速 Slerp
		var angleDiff = new Quaternion(TurretRotatableNode!.Basis).AngleTo(clampedQ);
		var maxAngle = Mathf.DegToRad(RotateAngleVelocity) * (float)delta;

		TurretRotatableNode.Basis = new Basis(new Quaternion(TurretRotatableNode!.Basis).Slerp(clampedQ, (float)(angleDiff > 0.001f ? Mathf.Min(maxAngle / angleDiff, 1.0f) : 1.0f)));
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

	private bool touching = false;
	private double _delta;
}
