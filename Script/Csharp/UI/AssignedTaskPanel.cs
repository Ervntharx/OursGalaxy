using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class AssignedTaskPanel : Window {
	public AssignedTaskPanel Load() => (AssignedTaskPanel)ResourceLoader.Load<PackedScene>("res://Scene/MissionPanel.tscn").Instantiate();

	[Export]
	protected HFlowContainer? AllMissionsPanel { get; set; }
	[Export]
	protected HFlowContainer? AcceptedMissionPanel { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
