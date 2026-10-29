using System;
using System.ComponentModel;
using Godot;
namespace OursGalaxy;

/// <summary>
/// 远征任务事件类型
/// </summary>
[GlobalClass]
internal partial class TaskEvent : Resource {
	[Export]
	public DateTime Time { get; set; }

	[Export]
	public string Code { get; set; }

	public string Desc => desc.Value;

	public virtual string? ReactButtonName { get; }
	public virtual string? ReactUrl { get; }

	/// <summary>
	/// TaskData状态变更，期望重绘UI时发出。
	/// </summary>
	public Action<TaskEvent>? TaskEventStateChanged;

	public TaskEvent(string code) {
		this.Time = DateTime.Now();
		Code = code;
		desc = new("here's some desc");
	}

	protected TaskEvent() {
		this.Time = DateTime.Now();
		Code = string.Empty;
		desc = new(string.Empty);
	}

	public virtual void OnMatchUrl(Variant variant) { }

	private Lazy<string> desc;
}
