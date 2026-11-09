using Godot;
namespace OursGalaxy.ResourceConfig;

/// <summary>
/// 资源数据库类。表示一个任务事件的数据配置。但目前暂不支持ResourceLib加载
/// </summary>
[GlobalClass]
internal partial class TaskEventConfig : Resource {
	public const string TEMPLATE_EVENT_NAME_CODE = "Template";
	public static TaskEventConfig GetConfig(TaskEventData data) => ResourceLoader.Load<TaskEventConfig>($"res://ResourceLib/TaskEvent/{data.NameCode}.tres");

	public enum EventTypeEnum { Genearal, SignCombat, FightBack, Reward, Repair }
	[Export] public EventTypeEnum EventType { get; set; } = EventTypeEnum.Genearal;

	[Export] public string EventCode { get; set; } = string.Empty;

	[Export] public string DisplayName { get; set; } = string.Empty;

	[Export] public string Desc { get; set; } = string.Empty;

	[Export] public virtual string DealButtonName { get; set; } = string.Empty;
	[Export] public virtual string HandledButtonName { get; set; } = string.Empty;

	[Export] public virtual string DealUrl { get; set; } = string.Empty;

	public virtual void Deal() { }
}
