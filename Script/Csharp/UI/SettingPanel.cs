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
	public override void _EnterTree() {
		base._EnterTree();
		SettingsDataSource.PanelsMarginChanged += OnMarginChanged;

		QuitButton.Pressed += OnQuitPressed;

		#region Apperance
		// camera
		CameraDefaultDistanceEdit.Value = SettingsDataSource.Camera_Default_Distance;
		CameraDistanceMaxEdit.Value = SettingsDataSource.Camera_Distance_Max;
		CameraDistanceMinEdit.Value = SettingsDataSource.Camera_Distance_Min;
		CameraMoveVelocityEdit.Value = SettingsDataSource.Camera_Move_Velocity;
		CameraDistanceChangeVelocityEdit.Value = SettingsDataSource.Camera_Distance_ChangsStep;

		CameraDefaultDistanceEdit.ValueChanged += SetCameraDefualtDistance;
		CameraDistanceMaxEdit.ValueChanged += SetCameraDistanceMax;
		CameraDistanceMinEdit.ValueChanged += SetCameraDistanceMin;
		CameraMoveVelocityEdit.ValueChanged += SetCameraMoveVelocity;
		CameraDistanceChangeVelocityEdit.ValueChanged += SetCameraDistanceChangeVelocity;

		// panels margin
		PanelsMarginLeftBox.Value = SettingsDataSource.Apperance_UI_Panels_Margin_Left;
		PanelsMarginRightBox.Value = SettingsDataSource.Apperance_UI_Panels_Margin_Right;
		PanelsMarginTopBox.Value = SettingsDataSource.Apperance_UI_Panels_Margin_Top;
		PanelsMarginBottomBox.Value = SettingsDataSource.Apperance_UI_Panels_Margin_Bottom;

		PanelsMarginLeftBox.ValueChanged += SetPanelsMariginLeft;
		PanelsMarginRightBox.ValueChanged += SetPanelsMariginRight;
		PanelsMarginTopBox.ValueChanged += SetPanelsMariginTop;
		PanelsMarginBottomBox.ValueChanged += SetPanelsMariginBottom;
		#endregion Apperance

		#region Sound
		SoundMainSwitchButton.Toggled += SetSoundSwitch;
		#endregion Sound

		#region Advance
		ClearSavesData.Pressed += () => GlobalDataSource.Source.Clear();
		#endregion Advance

		AboutPage.MetaClicked += OnAboutPageMetaClicked;
	}

	public override void _ExitTree() {
		QuitButton.Pressed -= OnQuitPressed;

		CameraDefaultDistanceEdit.ValueChanged -= SetCameraDefualtDistance;
		CameraDistanceMaxEdit.ValueChanged -= SetCameraDistanceMax;
		CameraDistanceMinEdit.ValueChanged -= SetCameraDistanceMin;
		CameraMoveVelocityEdit.ValueChanged -= SetCameraMoveVelocity;
		CameraDistanceChangeVelocityEdit.ValueChanged -= SetCameraDistanceChangeVelocity;

		PanelsMarginLeftBox.ValueChanged -= SetPanelsMariginLeft;
		PanelsMarginRightBox.ValueChanged -= SetPanelsMariginRight;
		PanelsMarginTopBox.ValueChanged -= SetPanelsMariginTop;
		PanelsMarginBottomBox.ValueChanged -= SetPanelsMariginBottom;

		SoundMainSwitchButton.Toggled -= SetSoundSwitch;

		SettingsDataSource.PanelsMarginChanged -= OnMarginChanged;
		AboutPage.MetaClicked -= OnAboutPageMetaClicked;
		base._ExitTree();
	}

	private void SetCameraDefualtDistance(double newValue) => SettingsDataSource.Camera_Default_Distance = (float)newValue;
	private void SetCameraDistanceMax(double newValue) => SettingsDataSource.Camera_Distance_Max = (float)newValue;
	private void SetCameraDistanceMin(double newValue) => SettingsDataSource.Camera_Distance_Min = (float)newValue;
	private void SetCameraMoveVelocity(double newValue) => SettingsDataSource.Camera_Move_Velocity = (float)newValue;
	private void SetCameraDistanceChangeVelocity(double newValue) => SettingsDataSource.Camera_Distance_ChangsStep = (float)newValue;

	private void SetPanelsMariginRight(double newValue) => SettingsDataSource.Apperance_UI_Panels_Margin_Right = (int)newValue;
	private void SetPanelsMariginLeft(double newValue) => SettingsDataSource.Apperance_UI_Panels_Margin_Left = (int)newValue;
	private void SetPanelsMariginTop(double newValue) => SettingsDataSource.Apperance_UI_Panels_Margin_Top = (int)newValue;
	private void SetPanelsMariginBottom(double newValue) => SettingsDataSource.Apperance_UI_Panels_Margin_Bottom = (int)newValue;

	private void SetSoundSwitch(bool toggle) => SettingsDataSource.SoundMainSwitch = toggle;

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

	private void OnQuitPressed() {
		PanelsStack.PopPanel(this);
	}
}
