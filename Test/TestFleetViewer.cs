using Godot;
using System;
using OursGalaxy.UI;
namespace OursGalaxy.Test;

internal partial class TestFleetViewer : Control {
	[Export]
	protected FleetsViewer? TestedFleetsViewer { get; set; }


	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {

		// for test only
		TestedFleetsViewer!.Fleets.Add(new(new("旗舰型号名A", "自定义名A")));
		TestedFleetsViewer!.Fleets.Add(new(new("旗舰型号名B", "自定义名B")));
		TestedFleetsViewer!.Fleets.Add(new(new("旗舰型号名C", "自定义名C")));
		TestedFleetsViewer!.Fleets.Add(new(new("旗舰型号名D", "自定义名D")));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
