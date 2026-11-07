using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class MissionBox : Button {
	public static MissionBox Load() => (MissionBox)ResourceLoader.Load<PackedScene>("res://Scene/MissionBox.tscn").Instantiate();

	[Export] protected RichTextLabel TitleLabel { get; set; } = null!;

	public event Action<string>? PressedMission;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
