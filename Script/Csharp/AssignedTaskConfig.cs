using System;
using Godot;
namespace OursGalaxy.ResourceConfig;

/// <summary>
/// 数据库项类型。表示一个远征任务的配置。当前ResourceLib暂不支持加载它
/// </summary>
[GlobalClass]
internal partial class AssignedTaskConfig : Resource {
	public enum StateEnum {
		Unaccepted,  // 未接受任务的状态
		Running,   // 正在跑任务
		Completed //已完成
	}

	/// <summary>
	/// The minimum level of the expedition
	/// </summary>
	public const ushort MinLevel = 1;

	/// <summary>
	/// The maximum level of the expedition
	/// </summary>
	public const ushort MaxLevel = 30;

	public static AssignedTaskConfig GetConfig(AssignedTaskData data) => ResourceLoader.Load<AssignedTaskConfig>($"res://ResourceLib/Task/{data.NameCode}.tres");

	public enum RatingLevelEnum { D = 0, C = 1, B = 2, A = 3, AA = 4, S = 5, SS = 6 }

	[Export] public string TaskCode { get; set; } = string.Empty;

	[Export] public string DisplayName { get; set; } = string.Empty;

	[Export] public string Desc { get; set; } = string.Empty;

	/// <summary>
	/// 远征任务事件刷新时长,单位秒
	/// </summary>
	[Export] public long UpdateSec { get; set; } = (long)TimeSpan.FromHours(0.5).TotalSeconds;

	/// <summary>
	/// 远征任务事件刷新时长的浮动时间长度
	/// </summary>
	[Export] public long UpdateRandomFloatingSec { get; set; } = (long)TimeSpan.FromMinutes(5).TotalSeconds;
}
