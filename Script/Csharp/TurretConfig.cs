using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class TurretConfig : EquipConfig {
	public const string ITEM_CODE_PREFIX = "Equip/Turret";

	public const ulong DEFAULT_CDMISC = 3000;
	public const float DEFAULT_ROTATE_ANGLE_VELOCITY = 45;
	public const float DEFAULT_LEFT_CLAMP_ANGLE = 180;
	public const float DEFAULT_RIGHT_CLAMP_ANGLE = 180;
	public const float DEFAULT_UP_CLAMP_ANGLE = 45;
	public const float DEFAULT_DOWN_CLAMP_ANGLE = 45;

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
