using Godot;
using System;
namespace OursGalaxy.Test;

public partial class TestWindowMode : Node3D {
	[Export]
	protected Camera3D? Camera { get; set; }

	public double Aspect = 1;
	[Export]
	public double CurrentAspect { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		GetTree().Root.SizeChanged += OnRootViewportSizeChanged;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	public override void _ExitTree() {
		GetTree().Root.SizeChanged -= OnRootViewportSizeChanged;
		base._ExitTree();
	}

	private void OnRootViewportSizeChanged() {
		// GD.Print($"size changed, {(double)GetTree().Root.Size.X / (double)GetTree().Root.Size.Y}");
		CurrentAspect = (double)GetTree().Root.Size.X / (double)GetTree().Root.Size.Y;
		if (CurrentAspect >= Aspect) {
			Camera!.KeepAspect = Camera3D.KeepAspectEnum.Height;
		}
		else {
			Camera!.KeepAspect = Camera3D.KeepAspectEnum.Width;
		}
	}
}
