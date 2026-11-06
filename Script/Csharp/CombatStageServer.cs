using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Godot;
using OursGalaxy.UI;
namespace OursGalaxy;

/// <summary>
/// 战斗台服务
/// </summary>
internal partial class CombatStageServer {
	/// <summary>
	/// 存在战斗台
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public static bool Contains(string name) => stages.Keys.Contains(name);

	/// <summary>
	/// 注册战斗台
	/// </summary>
	/// <param name="name"></param>
	/// <param name="stage"></param>
	public static void Register(string name, CombatStage stage) {
		if (!Contains(name)) {
			stages.Add(name, stage);
			return;
		}
		GD.PushError($"<CombatStageServer> Try to register combat stage for one more times: {name}");
	}

	/// <summary>
	/// 取得统计驱动器件
	/// </summary>
	/// <param name="stageName"></param>
	/// <param name="result"></param>
	/// <returns></returns>
	public static bool TryGetStaticsSpawner(string stageName, [NotNullWhen(true)] out StaticsData? result) {
		if (Contains(stageName)) {
			result = stages[stageName].Statics;
			return true;
		}
		GD.PushError($"Try to get statics spawner from a non existed stage: {stageName}");
		result = null;
		return false;
	}

	// /// <summary>
	// /// 把战斗单位缓存到关联战斗台
	// /// </summary>
	// /// <param name="shipGUID"></param>
	// /// <param name="stageName"></param>
	// public static void CacheInStage(GUID shipGUID, string stageName) => shipsStagesCache.Add(shipGUID, stageName);

	/// <summary>
	/// 探测飞船
	/// </summary>
	/// <param name="fromShip"></param>
	/// <param name="filterTeam"></param>
	/// <returns></returns>
	public static IEnumerable<IBattleEntity> ProbeShips(GUID fromShip, CombatStage.TeamEnum filterTeam) {
		foreach (var stage in stages.Values) {
			if (stage.GetShips().FirstOrDefault(ship => ship.ID == fromShip) is not null) {
				return stage.GetShips().Where(ship => ship.Team != filterTeam);
			}
		}
		return [];
	}

	// /// <summary>
	// /// 取消缓存关系
	// /// </summary>
	// /// <param name="shipGUID"></param>
	// public static void Decached(GUID shipGUID) => shipsStagesCache.Remove(shipGUID);

	public static void Free(string name) => stages.Remove(name);

	private static Dictionary<string, CombatStage> stages = [];
}
