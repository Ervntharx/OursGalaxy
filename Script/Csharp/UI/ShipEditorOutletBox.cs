using Godot;
using OursGalaxy.ResourceConfig;
using OursGalaxy.ViewModel;
namespace OursGalaxy.UI;

/// <summary>
/// Spaceship装备插槽UI,同时具有安装了装备和没有安装装备两种状态
/// </summary>
internal partial class ShipEditorOutletBox : PanelContainer {
	public static ShipEditorOutletBox Load() => (ShipEditorOutletBox)ResourceLoader.Load<PackedScene>("res://Scene/ShipEditorOutletBox.tscn").Instantiate();

	[Export] private RichTextLabel OutletLabel { get; set; } = null!;
	[Export] private Button InstallButton { get; set; } = null!;
	[Export] private TextureRect EquipTextureRect { get; set; } = null!;
	[Export] private RichTextLabel EquipNameLabel { get; set; } = null!;
	[Export] private Button ChangeButton { get; set; } = null!;
	[Export] private PanelContainer EquipPanel { get; set; } = null!;
	[Export] private Button UninstallButtonToggleButton { get; set; } = null!;
	[Export] private Button UninstallButton { get; set; } = null!;

	public string OutletName { get; private set; } = string.Empty;
	public EquipData? Equip { get; private set; }

	/// <summary>
	/// 渲染Outlet
	/// </summary>
	public void Render(SpaceshipEditorVM vm, string outletName, OutletSizeType sizeType, EquipData? equip) {
		this.vm = vm;
		OutletName = outletName;
		Equip = equip;
		OutletLabel.Text = $"{outletName}\t{sizeType}";
		InstallButton.Visible = equip is null;
		EquipPanel.Visible = equip is not null;
		if (equip is not null) {
			var config = ResourceLib.GetConfig<EquipConfig>(equip);
			EquipTextureRect.Texture = config.Texture;
			EquipNameLabel.Text = config.DisplayName;
		}
		else {
			EquipTextureRect.Texture = null;
			EquipNameLabel.Text = string.Empty;
		}
	}

	public override void _EnterTree() {
		base._EnterTree();
		InstallButton.Pressed += OnSelectOutletPressed;
		ChangeButton.Pressed += OnChangePressed;
		UninstallButtonToggleButton.Pressed += OnUninstallTogglePressed;
		UninstallButton.Pressed += OnUninstallPressed;
	}

	public override void _ExitTree() {
		InstallButton.Pressed -= OnSelectOutletPressed;
		ChangeButton.Pressed -= OnChangePressed;
		UninstallButtonToggleButton.Pressed -= OnUninstallTogglePressed;
		UninstallButton.Pressed -= OnUninstallPressed;
		base._ExitTree();
	}

	private void OnSelectOutletPressed() {
		if (vm is null) {
			GD.PushError("<ShipEditorOutletBox> VM is null");
			return;
		}
		vm.BeginInstall(OutletName);
	}

	private void OnChangePressed() {
		if (vm is null) {
			GD.PushError("<ShipEditorOutletBox> VM is null");
			return;
		}
		// Change只记录待更换状态, 旧装备保持安装. 选中新装备后由VM决定先卸载.
		vm.BeginChange(OutletName);
	}

	private void OnUninstallTogglePressed() => UninstallButton.Visible = !UninstallButton.Visible;

	private void OnUninstallPressed() {
		if (vm is null || Equip is null) {
			GD.PushError("<ShipEditorOutletBox> VM or Equip is null");
			return;
		}
		vm.UninstallEquip(Equip);
	}

	private SpaceshipEditorVM? vm;
}
