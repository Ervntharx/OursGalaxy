namespace OursGalaxy;

internal struct DamageSet {

	/// <summary>
	/// 冲击伤害
	/// </summary>
	public int Impact { get => dmgValue[(int)DamageValueEnum.Impact]; set => dmgValue[(int)DamageValueEnum.Impact] = value; }

	/// <summary>
	/// 穿透伤害
	/// </summary>
	public int Penetrate { get => dmgValue[(int)DamageValueEnum.Penetrate]; set => dmgValue[(int)DamageValueEnum.Penetrate] = value; }

	/// <summary>
	/// 光热伤害
	/// </summary>
	public int Photothermal { get => dmgValue[(int)DamageValueEnum.Photothermal]; set => dmgValue[(int)DamageValueEnum.Photothermal] = value; }

	/// <summary>
	/// 酸蚀伤害
	/// </summary>
	public int Corrosion { get => dmgValue[(int)DamageValueEnum.Corrosion]; set => dmgValue[(int)DamageValueEnum.Corrosion] = value; }

	/// <summary>
	/// 电磁伤害
	/// </summary>
	public int Electromagnetic { get => dmgValue[(int)DamageValueEnum.Electromagnetic]; set => dmgValue[(int)DamageValueEnum.Electromagnetic] = value; }

	public DamageSet() { dmgValue = new int[5]; }

	public int Sum() => Impact + Penetrate + Photothermal + Corrosion + Electromagnetic;

	private int[] dmgValue;

	public enum DamageValueEnum {
		Impact = 0,         // 冲击上海
		Penetrate = 1,      // 穿透伤
		Photothermal = 2,   // 光热
		Corrosion = 3,      // 酸蚀
		Electromagnetic = 4 //电磁
	}
}
