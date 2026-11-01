using Godot;
using System;
using System.Collections.Generic;
namespace OursGalaxy;

internal partial class MissleTurret : TurretBase {
	public static MissleTurret Load() => (MissleTurret)ResourceLoader.Load<PackedScene>("res://Scene/MissleTurret.tscn").Instantiate();

	[Export]
	public CollisionShape3D RangeAreaShape { get; set; } = null!;

	public override float Range { get => ((SphereShape3D)RangeAreaShape.Shape).Radius; set => ((SphereShape3D)RangeAreaShape.Shape).Radius = value; }

	protected override BulletBase LoadBullet() {
		var result = Missile.Load();
		result.Damage = Damage;
		return result;
	}
}
