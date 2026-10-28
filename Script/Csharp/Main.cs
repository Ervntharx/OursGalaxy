using Godot;
using OursGalaxy.UI;
using System;
namespace OursGalaxy;

internal partial class Main : Node3D {
	[Export]
	protected Camera3D Camera { get; set; } = null!;

	protected const double ASPECT = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		GetTree().Root.SizeChanged += OnRootViewportSizeChanged;
		PanelsStack.PushPanel(StartMenuPanel.Load());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	public override void _ExitTree() {
		GetTree().Root.SizeChanged -= OnRootViewportSizeChanged;
		base._ExitTree();
	}

	private void OnRootViewportSizeChanged() {
		var currentAspect = GetTree().Root.Size.X / (double)GetTree().Root.Size.Y;
		if (currentAspect >= ASPECT) {
			Camera.KeepAspect = Camera3D.KeepAspectEnum.Height;
		}
		else {
			Camera.KeepAspect = Camera3D.KeepAspectEnum.Width;
		}
	}
}
