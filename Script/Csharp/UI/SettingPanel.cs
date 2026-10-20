using Godot;
using System;

public partial class SettingPanel : Panel {
	[Export]
	public Button? ExitButton { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		ExitButton!.Pressed += () => Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
