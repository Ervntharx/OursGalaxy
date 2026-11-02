using Godot;
namespace OursGalaxy.ResourceConfig;

internal static class SpaceshipLib {
	public static bool Exists(string shipType, ItemBaseConfig.QulityEnum qulity = ItemBaseConfig.QulityEnum.White)
		=> ResourceLib.Exists(SpaceshipConfig.ItemCodePrefix, shipType, qulity);

	public static SpaceshipConfig QueryConfig(string shipType, ItemBaseConfig.QulityEnum qulity)
		=> ResourceLib.QueryConfig<SpaceshipConfig>(SpaceshipConfig.ItemCodePrefix, shipType, qulity);

	public static SpaceshipConfig GetTemplate() => ResourceLoader.Load<SpaceshipConfig>("res://ResourceLib/Spaceship/TemplateShip.tres");
}
