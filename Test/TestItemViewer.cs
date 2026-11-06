using Godot;
using OursGalaxy.ResourceConfig;
using OursGalaxy.UI;
using System;
using System.Threading.Tasks;
namespace OursGalaxy.Test;

internal partial class TestItemViewer : Panel {
	[Export]
	protected Button? OpenButton { get; set; }
	[Export]
	protected ItemViewerPanel? Viewer { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready() {
		OpenButton!.Pressed += () => GD.Print("her");

		Viewer!.Items.Add(new GameItem("Test", "A", ItemBaseConfig.QulityEnum.Purple, 2));
		Viewer!.Items.Add(new GameItem("Test", "B", ItemBaseConfig.QulityEnum.Purple, 2));
		Viewer!.Items.Add(new GameItem("Test", "C", ItemBaseConfig.QulityEnum.Purple, 2));

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
