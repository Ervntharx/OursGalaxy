using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using OursGalaxy.UI;
namespace OursGalaxy;

/// <summary>
/// 统计服务器。统计Combat中Fleet的HP，ShipsCount,Ship的HP
/// </summary>
[GlobalClass]
[Obsolete]
internal partial class StaticsServer : Node {
	public CombatFleetStaticsData? LeftFleetStaticsData { get; set; }
	public CombatFleetStaticsData? RightFleetStaticsData { get; set; }



	[Obsolete]
	public void TrackShip(Spaceship ship) {
		if (ShipsStatics.Values.FirstOrDefault(shipstatics => shipstatics.ID == ship.ID) is null) {
			ShipsStatics.TryAdd(ship.FromFleetID, new(ship.Save()));
		}
		else {
			GD.PushWarning($"StaticsServer has already tracked ship {ship.ID}");
		}
	}

	private void OnTakeDamage(SpaceshipSnapshot attackedShot, SpaceshipSnapshot attackshot, DamageSet damage) {
		var statics = ShipsStatics.Values.FirstOrDefault(shipStatics => shipStatics.ID == attackedShot.UniqueID);
		if (statics is not null) {
			statics.TakeDamage += damage.Sum();
		}
		statics = ShipsStatics.Values.FirstOrDefault(ShipsStatics => ShipsStatics.ID == attackshot.UniqueID);
		if (statics is not null) {
			statics.MakeDamage += damage.Sum();
		}
	}

	private void OnShipDeath(SpaceshipSnapshot shipShot) {
		foreach (var shipStatics in ShipsStatics.Values) {
			if (shipStatics.ID == shipShot.UniqueID) {
				shipStatics.AliveTime = (CombatStage.Value?.CombatTotalTime - CombatStage.Value?.CombatLeftTime) ?? -1;
			}
		}
	}

	[Obsolete]
	private Godot.Collections.Dictionary<GUID, CombatSpaceshipStaticsData> ShipsStatics { get; set; } = [];
}
