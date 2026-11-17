using Godot;
using OursGalaxy.ResourceConfig;
using OursGalaxy.ViewModel;
using System;
using System.Linq;
namespace OursGalaxy.UI;

/// <summary>
/// 飞船装配编辑面板
/// </summary>
internal partial class ShipEditorPanel : MarginContainer {
	public static ShipEditorPanel Load() => (ShipEditorPanel)ResourceLoader.Load<PackedScene>("res://Scene/ShipEditorPanel.tscn").Instantiate();

	[Export] private Button QuitButton { get; set; } = null!;
	[Export] private SubViewport ShipOverview { get; set; } = null!;
	[Export] private Node3D ShipStage { get; set; } = null!;
	[Export] private VBoxContainer InstalledEquipsPanel { get; set; } = null!;

	[Export] private TabContainer EquipsTabs { get; set; } = null!;
	[Export] private VBoxContainer PickEquipsPanel { get; set; } = null!;

	[Export] public SpaceshipEditorVM VM { get; set; } = null!;

	public void Render(SpaceshipReference shipReference, SubRepository subRepository) {
		var shipData = subRepository[shipReference];
		var shipConfig = SpaceshipLib.GetConfig(shipData);
		foreach (var outletSet in shipConfig.OutletSet) {
			var outletUI = ShipConfigOutletBox.Load();
			outletUI.Render(outletSet.Key, outletSet.Value, shipData.EquipsLs.ContainsKey(outletSet.Key) ? subRepository[shipData.EquipsLs[outletSet.Key]] : null);
		}

		VM.LocalRepository = subRepository;
		VM.EditedShip = subRepository[shipReference];
	}


	// Called when the node enters the scene tree for the first time.
	public override void _EnterTree() {
		base._EnterTree();
		QuitButton.Pressed += () => PanelsStack.PopPanel(this);
	}

	public override void _ExitTree() {
		base._ExitTree();
	}

	// 加载可装备的装备
	private void OnPushPendingEditOutletEvent(OutletSizeType size) {
		// EquipsTabs.TabsVisible = true;
		foreach (var equip in GlobalDataSource.MainRepository.Equips.Where(equip => ResourceLib.GetConfig<EquipConfig>(equip).SizeType == size)) {
			var equipUI = PickEquipBox.Load();
			equipUI.Render(VM, equip);
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
