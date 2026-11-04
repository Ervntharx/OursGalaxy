using System.Collections.Generic;
using System.Linq;
using Godot;
using OursGalaxy.UI;
namespace OursGalaxy;

internal partial class CombatStageServer {
	public static bool Contains(string name) => stages.Keys.Contains(name);

	public static void Register(string name, CombatStage stage) {
		if (!Contains(name)) {
			stages.Add(name, stage);
			return;
		}
		GD.PushError($"<CombatStageServer> Try to register combat stage for one more times: {name}");
	}

	public static void Connect(GUID shipGUID, string stageName) => shipsStagesCache.Add(shipGUID, stageName);

	public static IEnumerable<IBattleEntity> ProbeShips(GUID fromShip, CombatStage.TeamEnum filterTeam) {
		if (shipsStagesCache.Keys.Contains(fromShip)) {
			return stages[shipsStagesCache[fromShip]].GetChildren().OfType<IBattleEntity>().Where(ship => ship.Team != filterTeam);
		}
		foreach (var stage in stages.Values) {
			if (stage.GetChildren().OfType<Spaceship>().FirstOrDefault(ship => ship.ID == fromShip) is not null) {
				return stage.GetChildren().OfType<IBattleEntity>().Where(ship => ship.Team != filterTeam);
			}
		}
		return [];
	}

	public static void Disconnect(GUID shipGUID) => shipsStagesCache.Remove(shipGUID);

	public static void Free(string name) => stages.Remove(name);

	private static Dictionary<string, CombatStage> stages = [];
	private static Dictionary<GUID, string> shipsStagesCache = [];
}
