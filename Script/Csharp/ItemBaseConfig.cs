using Godot;
namespace OursGalaxy.ResourceConfig;

/// <summary>
/// ItemBase的配置项项目
/// </summary>
[GlobalClass]
internal partial class ItemBaseConfig : Resource {
	public enum QulityEnum { White, Green, Purple, Gold }

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
	/// 简介
	/// </summary>
	[Export] public string Introductuon { get; set; } = string.Empty;
}
