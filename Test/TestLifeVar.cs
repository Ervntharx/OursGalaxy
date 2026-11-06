using Godot;
using OursGalaxy;
using OursGalaxy.ResourceConfig;
using System;
namespace OursGalaxy;

internal partial class TestLifeVar : Control {
	// test Resourcelib
	public override void _Ready() {
		var item = new GameItem("Template", ResourceConfig.ItemBaseConfig.QulityEnum.White, 2);
		GD.Print("Gameitem DisplayName:\t", ResourceLib.GetConfig(item).DisplayName);

		var photon = new TurretData("Equip/Turret/Photon", "Template", ItemBaseConfig.QulityEnum.White);
		var equip = EquipFactory.Make(photon);
		GD.Print(equip.Name);
		// GD.Print(TurretLib.GetConfig<PhotonConfig>(photon).Range);

		var ship = new SpaceshipData(SpaceshipConfig.TEMPLATE_NAMECODE, ItemBaseConfig.QulityEnum.White);
		GD.Print(SpaceshipLib.GetConfig(ship).HP);

		GD.Print("break test");

		GD.Print(ResourceLib.GetConfig<PhotonConfig>(photon).Range);
		GD.Print(ResourceLib.GetConfig<SpaceshipConfig>(ship).HP);
	}
}
