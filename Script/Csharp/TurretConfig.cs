using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class TurretConfig : EquipConfig {
	public const string ITEM_CODE_PREFIX = "Equip/Turret";

	[Export]
	public float Range { get; set; } = 1;

	[Export]
	public ulong CDMisc { get; set; } = 1000;

	[ExportCategory("Damage")]

	/// <summary>
	/// 冲击伤害
	/// </summary>
	[Export]
	public int Impact { get; set; } = 1;

	/// <summary>
	/// 穿透伤害
	/// </summary>
	[Export]
	public int Penetrate { get; set; }

	/// <summary>
	/// 光热伤害
	/// </summary>
	[Export]
	public int Photothermal { get; set; }

	/// <summary>
	/// 酸蚀伤害
	/// </summary>
	[Export]
	public int Corrosion { get; set; }

	/// <summary>
	/// 电磁伤害
	/// </summary>
	[Export]
	public int Electromagnetic { get; set; }
}
