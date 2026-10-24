using System.Diagnostics.CodeAnalysis;
using Godot;
using Godot.Collections;
namespace OursGalaxy;

[GlobalClass]
internal partial class MissionEvent : Resource {
	/// <summary>
	/// 远征任务事件名
	/// </summary>
	[Export]
	public string Code { get; set; }

	/// <summary>
	/// 发生时间
	/// </summary>
	[Export]
	public DateTime Time { get; set; }

	/// <summary>
	/// 序列化的事件奖励
	/// </summary>
	[Export]
	public Array<ItemBase>? Reward { get; set; }

	[Export]
	public bool HasAcceptedReward { get; set; }

	public MissionEvent(string code, DateTime time) {
		Code = code;
		Time = time;
	}



	public bool TryQueryEventDesription([NotNullWhen(true)] out string? result) {
		result = null;
		return false;
	}

	// 禁止无参构造
	private MissionEvent() { Code = string.Empty; Time = DateTime.Now(); }
}
