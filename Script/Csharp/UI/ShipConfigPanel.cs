using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class ShipConfigPanel : MarginContainer {
	public static ShipConfigPanel Load() => (ShipConfigPanel)ResourceLoader.Load<PackedScene>("res://Scene/ShipConfigPanel.tscn").Instantiate();

	[Export]
	protected Button QuitButton { get; set; } = null!;

	[Export]
	protected ItemViewerPanel IVPanel { get; set; } = null!;

	[Export]
	protected SubViewport ShipOverview { get; set; } = null!;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
