using System.Collections.Generic;
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
		// var ls = new List<GameItem>();
		// var a = new GameItem("WhiteCup", ItemBaseConfig.QulityEnum.White, 2);
		// var b = new GameItem("WhiteCup", ItemBaseConfig.QulityEnum.White, 50);
		// GD.Print(a.Equals(b));
		// ls.Add(a);
		// GD.Print(ls.Contains(b));
		base._Ready();
		GlobalDataSource.Source.GameItemsAppend(new("WhiteCup", ItemBaseConfig.QulityEnum.White, 3));
		GlobalDataSource.Source.GameItemsAppend(new("BlueCup", ItemBaseConfig.QulityEnum.Green, 4));
		GlobalDataSource.Source.GameItemsAppend(new("GoldCup", ItemBaseConfig.QulityEnum.Gold, 5));

		here.Pressed += () => GlobalDataSource.Source.GameItemsChangeNum(GlobalDataSource.Source.GetGameItems(it => it.NameCode == "WhiteCup").First(), -1);
	}
}
