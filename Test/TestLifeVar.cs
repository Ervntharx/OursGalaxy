using Godot;
using OursGalaxy;
using System;
namespace OursGalaxy;

internal partial class TestLifeVar : Control {
	[Export]
	protected Archive? ArchiveData { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		var a = 2;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {

	}
}
