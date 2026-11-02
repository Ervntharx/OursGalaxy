using Godot;
namespace OursGalaxy.ResourceConfig;

/// <summary>
/// ItemBase的配置项项目
/// </summary>
[GlobalClass]
internal partial class ItemBaseConfig : Resource {
	/// <summary>
	/// ItemBase的物品类型Code
	/// </summary>
	[Export]
	public string Code { get; set; } = TEMPLATE_CODE;

	[Export]
	public string DisplayName { get; set; } = string.Empty;

	/// <summary>
	/// 简介
	/// </summary>
	[Export]
	public string Introductuon { get; set; } = string.Empty;

	private const string TEMPLATE_CODE = "Template";
}
