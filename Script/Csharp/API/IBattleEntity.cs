using System;
using System.Diagnostics.CodeAnalysis;
using Godot;
using OursGalaxy.UI;
namespace OursGalaxy;

/// <summary>
/// 战斗实体接口
/// </summary>
internal interface IBattleEntity : ISpaceEntity {
	public Area3D AttackableArea { get; }

	public CombatStage.TeamEnum Team { get; }
	public bool IsFlagShip { get; }

	public int HP { get; set; }
	public int CurrentHP { get; set; }

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

	public void Attacked(DamageSet damage, GUID from);

	public delegate void OnHPChangedEventHandler(SpaceshipSnapshot shot, int currentHP, int hpChanged);

	public event OnHPChangedEventHandler? OnHPChanged;

	public event Action<SpaceshipSnapshot>? OnEntityDestory;
}
