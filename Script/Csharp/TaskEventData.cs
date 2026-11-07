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

	[Export] public string NameCode { get; set; }

	/// <summary>
	/// 标记事件有没有被处理
	/// </summary>
	[Export] public bool Deal { get; set; }

	/// <summary>
	/// TaskData状态变更，期望重绘UI时发出。
	/// </summary>
	public Action<TaskEventData>? TaskEventStateChanged;

	public TaskEventData(string code) {
		this.Time = DateTime.Now();
		NameCode = code;
	}

	protected TaskEventData() {
		this.Time = DateTime.Now();
		NameCode = string.Empty;
	}
}
