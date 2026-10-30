using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class AlliancePanel : MarginContainer {
	public AlliancePanel Load() => (AlliancePanel)ResourceLoader.Load<PackedScene>("res://Scene/AlliancePanel.tscn").Instantiate();

	[Export]
	protected Button QuitButton { get; set; } = null!;
	[Export]
	protected TextureRect Avater { get; set; } = null!;
	[Export]
	protected RichTextLabel AllianceTitleLabel { get; set; } = null!;

	[Export]
	protected TabContainer TabCs { get; set; } = null!;
	[Export]
	protected VBoxContainer MembersPanel { get; set; } = null!;
	[Export]
	protected HBoxContainer SupportsFleetViewer { get; set; } = null!;
	[Export]
	protected VBoxContainer ChatPanel { get; set; } = null!;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		QuitButton.Pressed += () => PanelsStack.PopPanel(this);
		SettingsArchive.Data.PanelsMarginChanged += OnSettingMarginChanged;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private void OnSettingMarginChanged(SettingsArchive.PanelsMarginEnum panelsMarginEnum, int value) {
		switch (panelsMarginEnum) {
			case SettingsArchive.PanelsMarginEnum.Left:
				AddThemeConstantOverride("margin_left", value);
				break;
			case SettingsArchive.PanelsMarginEnum.Right:
				AddThemeConstantOverride("margin_right", value);
				break;
			case SettingsArchive.PanelsMarginEnum.Top:
				AddThemeConstantOverride("margin_top", value);
				break;
			case SettingsArchive.PanelsMarginEnum.Bottom:
				AddThemeConstantOverride("margin_buttom", value);
				break;
			default:
				break;
		}
	}
}
