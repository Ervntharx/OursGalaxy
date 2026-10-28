using System;
using Godot;
namespace OursGalaxy;

/// <summary>
/// 运行时飞船类
/// </summary>
internal partial class Spaceship : Node3D, ISpaceEntity, IBattleEntity {
	public enum ScaleSizeEnum { FF, DD, CL, CA, BB }

	public T ToNode<T>() where T : Node => (this as T)!;
	public Node3D ToNode3D() => this;

	[Export]
	protected CombatBehaviorStateMachine? BehaviorStateMachine { get; set; }

	[Export]
	public ScaleSizeEnum ScaleSize { get; set; } = ScaleSizeEnum.FF;

	public GUID ID { get; init; }

	public string TypeName { get; set; }
	public string? CustomName { get; set; }

	public int Health { get; set; }

	public int CurrentHealth { get; set; }

	/// <summary>
	/// 回避性能
	/// </summary>
	public int PreventValue { get; set; }

	/// <summary>
	/// 冲击伤害耐性
	/// </summary>
	public int ImpactTolerance { get; set; }

	/// <summary>
	/// 穿透伤害耐性
	/// </summary>
	public int PenetrateTolerance { get; set; }

	/// <summary>
	/// 光热伤害耐性
	/// </summary>
	public int PhotothermalTolerance { get; set; }

	/// <summary>
	/// 酸蚀伤害耐性
	/// </summary>
	public int CorrosionTolerance { get; set; }

	/// <summary>
	/// 电磁伤害耐性
	/// </summary>
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

	public Spaceship() {
		ID = GUID.Empty;
		TypeName = string.Empty;
		CurrentHealth = 0;
	}

	public Spaceship(SpaceshipData data) {
		ID = data.ID;
		TypeName = data.GetShipTypeName();
		CustomName = data.CustomName;
		CurrentHealth = data.CurrentHP;
	}

	public SpaceshipData Save() {
		return new SpaceshipData(TypeName, ID);
	}

	/// <summary>
	/// 死亡事件
	/// </summary>
	public event Action<IBattleEntity>? OnDeath;

	public override void _Process(double delta) {

		base._Process(delta);
	}

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);
	}
}
