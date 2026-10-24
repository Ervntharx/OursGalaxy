using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class ShipCombatStatics : Panel {
	[Export]
	protected RichTextLabel? ShipNameLabel { get; set; }

	[Export]
	protected RichTextLabel? KillStaticsLabel { get; set; }

	[Export]
	protected ProgressBar? HPLeftProgressBar { get; set; }
	[Export]
	protected ProgressBar? DamageTakePrecentBar { get; set; }



	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
