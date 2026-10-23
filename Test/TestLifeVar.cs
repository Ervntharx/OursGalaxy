using Godot;
using System;

public partial class TestLifeVar : Control {
	[Export]
	protected Label? LabelElement { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		LabelElement?.QueueFree();
		GD.Print(LabelElement is not null, GodotObject.IsInstanceValid(LabelElement));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {

	}
}
