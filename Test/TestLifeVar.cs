using System.Linq;
using Godot;
using OursGalaxy.ResourceConfig;
using OursGalaxy.UI;
using OursGalaxy.ViewModel;
namespace OursGalaxy;

internal partial class TestLifeVar : Control {
	[Export] Button here { get; set; } = null!;
	[Export] Storehouse Storehouse { get; set; } = null!;
	// // test Resourcelib
	// public override void _Ready() {
	// 	var shipData = new SpaceshipData(SpaceshipConfig.TEMPLATE_NAMECODE, ItemBaseConfig.QulityEnum.White);
	// 	var turretData = new TurretData("Equip/Turret/Photon", "Template", ItemBaseConfig.QulityEnum.White);
	// 	var mainRp = new SubRepository();
	// 	mainRp.Add(shipData);
	// 	mainRp.Add(turretData);
	// 	var otherRp = new SubRepository();

	// 	shipData.EquipsLs.Add("L_0", turretData.MakeReference());
	// 	GD.Print(turretData.IsOccupied());

	// 	try {
	// 		mainRp.Move(turretData, otherRp);
	// 	}
	// 	catch (Exception ex) {
	// 		GD.Print("Caught\t" + ex.Message);
	// 	}

	// 	var equipReference = shipData.EquipsLs["L_0"];
	// 	shipData.EquipsLs.Remove("L_0");
	// 	turretData.RecycleReference(equipReference);

	// 	mainRp.Move(turretData, otherRp);

	// 	PrintRepostory(mainRp);
	// 	PrintRepostory(otherRp);
	// }

	// private void PrintRepostory(SubRepository repository) {
	// 	GD.Print("{");
	// 	GD.Print("\t[" + string.Join(", ", repository.Ships) + "]");
	// 	GD.Print("\t[" + string.Join(", ", repository.Equips) + "]");
	// 	GD.Print("}");
	// }

	public override void _Ready() {
		base._Ready();
		GD.Print(Storehouse.IsNodeReady());
		Storehouse.VM.Add(new("WhiteCup", ItemBaseConfig.QulityEnum.White, 3));
		Storehouse.VM.Add(new("BlueCup", ItemBaseConfig.QulityEnum.Green, 4));
		Storehouse.VM.Add(new("GoldCup", ItemBaseConfig.QulityEnum.Gold, 5));

		here.Pressed += () => Storehouse.VM.GetGameItems(item => item.NameCode == "WhiteCup").First().Num -= 1;

		var config1 = ResourceLoader.Load<ItemBaseConfig>("res://ResourceLib/GameItem/GoldCup_3.tres");
		var config2 = ResourceLoader.Load<ItemBaseConfig>("res://ResourceLib/GameItem/GoldCup_3.tres");
		GD.Print(ReferenceEquals(config1, config2), config1.Equals(config2));
	}
}
