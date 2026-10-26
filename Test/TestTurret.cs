using Godot;
using OursGalaxy;
using System;
namespace OursGalaxy.Test;

internal partial class TestTurret : Node3D {
	[Export]
	protected LaserTurret? Laser { get; set; }
	[Export]
	protected Node3D? CameraBase { get; set; }
	[Export]
	protected Node3D? CamerArm { get; set; }

	[Export]
	protected Camera3D? Camera { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		Laser!.TargetBasis = Camera!.GlobalBasis;
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

	private bool touching = false;
	private double _delta;
}
