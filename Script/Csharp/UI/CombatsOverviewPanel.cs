using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class CombatsOverviewPanel : MarginContainer {
	public CombatsOverviewPanel Load() => (CombatsOverviewPanel)ResourceLoader.Load<PackedScene>("res://Scene/CombatsOverviewPanel.tscn").Instantiate();

	[Export]
	protected Button? QuitButton { get; set; }

	[Export]
	protected HFlowContainer? CombatsPanel { get; set; }

	[Export]
	protected HFlowContainer? HuntsPanel { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
