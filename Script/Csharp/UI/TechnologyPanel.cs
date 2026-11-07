using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class TechnologyPanel : MarginContainer {
	public static TechnologyPanel Load() => (TechnologyPanel)ResourceLoader.Load<PackedScene>("res://Scene/TechnologyPanel.tscn").Instantiate();

	[Export] protected Button QuitButton { get; set; } = null!;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		QuitButton.Pressed += () => PanelsStack.PopPanel(this);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
