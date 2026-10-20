using System;
using Godot;
/// <summary>
/// 飞船类
/// </summary>
[GlobalClass]
internal partial class Spaceship : Node3D, ISpaceEntity, IBattleEntity {
	public T ToNode<T>() where T : Node {
		return (this as T)!;
	}

	public Node3D ToNode3D() {
		return this;
	}

	[Export]
	public int Health { get; set; }
	[Export]
	public int CurrentHealth { get; set; }
	[Export]
	public int Shield { get; set; }

	/// <summary>
	/// 回避性能
	/// </summary>
	[Export]
	public int PreventValue { get; set; }

	/// <summary>
	/// 冲击伤害耐性
	/// </summary>
	[Export]
	public int ImpactTolerance { get; set; }

	/// <summary>
	/// 穿透伤害耐性
	/// </summary>
	[Export]
	public int PenetrateTolerance { get; set; }

	/// <summary>
	/// 光热伤害耐性
	/// </summary>
	[Export]
	public int PhotothermalTolerance { get; set; }

	/// <summary>
	/// 酸蚀伤害耐性
	/// </summary>
	[Export]
	public int CorrosionTolerance { get; set; }

	/// <summary>
	/// 电磁伤害耐性
	/// </summary>
	[Export]
	public int ElectromagneticTolerance { get; set; }

	/// <summary>
	/// 被攻击
	/// </summary>
	/// <param name="target">目标实体</param>
	public void Attacked(DamageSet damageSet) {
		var damage = new DamageSet() {
			Impact = Math.Max(damageSet.Impact - ImpactTolerance, 0),
			Penetrate = Math.Max(damageSet.Penetrate - PenetrateTolerance, 0),
			Photothermal = Math.Max(damageSet.Photothermal - PhotothermalTolerance, 0),
			Corrosion = Math.Max(damageSet.Corrosion - CorrosionTolerance, 0),
			Electromagnetic = Math.Max(damageSet.Electromagnetic - ElectromagneticTolerance, 0)
		};
	}

	public Spaceship(SpaceshipData data) {
		CurrentHealth = data.CurrentHealth;
		uniqueID = data.UniqueID;
	}

	public SpaceshipData Save() {
		return new SpaceshipData() {
			CurrentHealth = CurrentHealth,
			UniqueID = uniqueID
		};
	}

	/// <summary>
	/// 死亡事件
	/// </summary>
	public event Action<IBattleEntity>? OnDeath;

	private readonly int uniqueID;
}
