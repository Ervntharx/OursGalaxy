using System;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 资源数据库
/// </summary>
internal static class ResourceLib {
	public static bool Exists(string code) => ResourceLoader.Exists(GetConfigPath(code));

	public static ItemBaseConfig QueryConfig(string code) {
		if (ResourceLoader.Exists(GetConfigPath(code))) {
			return ResourceLoader.Load<ItemBaseConfig>(GetConfigPath(code));
		}
		throw new ArgumentNullException($"no such item: {code}");
	}

	private static string GetConfigPath(string code) {
		var codeLs = code.Split("_", false);
		return $"res://ResourceLib/{codeLs[0]}/{codeLs[1]}_{codeLs[2]}";
	}
}
