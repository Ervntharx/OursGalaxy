using Godot;
using OursGalaxy.ResourceConfig;
using OursGalaxy.ViewModel;
using System.Linq;
namespace OursGalaxy.UI;

/// <summary>
/// 飞船装配编辑面板
/// </summary>
internal partial class ShipEditorPanel : MarginContainer {
	public static ShipEditorPanel Load() => (ShipEditorPanel)ResourceLoader.Load<PackedScene>("res://Scene/ShipEditorPanel.tscn").Instantiate();

	private const float PREVIEW_ROTATE_SPEED = 0.01f;

	[Export] private Button QuitButton { get; set; } = null!;
	[Export] private SubViewport ShipOverview { get; set; } = null!;
	[Export] private Node3D ShipStage { get; set; } = null!;
	[Export] private Node3D ShipPreview { get; set; } = null!;
	[Export] private SubViewportContainer ShipPreviewContainer { get; set; } = null!;
	[Export] private VBoxContainer InstalledEquipsPanel { get; set; } = null!;

	[Export] private TabContainer EquipsTabs { get; set; } = null!;
	[Export] private Button CancelPickButton { get; set; } = null!;
	[Export] private VBoxContainer PickEquipsPanel { get; set; } = null!;

	[Export] public SpaceshipEditorVM VM { get; set; } = null!;

	public void Render(SpaceshipReference shipReference, SubRepository subRepository) => Render(subRepository[shipReference], subRepository);

	public void Render(SpaceshipData shipData, SubRepository subRepository) {
		VM.Load(subRepository, shipData);
	}

	public override void _EnterTree() {
		base._EnterTree();
		QuitButton.Pressed += OnQuitPressed;
		CancelPickButton.Pressed += OnCancelPickPressed;
		VM.SelectedOutletChangedEvent += OnSelectedOutletChanged;
		GlobalDataSource.Source.ShipInstallEquipEvent += OnShipInstallEquip;
		GlobalDataSource.Source.ShipUninstallEquipEvent += OnShipUninstallEquip;
		GlobalDataSource.Source.RepositoryAddEquipEvent += OnRepositoryAddEquip;
		GlobalDataSource.Source.RepositoryRemoveEquipEvent += OnRepositoryRemoveEquip;

		// Render约定在入树前调用, 这里全量初始化后只监听事件做局部刷新
		BuildOutlets();
		RefreshPickEquips(VM.SelectedOutlet);
		BuildPreview();
	}

	public override void _ExitTree() {
		QuitButton.Pressed -= OnQuitPressed;
		CancelPickButton.Pressed -= OnCancelPickPressed;
		VM.SelectedOutletChangedEvent -= OnSelectedOutletChanged;
		GlobalDataSource.Source.ShipInstallEquipEvent -= OnShipInstallEquip;
		GlobalDataSource.Source.ShipUninstallEquipEvent -= OnShipUninstallEquip;
		GlobalDataSource.Source.RepositoryAddEquipEvent -= OnRepositoryAddEquip;
		GlobalDataSource.Source.RepositoryRemoveEquipEvent -= OnRepositoryRemoveEquip;

		ClearChildren(ShipPreview);
		previewShip = null;
		base._ExitTree();
	}

	public override void _Input(InputEvent @event) {
		if (!Visible) {
			return;
		}

		if (@event is InputEventMouseButton mouseButton && mouseButton.ButtonIndex == MouseButton.Left && mouseButton.Pressed) {
			if (ShipPreviewContainer.GetGlobalRect().HasPoint(mouseButton.Position)) {
				GetViewport().SetInputAsHandled();
			}
			return;
		}

		if (@event is InputEventMouseMotion mouseMotion
			&& ((int)mouseMotion.ButtonMask & (int)MouseButtonMask.Left) != 0
			&& ShipPreviewContainer.GetGlobalRect().HasPoint(mouseMotion.Position)) {
			var rotation = ShipPreview.Rotation;
			rotation.Y -= mouseMotion.Relative.X * PREVIEW_ROTATE_SPEED;
			rotation.X = Mathf.Clamp(rotation.X - mouseMotion.Relative.Y * PREVIEW_ROTATE_SPEED, -Mathf.Pi / 2, Mathf.Pi / 2);
			ShipPreview.Rotation = rotation;
			GetViewport().SetInputAsHandled();
		}
	}

	private void OnQuitPressed() => PanelsStack.PopPanel(this);

	private void OnCancelPickPressed() => VM.CancelSelection();

	private void OnSelectedOutletChanged(string? outletName) => RefreshPickEquips(outletName);

	private void OnShipInstallEquip(SpaceshipData ship, string outletName, EquipData equip) {
		RemovePickCandidate(equip);
		if (VM.EditedShip is null || !VM.EditedShip.Equals(ship)) {
			return;
		}

		UpdateOutletBox(outletName, equip);
		if (previewShip is not null && GodotObject.IsInstanceValid(previewShip)) {
			if (previewShip.OutletHasInstall(outletName)) {
				previewShip.UninstallEquip(outletName);
			}
			previewShip.InstallEquip(outletName, equip);
		}
	}

