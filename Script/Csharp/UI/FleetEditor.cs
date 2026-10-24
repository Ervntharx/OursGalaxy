using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class FleetEditor : Node3D {
	public FleetEditor Load() => (FleetEditor)ResourceLoader.Load<PackedScene>("res://Scene/FleetEditor.tscn").Instantiate();

	[Export]
	protected LineEdit? FleetNameEdit { get; set; }

	[Export]
	protected Button? QuitButton { get; set; }

	[Export]
	protected Label? SizeLabel { get; set; }

	[Export]
	protected Button? AddShip { get; set; }

	[Export]
	protected Button? ChangeFlagShip { get; set; }

	[Export]
	protected VBoxContainer? ShipsOverviewsPanel { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
