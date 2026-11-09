using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class MissionBox : Button {
	public static MissionBox Load() => (MissionBox)ResourceLoader.Load<PackedScene>("res://Scene/MissionBox.tscn").Instantiate();

	[Export] protected RichTextLabel TitleLabel { get; set; } = null!;

	public event Action<string>? PressedMission;
}
