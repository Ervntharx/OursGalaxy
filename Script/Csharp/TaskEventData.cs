using System;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 远征任务事件类型
/// </summary>
[GlobalClass]
internal partial class TaskEventData : Resource {
	[Export] public DateTime Time { get; set; }

	[Export] public TaskEventConfig.EventTypeEnum EventType { get; set; } = TaskEventConfig.EventTypeEnum.Genearal;

	[Export] public string NameCode { get; set; }

	/// <summary>
	/// 标记事件有没有被处理
	/// </summary>
	[Export] public bool Deal { get; set; }

	public TaskEventData(TaskEventConfig.EventTypeEnum eventType, string code) {
		EventType = eventType;
		this.Time = DateTime.Now();
		NameCode = code;
	}

	protected TaskEventData() {
		this.Time = DateTime.Now();
		NameCode = string.Empty;
	}
}
