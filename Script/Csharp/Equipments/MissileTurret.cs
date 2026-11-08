using Godot;
using OursGalaxy.ResourceConfig;
using System;
using System.Collections.Generic;
namespace OursGalaxy;

internal partial class MissleTurret : TurretBase {
	public static MissleTurret Load() => (MissleTurret)ResourceLoader.Load<PackedScene>("res://Scene/MissleTurret.tscn").Instantiate();

	[Export]
	protected Area3D RangeArea { get; set; } = null!;

	[Export]
	protected CollisionShape3D RangeAreaShape { get; set; } = null!;

	public override float Range { get => ((SphereShape3D)RangeAreaShape.Shape).Radius; set => ((SphereShape3D)RangeAreaShape.Shape).Radius = value; }

	public override bool IsTargetInRange() => GodotObject.IsInstanceValid(Target?.AsNode3D()) && RangeArea.OverlapsArea(Target.AttackableArea);

	/// <summary>
	/// 进入CD的时间
	/// </summary>
	public ulong EnterCDTime { get; set; }

	/// <summary>
	/// CD时长
	/// </summary>
	public ulong CDMisc { get; set; }

	/// <summary>
	/// 多段伤害段时间间隙
	/// </summary>
	public ulong MakeDamageTimeGapMisc { get; set; } = 200;

	/// <summary>
	/// 总伤害段数。对应激光的连续伤害判定次数和射弹武器的连续射击次数
	/// </summary>
	public int MakeDamageCount { get; set; } = 1;

	/// <summary>
	/// 已造成伤害段数
	/// </summary>
	public int HasMadeDamageCount { get; set; }

	/// <summary>
	/// 上次造成伤害时间
	/// </summary>
	public ulong LastMakeDamageTime { get; set; }

	public float BulletLinearVelocity { get; set; } = MissileConfig.DEFAULT_LINEAR_VELOCITY;

	public override void _PhysicsProcess(double delta) {
		switch (State) {
			case TurretBase.StateEnum.IsReady:
				if (GodotObject.IsInstanceValid(Target?.AsNode3D()) && IsTargetInRange()) {
					State = TurretBase.StateEnum.OnFire;
				}
				break;
			case TurretBase.StateEnum.OnFire:
				// 已打出所有伤害段，进入冷却
				if (HasMadeDamageCount >= MakeDamageCount) {
					State = TurretBase.StateEnum.CoolDown;
					break;
				}
				// 继续打出伤害段
				if (Time.GetTicksMsec() - LastMakeDamageTime >= MakeDamageTimeGapMisc) {
					GD.Print("<TurretBase> try push bullet into BulletServer");
					BulletServer.Push(From, LoadBullet(), GetShootTransform(), GetShootVelocity());
					LastMakeDamageTime = Time.GetTicksMsec();
					HasMadeDamageCount += 1;
					break;
				}
				break;
			case TurretBase.StateEnum.CoolDown:
				if (Time.GetTicksMsec() - EnterCDTime >= CDMisc) {
					State = TurretBase.StateEnum.IsReady;
				}
				break;
			default:
				break;
		}
	}

	protected BulletBase LoadBullet() {
		var result = Missile.Load();
		result.Damage = Damage;
		return result;
	}

	private Transform3D GetShootTransform() {
		var transform = RangeArea.GlobalTransform;
		transform.Origin = transform.Origin + RangeArea.Basis.Y * 0.5f;
		return transform;
	}

	private Vector3 GetShootVelocity() => -GetShootTransform().Basis.Z * BulletLinearVelocity;
}
