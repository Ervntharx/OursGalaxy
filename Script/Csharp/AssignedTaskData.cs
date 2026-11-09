using Godot;
using System;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 远征任务。包括已在跑的和刷新出来没有跑的
/// </summary>
[GlobalClass]
internal partial class AssignedTaskData : Resource {
	/// <summary>
	/// 名称
	/// </summary>
	[Export] public string NameCode { get; set; } = "Expedition";

	[Export] public AssignedTaskConfig.RatingLevelEnum RatingLevel { get; set; } = AssignedTaskConfig.RatingLevelEnum.AA;

	/// <summary>
	/// The level of the expedition. ranges from 1 to MaxLevel
	/// </summary>
	[Export] public int Level { get; set; } = 1;

	[Export] public AssignedTaskConfig.StateEnum State { get; protected set; } = AssignedTaskConfig.StateEnum.Unaccepted;

	[Export] public DateTime? StartDate { get; set; }

	/// <summary>
	/// 上一次任务事件刷新的时间
	/// </summary>
	[Export] public DateTime? LastEventUpdatedTime { get; set; }

	protected long ProgressTime { get; set; }
	public TimeSpan ProgressTimeSpan => TimeSpan.FromSeconds(ProgressTime);

	[Export]
	public Godot.Collections.Array<TaskEventData> Events { get; set; } = [];

	public AssignedTaskData(string name, AssignedTaskConfig.RatingLevelEnum ratingLevel, ushort level) {
		NameCode = name;
		RatingLevel = ratingLevel;
		Level = level;
	}

	//不允许无参构造
	private AssignedTaskData() { }
}

