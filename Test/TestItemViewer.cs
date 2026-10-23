using Godot;
using OursGalaxy.UI;
using System;
namespace OursGalaxy.Test;

internal partial class TestItemViewer : Panel {
	[Export]
	protected Button? OpenButton { get; set; }
	[Export]
	protected ItemViewer? Viewer { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		OpenButton!.Pressed += () => GD.Print("her");
		var b = new GameItem("B", 5);
		Viewer!.Items.Add(new GameItem("A", 2));
		Viewer.Items.Add(b);
		Viewer.Items.Add(b);
		Viewer.Items.Add(new GameItem("C", 4));

		// Viewer.Items.Remove(b);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
