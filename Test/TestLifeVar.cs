using System.Collections.Generic;
using System.Linq;
using Godot;
using OursGalaxy.ResourceConfig;
using OursGalaxy.UI;
using OursGalaxy.ViewModel;
namespace OursGalaxy;

internal partial class TestLifeVar : Control {
	[Export] Button reduceA { get; set; } = null!;
	[Export] Button increaseA { get; set; } = null!;
	[Export] Storehouse StorehouseA { get; set; } = null!;
	[Export] Storehouse StorehouseB { get; set; } = null!;
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
		var w = new GameItem("WhiteCup", ItemBaseConfig.QulityEnum.White, 2);
		var b = new GameItem("BlueCup", ItemBaseConfig.QulityEnum.Green, 1);
		var g = new GameItem("GoldCup", ItemBaseConfig.QulityEnum.Gold, 1);
		GlobalDataSource.Source.GameItemsAppend(w);
		GlobalDataSource.Source.GameItemsAppend(b);
		GlobalDataSource.Source.GameItemsAppend(g);

		increaseA.Pressed += () => GlobalDataSource.Source.GameItemsChangeNum(w, 2);
		reduceA.Pressed += () => GlobalDataSource.Source.GameItemsChangeNum(w, -1);
	}
}
