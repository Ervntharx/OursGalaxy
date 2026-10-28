using Godot;
using System;
namespace OursGalaxy;

internal partial class PowerUpPanel : MarginContainer {
	public PowerUpPanel Load() => (PowerUpPanel)ResourceLoader.Load<PackedScene>("res://Scene/PowerUpPanel.tscn").Instantiate();

	[Export]
	protected Button QuitButton { get; set; } = null!;
	[Export]
	protected VBoxContainer PowerUpItemListPanel { get; set; } = null!;
	[Export]
	protected HFlowContainer MaterialPanel { get; set; } = null!;
	[Export]
	protected ProgressBar PgrsBar { get; set; } = null!;
	[Export]
	protected Button PowerUpButton { get; set; } = null!;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
