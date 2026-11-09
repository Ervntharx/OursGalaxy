using Godot;
using OursGalaxy.ResourceConfig;
using System;
namespace OursGalaxy.UI;

internal partial class ShipConfigOutletBox : Button {
	public static ShipConfigOutletBox Load() => (ShipConfigOutletBox)ResourceLoader.Load<PackedScene>("res://Scene/ShipConfigOutletBox.tscn").Instantiate();

	[Export] private MarginContainer EquipContainer { get; set; } = null!;
	[Export] private RichTextLabel OutletLabel { get; set; } = null!;

	public void Make(OutletSizeType sizeType) {
		OutletLabel.Text = sizeType.ToString();
	}

	public void Install(EquipData equip) {
		var equipUI = ShipConfigEquipBox.Load();
		equipUI.Make(equip);
		EquipContainer.AddChild(equipUI);
	}
}
