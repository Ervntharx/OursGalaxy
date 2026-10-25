using Godot;
using System;
using Godot.Collections;
using System.Collections.ObjectModel;
namespace OursGalaxy.UI;

public partial class MainPanel : Control {
	[Export]
	protected Button? ExitButton { get; set; }
	[Export]
	protected Button? SettingButton { get; set; }
	[Export]
	protected Button? PersonalArchiveButton { get; set; }

	[Export]
	protected Button? TechnologyButton { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		TechnologyButton!.Pressed += () => GD.Print("tech");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
