using Godot;
using OursGalaxy.ResourceConfig;
using System;
using System.Linq;
namespace OursGalaxy.UI;

/// <summary>
/// 飞船装配编辑面板
/// </summary>
internal partial class ShipConfigPanel : MarginContainer {
	public static ShipConfigPanel Load() => (ShipConfigPanel)ResourceLoader.Load<PackedScene>("res://Scene/ShipConfigPanel.tscn").Instantiate();

	[Export] private Button QuitButton { get; set; } = null!;
	[Export] private SubViewport ShipOverview { get; set; } = null!;
	[Export] private Node3D ShipStage { get; set; } = null!;
	[Export] private VBoxContainer InstalledEquipsPanel { get; set; } = null!;

	[Export] private TabContainer EquipsTabs { get; set; } = null!;
	[Export] private VBoxContainer PickEquipsPanel { get; set; } = null!;

	public ShipConfigViewModel ViewModel { get; }

	public ShipConfigPanel() {
		ViewModel = new(this);
	}

	public void Render(SpaceshipReference ship, SubRepository subRepository) {
		var shipData = subRepository.GetSpaceshipData(ship);
		var shipConfig = SpaceshipLib.GetConfig(shipData);
		foreach (var outletSet in shipConfig.OutletSet) {
			var outletUI = ShipConfigOutletBox.Load();
			outletUI.Render(outletSet.Key, outletSet.Value, shipData.EquipsLs.ContainsKey(outletSet.Key) ? subRepository.GetEquipData(shipData.EquipsLs[outletSet.Key]) : null);
		}

		ViewModel.Load(subRepository, ship);
	}


	// Called when the node enters the scene tree for the first time.
	public override void _EnterTree() {
		base._EnterTree();
		QuitButton.Pressed += () => PanelsStack.PopPanel(this);
		ViewModel.PushPendingEditOutletEvent += OnPushPendingEditOutletEvent;
		ViewModel.OutletUPdatedEvent += OnOutletUpdatedEvent;
	}

	public override void _ExitTree() {
		ViewModel.PushPendingEditOutletEvent -= OnPushPendingEditOutletEvent;
		base._ExitTree();
	}

	// 加载可装备的装备
	private void OnPushPendingEditOutletEvent(OutletSizeType size) {
		EquipsTabs.TabsVisible = true;
		foreach (var equip in GlobalViewModel.MainRepository.Equips.Where(equip => ResourceLib.GetConfig<EquipConfig>(equip).SizeType == size)) {
			var equipUI = PickEquipBox.Load();
			equipUI.Render(equip, ViewModel);
			PickEquipsPanel.AddChild(equipUI);
		}
	}

	// 安装完成后UI刷新.
	private void OnOutletUpdatedEvent(string outletName, EquipReference? equipReference) {
		EquipsTabs.TabsVisible = false;
		// 清理PickEquips中的装备UI,下次整体重新绘制
		foreach (var box in PickEquipsPanel.GetChildren()) {
			box.QueueFree();
		}
	}
}
