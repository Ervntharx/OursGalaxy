using Godot;
using System;
namespace OursGalaxy;

internal partial class TemplateShip : Node3D {
	[Export]
	public Godot.Collections.Dictionary<OutletSizeType, NodePath> Outlets { get; set; } = [];

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
