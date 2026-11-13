using Godot;
namespace OursGalaxy.ResourceConfig;

/// <summary>
/// ItemBase的配置项项目
/// </summary>
[GlobalClass]
internal partial class ItemBaseConfig : Resource {
	public const string GAME_ITEM_CODE_PREFIX = "GameItem";
	public enum QulityEnum { White = 0, Green = 1, Purple = 2, Gold = 3 }

	/// <summary>
	/// 物品类型code
	/// </summary>
	[Export] public string TypeCode { get; set; } = string.Empty;

	/// <summary>
	/// 名称code
	/// </summary>
	[Export] public string NameCode { get; set; } = string.Empty;

	/// <summary>
	/// 品质
	/// </summary>
	[Export] public QulityEnum Qulity { get; set; } = QulityEnum.White;

	/// <summary>
	/// 显示名
	/// </summary>
	[Export] public string DisplayName { get; set; } = string.Empty;

	/// <summary>
	/// 图标
	/// </summary>
	[Export] public Texture2D? Texture { get; set; }

	/// <summary>
	/// 简介
	/// </summary>
	[Export] public string Introductuon { get; set; } = string.Empty;
}
