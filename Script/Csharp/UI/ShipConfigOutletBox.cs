using Godot;
using OursGalaxy.ResourceConfig;
using System;
namespace OursGalaxy.UI;

internal partial class ShipConfigOutletBox : Button {
	public static ShipConfigOutletBox Load() => (ShipConfigOutletBox)ResourceLoader.Load<PackedScene>("res://Scene/ShipConfigOutletBox.tscn").Instantiate();

	[Export] private MarginContainer EquipContainer { get; set; } = null!;
	[Export] private RichTextLabel OutletLabel { get; set; } = null!;
	[Export] private Button InstallButton { get; set; } = null!;

	public string OutletName { get; private set; } = string.Empty;

	/// <summary>
	/// 渲染Outlet
	/// </summary>
	/// <param name="outletName"></param>
	/// <param name="sizeType"></param>
	/// <param name="equip"></param>
	public void Render(string outletName, OutletSizeType sizeType, EquipData? equip = null) {
		OutletLabel.Text = $"{outletName}\t{sizeType.ToString()}";
		OutletName = outletName;
		RenderEquip(equip);
	}

	/// <summary>
	/// 只渲染Equip
	/// </summary>
	/// <param name="equip"></param>
	public void RenderEquip(EquipData? equip = null) {
		EquipContainer.GetNode(EQUIP_BOX_NODE_NAME).QueueFree();
		if (equip is not null) {
			var equipUI = ShipConfigEquipBox.Load();
			equipUI.Name = EQUIP_BOX_NODE_NAME;
			equipUI.Render(vm!, OutletName, equip);
			EquipContainer.AddChild(equipUI, true);
		}
	}


	public override void _EnterTree() {
		base._EnterTree();
		InstallButton.Pressed += OnInstallPressed;
	}

	public override void _ExitTree() {
		InstallButton.Pressed -= OnInstallPressed;
		base._ExitTree();
	}

	private void OnInstallPressed() {
		vm?.PushOutletIntoPending(OutletName);
	}

	private const string EQUIP_BOX_NODE_NAME = "EquipBox";

	private ShipConfigViewModel? vm;
}
