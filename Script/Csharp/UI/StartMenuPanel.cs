using Godot;
using System;
namespace OursGalaxy.UI;

/// <summary>
/// 开始主界面
/// </summary>
internal partial class StartMenuPanel : MarginContainer {
	public static StartMenuPanel Load() => (StartMenuPanel)ResourceLoader.Load<PackedScene>("res://Scene/StartMenuPanel.tscn").Instantiate();

	[Export]
	public Button EnterButton { get; set; } = null!;
	[Export]
	public Button SettingsButton { get; set; } = null!;
	[Export]
	public Button ExitButton { get; set; } = null!;

	// public const string SETTING_PANEL_NODE_NAME = "SettingPanel";
	[Export]
	public PackedScene? SettingPanelScene { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		EnterButton.Pressed += () => {
			PanelsStack.PushPanel(MainPanel.Load());
			PanelsStack.PopPanel(this);
		};
		SettingsButton.Pressed += () => { PanelsStack.PushPanel(SettingPanel.Load()); };
		ExitButton.Pressed += () => { PanelsStack.RequireQuitGame(); };
	}

	~StartMenuPanel() {
		GD.Print("free");
	}
}
