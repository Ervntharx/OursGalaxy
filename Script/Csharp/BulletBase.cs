using Godot;
namespace OursGalaxy;

internal partial class BulletBase : Area3D {
	public static BulletBase Load() => (BulletBase)ResourceLoader.Load<PackedScene>("res://Scene/Bullet/TemplateBullet.tscn").Instantiate();

	public GUID From { get; set; } = GUID.Empty;

	[Export]
	public CollisionShape3D Shape { get; set; } = null!;

	public Vector3 LinearVelocity { get; set; }

	public DamageSet Damage { get; set; }

	/// <summary>
	/// 子弹存活时间。超过时间自动销毁自己
	/// </summary>
	public ulong AliveTimeMisc { get; set; } = 3000;

	public override void _EnterTree() {
		shootTime = Time.GetTicksMsec();
	}

	public override void _PhysicsProcess(double delta) {
		if (shootTime > AliveTimeMisc) {
			QueueFree();
		}
		var pos = GlobalPosition;
		pos += LinearVelocity * (float)delta;
		GlobalPosition = pos;
	}

	private ulong shootTime;
}