	private void OnShipUninstallEquip(SpaceshipData ship, string outletName, EquipData equip) {
		AddPickCandidate(equip);
		if (VM.EditedShip is null || !VM.EditedShip.Equals(ship)) {
			return;
		}

		UpdateOutletBox(outletName, null);
		if (previewShip is not null && GodotObject.IsInstanceValid(previewShip) && previewShip.OutletHasInstall(outletName)) {
			previewShip.UninstallEquip(outletName);
		}
	}

	private void OnRepositoryAddEquip(EquipData equip) => AddPickCandidate(equip);

	private void OnRepositoryRemoveEquip(EquipData equip) => RemovePickCandidate(equip);

	private void BuildOutlets() {
		ClearChildren(InstalledEquipsPanel);
		if (VM.EditedShip is null || VM.LocalRepository is null) {
			return;
		}

		var shipConfig = SpaceshipLib.GetConfig(VM.EditedShip);
		foreach (var outletSet in shipConfig.OutletSet) {
			var equip = VM.EditedShip.EquipsLs.ContainsKey(outletSet.Key)
				? VM.LocalRepository[VM.EditedShip.EquipsLs[outletSet.Key]]
				: null;
			var outletUI = ShipEditorOutletBox.Load();
			outletUI.Render(VM, outletSet.Key, outletSet.Value, equip);
			InstalledEquipsPanel.AddChild(outletUI);
		}
	}

	private void UpdateOutletBox(string outletName, EquipData? equip) {
		var box = FindOutletBox(outletName);
		if (box is null) {
			GD.PushError($"<ShipEditorPanel> No such outlet box: {outletName}");
			return;
		}
		var shipConfig = SpaceshipLib.GetConfig(VM.EditedShip!);
		if (!shipConfig.OutletSet.ContainsKey(outletName)) {
			return;
		}
		box.Render(VM, outletName, shipConfig.OutletSet[outletName], equip);
	}

	private ShipEditorOutletBox? FindOutletBox(string outletName) => InstalledEquipsPanel.GetChildren().OfType<ShipEditorOutletBox>()
			.FirstOrDefault(box => box.OutletName == outletName);

	private void RefreshPickEquips(string? outletName) {
		CancelPickButton.Text = VM.IsChangingEquip ? "取消更换" : "取消";
		ClearChildren(PickEquipsPanel);
		if (outletName is null || VM.EditedShip is null || VM.LocalRepository is null) {
			// EquipsTabs.TabsVisible = false;
			EquipsTabs.CurrentTab = 0;
			return;
		}

		var shipConfig = SpaceshipLib.GetConfig(VM.EditedShip);
		if (!shipConfig.OutletSet.ContainsKey(outletName)) {
			GD.PushError($"<ShipEditorPanel> No such outlet: {outletName}");
			// EquipsTabs.TabsVisible = false;
			EquipsTabs.CurrentTab = 0;
			return;
		}

		foreach (var equip in GlobalDataSource.Source.GetInstallableEquips(shipConfig.OutletSet[outletName])) {
			var equipUI = PickEquipBox.Load();
			equipUI.Render(VM, equip);
			PickEquipsPanel.AddChild(equipUI);
		}
		// EquipsTabs.TabsVisible = true;
		EquipsTabs.CurrentTab = 1;
	}

	private void AddPickCandidate(EquipData equip) {
		if (VM.SelectedOutlet is null || VM.EditedShip is null) {
			return;
		}
		if (equip.IsOccupied()) {
			return;
		}

		var shipConfig = SpaceshipLib.GetConfig(VM.EditedShip);
		if (!shipConfig.OutletSet.ContainsKey(VM.SelectedOutlet)) {
			return;
		}
		if (!ResourceLib.GetConfig<EquipConfig>(equip).SizeType.CanFitTo(shipConfig.OutletSet[VM.SelectedOutlet])) {
			return;
		}
		if (FindPickBox(equip.ID) is not null) {
			return;
		}

		var equipUI = PickEquipBox.Load();
		equipUI.Render(VM, equip);
		PickEquipsPanel.AddChild(equipUI);
	}

	private void RemovePickCandidate(EquipData equip) {
		var box = FindPickBox(equip.ID);
		if (box is null) {
			return;
		}
		PickEquipsPanel.RemoveChild(box);
		box.QueueFree();
	}

	private PickEquipBox? FindPickBox(GUID id) => PickEquipsPanel.GetChildren().OfType<PickEquipBox>()
			.FirstOrDefault(box => box.Equip is not null && box.Equip.ID == id);

	private void BuildPreview() {
		ClearChildren(ShipPreview);
		previewShip = null;
		if (VM.EditedShip is null || VM.LocalRepository is null) {
			return;
		}

		previewShip = SpaceshipFactory.Make(VM.EditedShip, VM.LocalRepository);
		previewShip.BaseState = Spaceship.BaseStateEnum.Display;
		ShipPreview.AddChild(previewShip);
	}

	private static void ClearChildren(Node parent) {
		foreach (var child in parent.GetChildren()) {
			parent.RemoveChild(child);
			child.QueueFree();
		}
	}

	private Spaceship? previewShip;
}
