using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 统计服务器。统计Combat中Fleet的HP，ShipsCount,Ship的HP
/// </summary>
[GlobalClass]
internal partial class StaticsServer : Node {
	public void TrackShip(Spaceship ship) {
		if (ShipsStatics.FirstOrDefault(shipstatics => shipstatics.ID == ship.ID) is null) {
			ShipsStatics.Add(new(ship.Save()));
		}
		else {
			GD.PushWarning($"StaticsServer has already tracked ship {ship.ID}");
		}
	}

	private HashSet<CombatSpaceshipStaticsData> ShipsStatics { get; set; } = [];

	private void OnShipDeath(SpaceshipSnapshot shipShot) {

	}
}
