using Godot;
using OursGalaxy.UI;
using System;
using System.Threading.Tasks;
namespace OursGalaxy.Test;

internal partial class TestItemViewer : Panel {
	[Export]
	protected Button? OpenButton { get; set; }
	[Export]
	protected ItemViewer? Viewer { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready() {
		OpenButton!.Pressed += () => GD.Print("her");

		Viewer!.Items.Add(new GameItem("A", 2));
		Viewer.Items.Add(new GameItem("B", 3));
		Viewer.Items.Add(new GameItem("C", 4));

		Viewer.Show();

		await Task.Delay(500);

		var itemBaseB = Viewer!.Items[1];
		if (itemBaseB is GameItem gameItemB) {
			gameItemB.Num = 9999;
			GD.Print("changed");
		}
		GD.Print("end.");
		// Viewer.Items.Remove(b);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
