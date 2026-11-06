using Godot;
namespace OursGalaxy.ResourceConfig;

/// <summary>
/// 数据库配置项类型。作战任务的配置，包括讨伐，猎杀的配置。ResourceLib暂时不支持加载它
/// </summary>
[GlobalClass]
internal partial class CombatConfig : Resource {
	public static CombatConfig GetConfig(CombatData data) => ResourceLoader.Load<CombatConfig>($"res://ResourceLib/Combat/{data.NameCode}.tres");

	[Export]
	public string NameCode { get; set; } = string.Empty;

	[Export]
	public string DisplayName { get; set; } = string.Empty;

	[Export]
	public FleetData? Fleet { get; set; }

	[Export]
	public string Desc { get; set; } = string.Empty;
}
