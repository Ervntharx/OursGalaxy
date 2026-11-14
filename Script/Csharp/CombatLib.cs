using System.IO;
using Godot;
namespace OursGalaxy.ResourceConfig;

internal static class CombatLib {
	public static CombatConfig GetConfig(CombatData data) {
		var path = GetResourcePath(data.NameCode);
		if (ResourceLoader.Exists(path)) {
			return ResourceLoader.Load<CombatConfig>(path);
		}
		throw new FileNotFoundException($"<CombatLib> {path} not found");
	}

	private static string GetResourcePath(string name) => $"res://ResourceLib/Combat/{name}.tres";
}
