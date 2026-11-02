using System;
using Godot;
namespace OursGalaxy;

internal static class EquipFactory {
	public static bool Exist(string name) => ResourceLoader.Exists(GetResourcePathFromName(name));

	public static EquipBase Load(string name) {
		if (Exist(name)) {
			return (EquipBase)ResourceLoader.Load<PackedScene>(GetResourcePathFromName(name)).Instantiate();
		}
		throw new ArgumentNullException($"no such equip: {name}");
	}

	private static string GetResourcePathFromName(string name) => $"{ResPath}{name}.tscn";
	private const string ResPath = "res://ResourceLib/Equips/";
}
