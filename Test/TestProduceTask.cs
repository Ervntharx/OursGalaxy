using Godot;
using OursGalaxy;
using OursGalaxy.UI;
using System;

namespace OursGalaxy.Test;

internal partial class TestProduceTask : Control {
	[Export]
	protected FactoryPanel? FP { get; set; }


	public override void _Ready() {

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

}
