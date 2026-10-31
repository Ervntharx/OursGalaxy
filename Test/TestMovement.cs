using Godot;
using System;
using System.Security.Cryptography.X509Certificates;
namespace OursGalaxy.Test;

internal partial class TestMovement : Area3D {
	[Export]
	protected CharacterBody3D Util { get; set; } = null!;

	[Export]
	protected Node3D Target { get; set; } = null!;

	[Export]
	protected MeshInstance3D UtilTargetPos { get; set; } = null!;

	protected float MaxRange { get; set; } = 1.5f;
	protected float MinRange { get; set; } = 0.2f;

	protected float MovementCloneAngle { get; set; } = 25;

	public override void _Ready() {
		var timer = new Timer() { WaitTime = 1, Autostart = true, OneShot = false };
		// timer.Timeout += () => GD.Print(Util.Velocity, "\t,\t", IsInRange(targetLocalPos), IsInCone(targetLocalPos));
		AddChild(timer);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta) {
		if (GodotObject.IsInstanceValid(Target)) {
			// var offPos = Target.GlobalPosition - Util.GlobalPosition;
			var offPos = Target.GlobalTransform.Origin - Util.GlobalTransform.Origin;
			// var offPos = Util.ToLocal(Target.GlobalPosition);
			// GD.Print(IsInCone());
			if (offPos.Length() >= 0.05f) {
				if (!IsInCone()) {
					var targetDir = offPos.Normalized();
					var targetQ = new Quaternion(Vector3.Forward, targetDir);

					var currentQ = Util.GlobalBasis.Orthonormalized().GetRotationQuaternion().Normalized();
					if (currentQ.AngleTo(targetQ) >= float.DegreesToRadians(0.5f)) {
						Util.GlobalBasis = new(currentQ.Slerp(targetQ, Mathf.Clamp(2f * (float)delta / currentQ.AngleTo(targetQ), 0f, 1f)));
					}
					else {
						Util.GlobalBasis = new(currentQ);
					}

				}
				if (IsInCone()) {
					var distance = Target.GlobalPosition.DistanceTo(Util.GlobalPosition);
					if (distance > MaxRange) {
						Util.Velocity = -Util.GlobalBasis.Z * 5f;
						GD.Print("approaching");
					}
					else if (distance < MinRange) {
						Util.Velocity = Util.GlobalBasis.Z * 1f;
						GD.Print("away");
					}
					else {
						if (!Util.Velocity.IsZeroApprox()) {
							Util.Velocity = Util.Velocity.Lerp(new Vector3(), Math.Clamp(0.4f, 0f, 1f));
							GD.Print("stay");
						}
					}
				}
			}
			Util.MoveAndSlide();
			// Util.MoveAndCollide()
		}
	}

	// private bool IsInRange() {
	// 	return (Target.GlobalPosition - Util.GlobalPosition).Length() < MaxRange;
	// }

	private bool IsInCone() {
		return Util.ToLocal(Target.GlobalPosition).AngleTo(Vector3.Forward) <= float.DegreesToRadians(25);
	}

	// private Vector3 targetLocalPos;
}
