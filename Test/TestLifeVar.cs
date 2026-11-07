using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

internal partial class TestLifeVar : Control {
	// test Resourcelib
	public override void _Ready() {
		var LocalRepository = new SubRepository();
		var ship = new SpaceshipData(SpaceshipConfig.TEMPLATE_NAMECODE, ItemBaseConfig.QulityEnum.White);
		LocalRepository.Ships.Add(ship);
		var refer = ship.MakeReference();
		GD.Print(ship.IsOccupied());
		GD.Print(SpaceshipLib.GetConfig(ship).HP);

		var equip = new TurretData("Equip/Turret/Photon", "Template", ItemBaseConfig.QulityEnum.White);
		var referEq = equip.MakeReference();
		GD.Print(equip.IsOccupied());
		GD.Print(TurretLib.GetConfig(equip).Range);
	}
}
