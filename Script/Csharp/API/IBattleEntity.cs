using System;
namespace OursGalaxy;

/// <summary>
/// 战斗实体接口
/// </summary>
public interface IBattleEntity {
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
	public void Attacked(DamageSet damageSet);
}
