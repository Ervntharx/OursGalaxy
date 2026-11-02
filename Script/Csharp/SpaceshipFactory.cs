using System;
using Godot;

namespace OursGalaxy;

internal static class SpaceshipFactory {
	public static bool Exist(string typeName) => ResourceLoader.Exists(GetResourcePathFromTypeName(typeName));

	public static Spaceship Make(string typeName) {
		if (Exist(typeName)) {
			return (Spaceship)ResourceLoader.Load<PackedScene>(GetResourcePathFromTypeName(typeName)).Instantiate();
		}
		throw new ArgumentNullException($"no such ship: {typeName}");
	}

	private static string GetResourcePathFromTypeName(string typeName) => $"{ResPath}{typeName}.tscn";
	private const string ResPath = "res://ResourceLib/Spaceships/";
}
