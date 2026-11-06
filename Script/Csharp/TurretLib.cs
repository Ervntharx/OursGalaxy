using Godot;

namespace OursGalaxy.ResourceConfig;

internal static class TurretLib {
	/// <summary>
	/// 查询配置,按turretData
	/// </summary>
	/// <param name="turret"></param>
	/// <returns></returns>
	public static bool Exists(TurretData turret) => ResourceLib.Exists(turret);

	/// <summary>
	/// 查询配置
	/// </summary>
	/// <param name="turretType"></param>
	/// <param name="turretName"></param>
	/// <param name="qulity"></param>
	/// <returns></returns>
	public static bool Exists(string turretType, string turretName, ItemBaseConfig.QulityEnum qulity = ItemBaseConfig.QulityEnum.White)
		=> ResourceLib.Exists(turretType, turretName, qulity);

	/// <summary>
	/// 取得配置
	/// </summary>
	/// <param name="turretType"></param>
	/// <param name="turretName"></param>
	/// <param name="qulity"></param>
	/// <returns></returns>
	public static TurretConfig GetConfig(string turretType, string turretName, ItemBaseConfig.QulityEnum qulity)
		=> ResourceLib.GetConfig<TurretConfig>(turretType, turretName, qulity);

	/// <summary>
	/// 取得配置,泛型版本
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="turretTypeCode"></param>
	/// <param name="turretName"></param>
	/// <param name="qulity"></param>
	/// <returns></returns>
	public static T GetConfig<T>(string turretTypeCode, string turretName, ItemBaseConfig.QulityEnum qulity) where T : TurretConfig
		=> ResourceLib.GetConfig<T>(turretTypeCode, turretName, qulity);

	/// <summary>
	/// 取得模板.模板是一个Photon
	/// </summary>
	/// <returns></returns>
	public static TurretConfig GetTemplate() => GetConfig("Equip/Turret/Photon", "Template", ItemBaseConfig.QulityEnum.White);
}
