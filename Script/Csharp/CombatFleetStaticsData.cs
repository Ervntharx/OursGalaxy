using Godot;
using Godot.Collections;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

[GlobalClass]
internal partial class FleetStaticsData : Resource {
	[Export]
	public GUID FleetID { get; set; }

	[Export]
	public string FleetName { get; set; }

	[Export]
	public int HP { get; set; }

	[Export]
	public int LeftHP { get; set; }

	[Export]
	public Dictionary<SpaceshipConfig.ScaleSizeEnum, int> ShipsCount { get; set; } = [];

	[Export]
	public Dictionary<SpaceshipConfig.ScaleSizeEnum, int> LeftShipsCount { get; set; } = [];

	[Export]
	public Array<CombatSpaceshipStaticsData> ShipsStatics { get; set; } = [];

	public FleetStaticsData(FleetData fleet) {
		FleetID = fleet.ID;
		FleetName = fleet.Name;

		foreach (var ship in fleet.Ships.Keys) {
			var config = SpaceshipLib.QueryConfig(ship);
			HP += config.HP;
			if (ShipsCount.ContainsKey(config.ScaleSize)) {
				ShipsCount.Add(config.ScaleSize, 1);
			}
			else {
				ShipsCount[config.ScaleSize] += 1;
			}
		}
	}

}
