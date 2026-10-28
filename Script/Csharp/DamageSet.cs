namespace OursGalaxy;

public struct DamageSet {
	/// <summary>
	/// 攻击者.用于伤害统计
	/// </summary>
	public IBattleEntity Attacker { get; set; }
	/// <summary>
	/// 冲击伤害
	/// </summary>
	public int Impact { get; set; }

	/// <summary>
	/// 穿透伤害
	/// </summary>
	public int Penetrate { get; set; }

	/// <summary>
	/// 光热伤害
	/// </summary>
	public int Photothermal { get; set; }

	/// <summary>
	/// 酸蚀伤害
	/// </summary>
	public int Corrosion { get; set; }

	/// <summary>
	/// 电磁伤害
	/// </summary>
	public int Electromagnetic { get; set; }

	public int Sum() => Impact + Penetrate + Photothermal + Corrosion + Electromagnetic;
}
