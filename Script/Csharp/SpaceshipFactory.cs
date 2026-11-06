using System;
using Godot;
using OursGalaxy.ResourceConfig;

namespace OursGalaxy;

internal static class SpaceshipFactory {
	public static bool Exist(string typeName) => ResourceLoader.Exists(GetResourcePathFromTypeName(typeName));

	public static Spaceship Make(string typeName) {
		if (Exist(typeName)) {
			return (Spaceship)ResourceLoader.Load<PackedScene>(GetResourcePathFromTypeName(typeName)).Instantiate();
		}
		throw new ArgumentNullException($"no such ship: {typeName}");
	}

	public static Spaceship Make(SpaceshipData data) {
		if (Exist(data.GetShipTypeName())) {
			var result = (Spaceship)ResourceLoader.Load<PackedScene>(GetResourcePathFromTypeName(data.GetShipTypeName())).Instantiate();
			var config = SpaceshipLib.GetConfig(data);
			result.ShipTypeName = config.ShipTypeName;
			result.HP = config.HP;
			result.ScaleSize = config.ScaleSize;
			result.PreventValue = config.PreventValue;
			result.ImpactTolerance = config.ImpactTolerance;
			result.PenetrateTolerance = config.PenetrateTolerance;
			result.PhotothermalTolerance = config.PhotothermalTolerance;
			result.ElectromagneticTolerance = config.ElectromagneticTolerance;
			result.RotateCapability = config.RotateCapability;
			result.MicroPowrCapability = config.MicroPowerCapability;
			result.PowerCapability = config.PowerCapability;
			result.BackPowerCapability = config.BackPowerCapability;

			result.CurrentHP = data.CurrentHP;
			result.CustomName = data.CustomName;
			return result;
		}
		throw new ArgumentNullException($"no such ship: {data.GetShipTypeName()}");
	}

	private static string GetResourcePathFromTypeName(string typeName) => $"{ResPath}{typeName}.tscn";
	private const string ResPath = "res://Scene/Spaceship/";
}
