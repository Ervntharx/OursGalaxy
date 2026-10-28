using System.Reflection;
using Godot;
namespace OursGalaxy.UI;

/// <summary>
/// 设置界面UI
/// </summary>
internal partial class SettingPanel : MarginContainer {
	public static SettingPanel Load() => (SettingPanel)ResourceLoader.Load<PackedScene>("res://Scene/SettingPanel.tscn").Instantiate();

	[Export]
	protected Button QuitButton { get; set; } = null!;

	[Export]
	protected RichTextLabel? AboutPage { get; set; }

	#region Apperance
	[Export]
	protected SpinBox? PanelsMarginLeftBox { get; set; }
	[Export]
	protected SpinBox? PanelsMarginRightBox { get; set; }
	[Export]
	protected SpinBox? PanelsMarginTopBox { get; set; }
	[Export]
	protected SpinBox? PanelsMarginBottomBox { get; set; }
	#endregion Apperance

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		base._Ready();
		QuitButton.Pressed += () => PanelsStack.PopPanel(this);

		SettingsArchive.Data.PanelsMarginChanged += OnMarginChanged;


		QuitButton!.Pressed += QueueFree;
		AboutPage!.MetaClicked += OnAboutPageMetaClicked;

		#region Apperance
		PanelsMarginLeftBox!.ValueChanged += (newValue) => {
			SettingsArchive.Data.Apperance_UI_Panels_Margin_Left = (int)newValue;
		};
		PanelsMarginRightBox!.ValueChanged += (newValue) => { SettingsArchive.Data.Apperance_UI_Panels_Margin_Right = (int)newValue; };
		PanelsMarginTopBox!.ValueChanged += (newValue) => { SettingsArchive.Data.Apperance_UI_Panels_Margin_Top = (int)newValue; };
		PanelsMarginBottomBox!.ValueChanged += (newValue) => { SettingsArchive.Data.Apperance_UI_Panels_Margin_Bottom = (int)newValue; };
		#endregion Apperance
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	public override void _ExitTree() {
		QuitButton!.Pressed -= QueueFree;
		base._ExitTree();
	}

	private void OnMarginChanged(SettingsArchive.PanelsMarginEnum panelsMarginEnum, int newValue) {
		switch (panelsMarginEnum) {
			case SettingsArchive.PanelsMarginEnum.Left:
				this.AddThemeConstantOverride("margin_left", newValue);
				break;
			case SettingsArchive.PanelsMarginEnum.Right:
				this.AddThemeConstantOverride("margin_right", newValue);
				break;
			case SettingsArchive.PanelsMarginEnum.Top:
				this.AddThemeConstantOverride("margin_top", newValue);
				break;
			case SettingsArchive.PanelsMarginEnum.Bottom:
				this.AddThemeConstantOverride("margin_bottom", newValue);
				break;
			default:
				break;
		}
	}

	private void OnAboutPageMetaClicked(Variant variant) {
		var str = variant.ToString();
		if (str.StartsWith("open_url:")) {
			var url = str.Replace("open_url:", string.Empty);
			GD.Print(url);
			OS.ShellOpen(url);
		}
	}
}
