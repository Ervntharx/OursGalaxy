using System;
using System.Collections.ObjectModel;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

internal partial class TestLifeVar : Control {
	// test Resourcelib
	public override void _Ready() {
		var shipData = new SpaceshipData(SpaceshipConfig.TEMPLATE_NAMECODE, ItemBaseConfig.QulityEnum.White);
		var turretData = new TurretData("Equip/Turret/Photon", "Template", ItemBaseConfig.QulityEnum.White);
		var mainRp = new SubRepository() {
			Ships = new ReadOnlyCollection<SpaceshipData>([shipData]),
			Equips = new ReadOnlyCollection<EquipData>([turretData])
		};
		var otherRp = new SubRepository() { Ships = [], Equips = [] };

		shipData.EquipsLs.Add("L_0", turretData.MakeReference());
		GD.Print(turretData.IsOccupied());

		try {
			mainRp.Move(turretData, otherRp);
		}
		catch (Exception ex) {
			GD.Print("Caught\t" + ex.Message);
		}

		var equipReference = shipData.EquipsLs["L_0"];
		shipData.EquipsLs.Remove("L_0");
		turretData.RecycleReference(equipReference);

		mainRp.Move(turretData, otherRp);

		PrintRepostory(mainRp);
		PrintRepostory(otherRp);
	}

	private void PrintRepostory(SubRepository repository) {
		GD.Print("{");
		GD.Print("\t[" + string.Join(", ", repository.Ships) + "]");
		GD.Print("\t[" + string.Join(", ", repository.Equips) + "]");
		GD.Print("}");
	}
}
