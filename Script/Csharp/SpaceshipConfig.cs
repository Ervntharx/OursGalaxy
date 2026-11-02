using System.ComponentModel.DataAnnotations;
using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class SpaceshipConfig : ItemBaseConfig {
	public const string ItemCodePrefix = "Ship";

	[Export]
	public string ShipTypeName { get; set; } = string.Empty;

	public enum ScaleSizeEnum { FF, DD, CL, CA, BB }
	[Export]
	public ScaleSizeEnum ScaleSize { get; set; } = ScaleSizeEnum.FF;

	[Export(PropertyHint.Range, "1,1000,1")]
	public int Size { get; set; }

	/// <summary>
	/// 血量上限
	/// </summary>
	[ExportGroup("Battle")]
	[Export(PropertyHint.Range, "1,9999,1")]
	public int HP { get; set; }

	[Export(PropertyHint.Range, "1,9999,0")]
	public int Raid { get; set; }

	/// <summary>
	/// 回避性能
	/// </summary>
	[Export(PropertyHint.Range, "0, 9999, 1")]
	public int PreventValue { get; set; }

	/// <summary>
	/// 冲击伤害耐性
	/// </summary>
	[Export(PropertyHint.Range, "0, 9999, 1")]
	public int ImpactTolerance { get; set; }

	/// <summary>
	/// 穿透伤害耐性
	/// </summary>
	[Export(PropertyHint.Range, "0, 9999, 1")]
	public int PenetrateTolerance { get; set; }

	/// <summary>
	/// 光热伤害耐性
	/// </summary>
	[Export(PropertyHint.Range, "0, 9999, 1")]
	public int PhotothermalTolerance { get; set; }

	/// <summary>
	/// 酸蚀伤害耐性
	/// </summary>
	[Export(PropertyHint.Range, "0, 9999, 1")]
	public int CorrosionTolerance { get; set; }

	/// <summary>
	/// 电磁伤害耐性
	/// </summary>
	[Export(PropertyHint.Range, "0, 9999, 1")]
	public int ElectromagneticTolerance { get; set; }


	/// <summary>
	/// 转向能力
	/// </summary>
	[ExportGroup("Movement")]
	[Export(PropertyHint.Range, "1, 999, 1")]
	public int RotateCapability { get; set; } = 15;

	/// <summary>
	/// 微机动能力
	/// </summary>
	[Export(PropertyHint.Range, "1, 999, 1")]
	public int MicroPowerCapability { get; set; } = 2;

	/// <summary>
	/// 推进能力
	/// </summary>
	[Export(PropertyHint.Range, "1, 999, 1")]
	public int PowerCapability { get; set; } = 5;

	/// <summary>
	/// 反推能力
	/// </summary>
	[Export(PropertyHint.Range, "1, 999, 1")]
	public int BackPowerCapability { get; set; } = 2;
}
