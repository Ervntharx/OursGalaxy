using Godot;
using OursGalaxy;
using OursGalaxy.UI;
using System;

namespace OursGalaxy.Test;

internal partial class TestProduceTask : Control {
	[Export]
	protected ProduceLine? UI { get; set; }


	public override void _Ready() {
		var task = new ProduceTaskData(DateTime.Now(), TimeSpan.FromSeconds(7), new ItemBase("anything"));
		// GD.Print(task);
		// UI!.Make(2, task);
		UI!.OperatePressed += (opr) => { GD.Print(opr); };

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

}
