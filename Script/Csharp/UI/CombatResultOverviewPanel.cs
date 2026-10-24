using Godot;
using System;

public partial class CombatResultOverviewPanel : Panel {
	[Export]
	protected RichTextLabel? CombatResultLabel { get; set; }

	[Export]
	protected VBoxContainer? LeftPanel { get; set; }
	[Export]
	protected VBoxContainer? RightPanel { get; set; }

	[Export]
	protected Button? Comfirm { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
