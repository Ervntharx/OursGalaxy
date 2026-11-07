using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class FleetEditorPanel : MarginContainer {
	public static FleetEditorPanel Load() => (FleetEditorPanel)ResourceLoader.Load<PackedScene>("res://Scene/FleetEditorPanel.tscn").Instantiate();

	[Export] protected Button QuitButton { get; set; } = null!;

	[Export] protected LineEdit? FleetNameEdit { get; set; }

	[Export] protected Button? ChangeFlagShip { get; set; }

	[Export] protected Button? AddShip { get; set; }

	[Export] protected VBoxContainer? ShipsOverviewsPanel { get; set; }

	[Export] protected RichTextLabel? ShipsCountLabel { get; set; }

	[Export] protected RichTextLabel? FleetSizeLabel { get; set; }

	[Export] protected RichTextLabel? FleetRaidLabel { get; set; }

	[Export] protected Node3D CameraArm { get; set; } = null!;
	[Export] protected Camera3D Camera { get; set; } = null!;

	[Export] protected Node3D FleetStage { get; set; } = null!;


	public void Make(FleetData fleetData) {

	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		QuitButton.Pressed += () => PanelsStack.PopPanel(this);
	}
}
