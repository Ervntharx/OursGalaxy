using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

[GlobalClass]
internal partial class CombatSpaceshipStaticsData : Resource {
	[Export]
	public GUID ID { get; set; }
	[Export]
	public string ShipType { get; set; }
	[Export]
	public SpaceshipConfig.QulityEnum Qulity { get; set; } = ItemBaseConfig.QulityEnum.White;

	[ExportGroup("Statics Items")]
	[Export]
	public int AliveTime { get; set; }

	[Export]
	public int MakeDamage { get; set; }

	[Export]
	public int TakeDamage { get; set; }

	[Export]
	public int DestoryShipsCount { get; set; }

	[Export]
	public int InterceptMissileCount { get; set; }

	public CombatSpaceshipStaticsData(SpaceshipData shipData) {
		ID = shipData.ID;
		ShipType = shipData.GetShipTypeName();
		Qulity = shipData.GetQulity();
	}
}

