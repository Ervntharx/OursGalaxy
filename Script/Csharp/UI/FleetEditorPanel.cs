using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class FleetEditorPanel : MarginContainer {
	public static FleetEditorPanel Load() => (FleetEditorPanel)ResourceLoader.Load<PackedScene>("res://Scene/FleetEditorPanel.tscn").Instantiate();

	[Export]
	protected Button? QuitButton { get; set; }

	[Export]
	protected LineEdit? FleetNameEdit { get; set; }

	[Export]
	protected Button? ChangeFlagShip { get; set; }

	[Export]
	protected Button? AddShip { get; set; }

	[Export]
	protected VBoxContainer? ShipsOverviewsPanel { get; set; }

	[Export]
	protected RichTextLabel? ShipsCountLabel { get; set; }

	[Export]
	protected RichTextLabel? FleetSizeLabel { get; set; }

	[Export]
	protected RichTextLabel? FleetRaidLabel { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
