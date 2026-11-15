using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class ProfilePanel : MarginContainer {
	public static ProfilePanel Load() => (ProfilePanel)ResourceLoader.Load<PackedScene>("res://Scene/ProfilePanel.tscn").Instantiate();

	[Export] private Button QuitButton { get; set; } = null!;

	public override void _EnterTree() {
		base._EnterTree();
		QuitButton.Pressed += OnQuitPressed;
	}

	public override void _ExitTree() {
		QuitButton.Pressed -= OnQuitPressed;
		base._ExitTree();
	}

	private void OnQuitPressed() {
		PanelsStack.PopPanel(this);
	}
}
