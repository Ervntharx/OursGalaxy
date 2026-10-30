using Godot;
using System;
namespace OursGalaxy;

[GlobalClass]
internal partial class ShieldData : Resource {
	public static readonly ShieldData Empty = new();
	public static ShieldData Template => ResourceLoader.Load<ShieldData>("res://Res/ShieldEquip/Template.tres");

	public static ShieldData? Load(string Code) => ResourceLoader.Exists($"res://Res/ShieldEquip/{Code}.tres") ? ResourceLoader.Load<ShieldData>($"res://Res/ShieldEquip/{Code}.tres") : null;

	[Export]
	public string Code { get; set; } = string.Empty;

	[Export]
	public string DisplayName { get; set; } = string.Empty;

	[Export]
	public int HP { get; set; }

	[Export]
	public float CDMisc { get; set; }
}
