using System;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 资源数据库。查询逻辑是在ResourceLib/{ItemBase,ItemType}/{ItemBase.ItemName}_{ItemBaseConfig.QulityEnum}.tres
/// </summary>
internal static class ResourceLib {
	/// <summary>
	/// 查新资源配置是否存在
	/// </summary>
	/// <param name="code"></param>
	/// <returns></returns>
	public static bool Exists(string code) => ResourceLoader.Exists(GetConfigPath(code));

	public static bool Exists(string itemType, string itemName, ItemBaseConfig.QulityEnum qulity)
		=> Exists($"{itemType}_{itemName}_{qulity}");

	/// <summary>
	/// 取得配置
	/// </summary>
	/// <param name="code"></param>
	/// <returns></returns>
	public static ItemBaseConfig QueryConfig(string code) => QueryConfig<ItemBaseConfig>(code);

	public static ItemBaseConfig QueryConfig(string itemType, string itemName, ItemBaseConfig.QulityEnum qulity)
		=> QueryConfig($"{itemType}_{itemName}_{qulity}");

	/// <summary>
	/// 取得配置。泛型版本
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="code"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException"></exception>
	public static T QueryConfig<T>(string code) where T : ItemBaseConfig {
		if (Exists(GetConfigPath(code))) {
			return ResourceLoader.Load<T>(GetConfigPath(code));
		}
		throw new ArgumentNullException($"no such config: {code}");
	}

	public static T QueryConfig<T>(string itemType, string itemName, ItemBaseConfig.QulityEnum qulity) where T : ItemBaseConfig
		=> QueryConfig<T>($"{itemType}_{itemName}_{qulity}");

	private static string GetConfigPath(string code) {
		var codeLs = code.Split("_", false);
		return $"res://ResourceLib/{codeLs[0]}/{codeLs[1]}_{codeLs[2]}.tres";
	}
}
