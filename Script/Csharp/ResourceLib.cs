using System;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 资源数据库。查询逻辑是在ResourceLib/{ItemBase,ItemType}/{ItemBase.ItemName}_{ItemBaseConfig.QulityEnum}.tres
/// </summary>
internal static class ResourceLib {
	/// <summary>
	/// 从ItemBase检查配置是否存在
	/// </summary>
	/// <param name="itemBase"></param>
	/// <returns></returns>
	public static bool Exists(ItemBase itemBase) => Exists(itemBase.TypeCode, itemBase.NameCode, itemBase.Qulity);

	/// <summary>
	/// 检查配置是否存在
	/// </summary>
	/// <param name="itemTypeCode"></param>
	/// <param name="itemNameCode"></param>
	/// <param name="qulity"></param>
	/// <returns></returns>
	public static bool Exists(string itemTypeCode, string itemNameCode, ItemBaseConfig.QulityEnum qulity)
		=> ResourceLoader.Exists(GetConfigPath(itemTypeCode, itemNameCode, qulity));

	/// <summary>
	/// 按ItemBaseConfig取得配置
	/// </summary>
	/// <param name="itemBase"></param>
	/// <returns></returns>
	public static ItemBaseConfig GetConfig(ItemBase itemBase) => GetConfig<ItemBaseConfig>(itemBase);

	/// <summary>
	/// 取得配置
	/// </summary>
	/// <param name="itemTypeCode"></param>
	/// <param name="itemNameCode"></param>
	/// <param name="qulity"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentException"></exception>
	public static ItemBaseConfig GetConfig(string itemTypeCode, string itemNameCode, ItemBaseConfig.QulityEnum qulity)
		=> GetConfig<ItemBaseConfig>(itemTypeCode, itemNameCode, qulity);

	/// <summary>
	/// 按ItemBase直接取得数据,泛型版本
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="itemBase"></param>
	/// <returns></returns>
	public static T GetConfig<T>(ItemBase itemBase) where T : ItemBaseConfig => GetConfig<T>(itemBase.TypeCode, itemBase.NameCode, itemBase.Qulity);

	/// <summary>
	/// 取得配置,泛型版本
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="itemTypeCode"></param>
	/// <param name="itemNameCode"></param>
	/// <param name="qulity"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentException"></exception>
	public static T GetConfig<T>(string itemTypeCode, string itemNameCode, ItemBaseConfig.QulityEnum qulity) where T : ItemBaseConfig {
		if (Exists(itemTypeCode, itemNameCode, qulity)) {
			return ResourceLoader.Load<T>(GetConfigPath(itemTypeCode, itemNameCode, qulity));
		}
		throw new ArgumentException($"No such config :{itemTypeCode}_{itemNameCode}_{(int)qulity}");
	}

	private static string GetConfigPath(string itemTypeCode, string itemNameCode, ItemBaseConfig.QulityEnum qulity)
		=> $"res://ResourceLib/{itemTypeCode}/{itemNameCode}_{(int)qulity}.tres";
}
