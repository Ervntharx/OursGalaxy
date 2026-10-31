using Godot;
using System;
namespace OursGalaxy;

internal partial class Missile : BulletBase {
	public new static Missile Load() => (Missile)ResourceLoader.Load<PackedScene>("res://Scene/Bullets/Missile.tscn").Instantiate();
}
