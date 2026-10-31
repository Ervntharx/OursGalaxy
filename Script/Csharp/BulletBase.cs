using Godot;
namespace OursGalaxy;

internal partial class BulletBase : Area3D {
	public static BulletBase Load() => (BulletBase)ResourceLoader.Load<PackedScene>("res://Scene/Bullets/TemplateBullet.tscn").Instantiate();

	[Export]
	public CollisionShape3D Shape { get; set; } = null!;

	public Vector3 LinearVelocity { get; set; }

	public DamageSet Damage { get; set; }
}
