using Godot;
namespace OursGalaxy.ResourceConfig;

/// <summary>
/// 数据库项类型。表示一个远征任务的配置。当前ResourceLib暂不支持加载它
/// </summary>
[GlobalClass]
internal partial class AssignedTaskConfig : Resource {
	public static AssignedTaskConfig GetConfig(AssignedTaskData data) => ResourceLoader.Load<AssignedTaskConfig>($"res://ResourceLib/Task/{data.NameCode}.tres");

	public enum RatingLevelEnum { D, C, B, A, AA, S, SS }

	[Export]
	public string TaskCode { get; set; } = string.Empty;

	[Export]
	public string DisplayName { get; set; } = string.Empty;

	[Export]
	public string Desc { get; set; } = string.Empty;
}
