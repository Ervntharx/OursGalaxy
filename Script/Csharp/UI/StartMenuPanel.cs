using Godot;
using System;
namespace OursGalaxy.UI;

/// <summary>
/// 开始主界面
/// </summary>
internal partial class StartMenuPanel : MarginContainer {
	public static StartMenuPanel Load() => (StartMenuPanel)ResourceLoader.Load<PackedScene>("res://Scene/StartMenuPanel.tscn").Instantiate();

	[Export]
	public Button? EnterButton { get; set; }
	[Export]
	public Button? SettingsButton { get; set; }
	[Export]
	public Button? ExitButton { get; set; }

	[Export]
	public ConfirmationDialog? ExitConfirmation { get; set; }

	// public const string SETTING_PANEL_NODE_NAME = "SettingPanel";
	[Export]
	public PackedScene? SettingPanelScene { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		SettingsButton!.Pressed += () => {
			AddChild(SettingPanel.Load());
		};

		ExitConfirmation!.Confirmed += () => { GetTree().Quit(); };
		ExitButton!.Pressed += () => {
			ExitConfirmation!.PopupCentered();
		};

		//theme_override_constants.margin_left
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
