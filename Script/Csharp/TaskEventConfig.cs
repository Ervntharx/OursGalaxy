using Godot;
namespace OursGalaxy.ResourceConfig;

/// <summary>
/// 资源数据库类。表示一个任务事件的数据配置。但目前暂不支持ResourceLib加载
/// </summary>
[GlobalClass]
internal partial class TaskEventConfig : Resource {
	public static TaskEventConfig GetConfig(TaskEventData data) => ResourceLoader.Load<TaskEventConfig>($"res://ResourceLib/TaskEvent/{data.NameCode}.tres");

	[Export]
	public string EventCode { get; set; } = string.Empty;

	[Export]
	public string DisplayName { get; set; } = string.Empty;

	[Export]
	public string Desc { get; set; } = string.Empty;


}
