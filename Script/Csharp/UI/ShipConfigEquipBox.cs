using System;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy.UI;

/// <summary>
/// 安装在Spaceship上的EquipBox
/// </summary>
internal partial class ShipConfigEquipBox : Button {
	public static ShipConfigEquipBox Load() => (ShipConfigEquipBox)ResourceLoader.Load<PackedScene>("res://Scene/ShipConfigPanel.tscn").Instantiate();

	[Export] private TextureRect ICONDisplayer { get; set; } = null!;
	[Export] private RichTextLabel EquipNameLabel { get; set; } = null!;
	[Export] private Button UninstallButton { get; set; } = null!;

	public string? OutletName { get; private set; }
	public EquipData? Equip { get; private set; }

	public void Render(ShipConfigViewModel VM, string outletName, EquipData data) {
		var config = ResourceLib.GetConfig(data);
		ICONDisplayer.Texture = config.Texture;
		EquipNameLabel.Text = config.DisplayName;

		Equip = data;
		OutletName = outletName;
		vm = VM;
	}

	private void OnInfPressed() {
		if (Equip is not null) {
			vm?.DisplayEquipInf(Equip);
		}
		else {
			GD.Print("<Ship Config Equip Box> Null reference of Equip");
		}
	}

	private void OnUninstallPressed() {
		if (OutletName is not null) {
			vm?.CLearOutlet(OutletName);
		}
		else {
			GD.PushError("<SHip Config Equip Box> Null outletName");
		}
	}

	public override void _EnterTree() {
		base._EnterTree();
		Pressed += OnInfPressed;
		UninstallButton.Pressed += OnUninstallPressed;
	}

	public override void _ExitTree() {
		Pressed -= OnInfPressed;
		UninstallButton.Pressed -= OnUninstallPressed;
		base._ExitTree();
	}

	private ShipConfigViewModel? vm;
}
