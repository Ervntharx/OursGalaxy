using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class TurretConfig : EquipConfig {
	[Export(PropertyHint.Range, "1,999,0")]
	public float Range { get; set; } = 1;

	[Export]
	public ulong CDMisc { get; set; } = 1000;

	[ExportCategory("Damage")]

	/// <summary>
	/// 冲击伤害
	/// </summary>
	[Export(PropertyHint.Range, "0,99,0")]
	public int Impact { get; set; } = 1;

	/// <summary>
	/// 穿透伤害
	/// </summary>
	[Export(PropertyHint.Range, "0,99,0")]
	public int Penetrate { get; set; }

	/// <summary>
	/// 光热伤害
	/// </summary>
	[Export(PropertyHint.Range, "0,99,0")]
	public int Photothermal { get; set; }

	/// <summary>
	/// 酸蚀伤害
	/// </summary>
	[Export(PropertyHint.Range, "0,99,0")]
	public int Corrosion { get; set; }

	/// <summary>
	/// 电磁伤害
	/// </summary>
	[Export(PropertyHint.Range, "0,99,0")]
	public int Electromagnetic { get; set; }
}
