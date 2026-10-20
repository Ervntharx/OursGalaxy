using Godot;
using System;

internal partial class StartMenu : Panel {
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
			var settingPanel = GetNodeOrNull<SettingPanel?>("SettingPanel");
			if (settingPanel is null) {
				var scene = SettingPanelScene!.Instantiate();
				// scene.Name = SETTING_PANEL_NODE_NAME;
				AddChild(scene);
				settingPanel = GetNodeOrNull<SettingPanel?>("SettingPanel");
			}
			else {
				settingPanel.Show();
			}

		};

		ExitConfirmation!.Confirmed += () => { GetTree().Quit(); };
		ExitButton!.Pressed += () => {
			ExitConfirmation!.PopupCentered();
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
