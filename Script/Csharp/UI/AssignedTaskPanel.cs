using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class AssignedTaskPanel : MarginContainer {
	public static AssignedTaskPanel Load() => (AssignedTaskPanel)ResourceLoader.Load<PackedScene>("res://Scene/AssignedTaskPanel.tscn").Instantiate();

	[Export] private Button QuitButton { get; set; } = null!;
	[Export] private HFlowContainer AcceptableTaskList { get; set; } = null!;
	[Export] private HFlowContainer AcceptedTaskList { get; set; } = null!;

	public override void _EnterTree() {
		base._EnterTree();
	}

	public override void _ExitTree() {
		base._ExitTree();
	}
}
