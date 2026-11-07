using System.Collections.Generic;
using System.Linq;
using Godot;

namespace OursGalaxy;

/// <summary>
/// 微型资产数据库，专门用来解决UniqueItems和Reference的数据归属问题。但资产数据是外部注入的
/// </summary>
internal static class RepositoryMap {
	public static void Resgister(SubRepository pool) {
		if (additionalPools.Contains(pool)) {
			GD.PushWarning($"<Repository> Try to register for once more: {pool.ID}");
		}
		additionalPools.Add(pool);

	}

	public static void Unregister(SubRepository pool) {
		if (!additionalPools.Remove(pool)) {
			GD.PushWarning($"<Repository> Try to unregister for once more: {pool.ID}");
		}
	}

	public static void Move(UniqueItem item, GUID toPool) {
		if (item.ItemLocation is not null) {
			GD.PushError($"<Repository> Try to move a occupied unique item {item.ToString()} (occupied by {item.ItemLocation}");
			return;
		}
		var targetPool = additionalPools.FirstOrDefault(pool => pool.ID == toPool);
		if (targetPool is null) {
			GD.PushError($"<Repository> Try to move to non existed pool {toPool}");
			return;
		}

	}

	// public static void Transfer()
	private static HashSet<SubRepository> additionalPools = [];
}
