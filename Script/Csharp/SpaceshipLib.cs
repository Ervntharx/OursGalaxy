using Godot;
namespace OursGalaxy.ResourceConfig;

/// <summary>
/// 舰船数据库
/// </summary>
internal static class SpaceshipLib {
	/// <summary>
	/// 从spaceshipData检查数据是否存在
	/// </summary>
	/// <param name="data"></param>
	/// <returns></returns>
	public static bool Exists(SpaceshipData data) => ResourceLib.Exists(data);

	/// <summary>
	/// 是否存在配置
	/// </summary>
	/// <param name="shipType"></param>
	/// <param name="qulity"></param>
	/// <returns></returns>
	public static bool Exists(string shipType, ItemBaseConfig.QulityEnum qulity = ItemBaseConfig.QulityEnum.White)
		=> ResourceLib.Exists(SpaceshipConfig.ITEM_CODE_PREFIX, shipType, qulity);

	/// <summary>
	/// 取得配置
	/// </summary>
	/// <param name="shipType"></param>
	/// <param name="qulity"></param>
	/// <returns></returns>
	public static SpaceshipConfig GetConfig(string shipType, ItemBaseConfig.QulityEnum qulity)
		=> ResourceLib.GetConfig<SpaceshipConfig>(SpaceshipConfig.ITEM_CODE_PREFIX, shipType, qulity);

	/// <summary>
	/// 取得配置
	/// </summary>
	/// <param name="data"></param>
	/// <returns></returns>
	public static SpaceshipConfig GetConfig(SpaceshipData data)
		=> ResourceLib.GetConfig<SpaceshipConfig>(data);


	/// <summary>
	/// 取得模板船的配置
	/// </summary>
	/// <returns></returns>
	public static SpaceshipConfig GetTemplate() => GetConfig(SpaceshipConfig.TEMPLATE_NAMECODE, ItemBaseConfig.QulityEnum.White);
}
