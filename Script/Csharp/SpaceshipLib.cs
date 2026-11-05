using Godot;
namespace OursGalaxy.ResourceConfig;

internal static class SpaceshipLib {
	public static bool Exists(string shipType, ItemBaseConfig.QulityEnum qulity = ItemBaseConfig.QulityEnum.White)
		=> ResourceLib.Exists(SpaceshipConfig.ItemCodePrefix, shipType, qulity);

	public static SpaceshipConfig QueryConfig(string shipType, ItemBaseConfig.QulityEnum qulity)
		=> ResourceLib.QueryConfig<SpaceshipConfig>(SpaceshipConfig.ItemCodePrefix, shipType, qulity);

	public static SpaceshipConfig QueryConfig(SpaceshipData data)
		=> SpaceshipLib.QueryConfig(data.GetShipTypeName(), data.Qulity);

	public static SpaceshipConfig GetTemplate() => ResourceLoader.Load<SpaceshipConfig>("res://ResourceLib/Spaceship/TemplateShip_0.tres");
}
