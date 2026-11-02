using Godot;

namespace OursGalaxy.ResourceConfig;

internal static class TurretLib {
	public static bool Exists(string turretType, string turretName, ItemBaseConfig.QulityEnum qulity = ItemBaseConfig.QulityEnum.White)
		=> ResourceLib.Exists(turretType, turretName, qulity);

	public static TurretConfig QueryConfig(string turretType, string turretName, ItemBaseConfig.QulityEnum qulity)
		=> ResourceLib.QueryConfig<TurretConfig>(turretType, turretName, qulity);

	public static TurretConfig GetTemplate() => ResourceLoader.Load<TurretConfig>("res://ResourceLib/Turret/Template.tres");
}
