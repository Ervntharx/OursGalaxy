using System.Reflection;
using Godot;
namespace OursGalaxy.UI;

/// <summary>
/// 设置界面UI
/// </summary>
internal partial class SettingPanel : MarginContainer {
	public static SettingPanel Load() => (SettingPanel)ResourceLoader.Load<PackedScene>("res://Scene/SettingPanel.tscn").Instantiate();

	[Export] private Button QuitButton { get; set; } = null!;

	[Export] private RichTextLabel AboutPage { get; set; } = null!;

	#region Apperance
	[Export] private SpinBox CameraDefaultDistanceEdit { get; set; } = null!;
	[Export] private SpinBox CameraDistanceMaxEdit { get; set; } = null!;
	[Export] private SpinBox CameraDistanceMinEdit { get; set; } = null!;
	[Export] private SpinBox CameraMoveVelocityEdit { get; set; } = null!;
	[Export] private SpinBox CameraDistanceChangeVelocityEdit { get; set; } = null!;

	// panels margin
	[Export] private SpinBox PanelsMarginLeftBox { get; set; } = null!;
	[Export] private SpinBox PanelsMarginRightBox { get; set; } = null!;
	[Export] private SpinBox PanelsMarginTopBox { get; set; } = null!;
	[Export] private SpinBox PanelsMarginBottomBox { get; set; } = null!;
	#endregion Apperance

	#region Sound
	[Export] private CheckButton SoundMainSwitchButton { get; set; } = null!;
	#endregion Sound

	#region Advance
	[Export] private Button ClearSavesData { get; set; } = null!;
	#endregion Advance

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		base._Ready();
		QuitButton.Pressed += () => PanelsStack.PopPanel(this);

		SettingsDataSource.PanelsMarginChanged += OnMarginChanged;


		QuitButton.Pressed += QueueFree;
		AboutPage.MetaClicked += OnAboutPageMetaClicked;

		#region Apperance
		// camera
		CameraDefaultDistanceEdit.ValueChanged += (newValue) => SettingsDataSource.Camera_Default_Distance = (float)newValue;
		CameraDistanceMaxEdit.ValueChanged += (newValue) => SettingsDataSource.Camera_Distance_Max = (float)newValue;
		CameraDistanceMinEdit.ValueChanged += (newValue) => SettingsDataSource.Camera_Distance_Min = (float)newValue;
		CameraMoveVelocityEdit.ValueChanged += (newValue) => SettingsDataSource.Camera_Move_Velocity = (float)newValue;
		CameraDistanceChangeVelocityEdit.ValueChanged += (newValue) => SettingsDataSource.Camera_Distance_ChangsStep = (float)newValue;

		// panels margin
		PanelsMarginLeftBox.ValueChanged += (newValue) => {
			SettingsDataSource.Apperance_UI_Panels_Margin_Left = (int)newValue;
		};
		PanelsMarginRightBox.ValueChanged += (newValue) => { SettingsDataSource.Apperance_UI_Panels_Margin_Right = (int)newValue; };
		PanelsMarginTopBox.ValueChanged += (newValue) => { SettingsDataSource.Apperance_UI_Panels_Margin_Top = (int)newValue; };
		PanelsMarginBottomBox.ValueChanged += (newValue) => { SettingsDataSource.Apperance_UI_Panels_Margin_Bottom = (int)newValue; };
		#endregion Apperance

		#region Sound
		SoundMainSwitchButton.Toggled += (toggle) => SettingsDataSource.SoundMainSwitch = toggle;
		#endregion Sound

		#region Advance
		ClearSavesData.Pressed += () => GlobalDataSource.Source.Clear();
		#endregion Advance
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	public override void _ExitTree() {
		QuitButton.Pressed -= QueueFree;
		base._ExitTree();
	}

	private void OnMarginChanged(SettingsDataSource.PanelsMarginEnum panelsMarginEnum, int newValue) {
		switch (panelsMarginEnum) {
			case SettingsDataSource.PanelsMarginEnum.Left:
				this.AddThemeConstantOverride("margin_left", newValue);
				break;
			case SettingsDataSource.PanelsMarginEnum.Right:
				this.AddThemeConstantOverride("margin_right", newValue);
				break;
			case SettingsDataSource.PanelsMarginEnum.Top:
				this.AddThemeConstantOverride("margin_top", newValue);
				break;
			case SettingsDataSource.PanelsMarginEnum.Bottom:
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
