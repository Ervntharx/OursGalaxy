using Godot;
using System;
namespace OursGalaxy;

internal partial class Missile : BulletBase {
	public new static Missile Load() => (Missile)ResourceLoader.Load<PackedScene>("res://Scene/Bullets/Missile.tscn").Instantiate();

	/// <summary>
	/// 长跟踪。不会脱锁的导弹为VLF类型
	/// </summary>
	public bool VLF { get; set; }

	public IBattleEntity? Target { get; set; }

	public ulong AliveTime { get; set; }

	/// <summary>
	/// 一阶段角速度
	/// </summary>
	public float Step1RotateVelocity { get; private set; }

	/// <summary>
	/// 二阶段角速度
	/// </summary>
	public float Step2RotateVelocity { get; private set; }

	public override void _PhysicsProcess(double delta) {
		if (GodotObject.IsInstanceValid(Target?.AsNode3D())) {

		}
		base._PhysicsProcess(delta);
	}
}

