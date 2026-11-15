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

	/// <summary>
	/// 接受任务的时间
	/// </summary>
	[Export] public DateTime? AcceptedTime { get; set; }

	/// <summary>
	/// 任务时长
	/// </summary>
	[Export] public long Time { get; set; }

	/// <summary>
	/// 上一次任务事件刷新的时间
	/// </summary>
	[Export] public DateTime? LastEventUpdatedTime { get; set; }

	[Export] public Godot.Collections.Array<TaskEventData> Events { get; set; } = [];

	[Export] public int RewardSolids { get; set; }
	[Export] public int RewardLiquids { get; set; }
	[Export] public int RewardIndustrialProduct { get; set; }

	// 不允许无参构造
	protected AssignedTaskData() { }

	public AssignedTaskData(string name, AssignedTaskConfig.RatingLevelEnum ratingLevel, int level) {
		NameCode = name;
		RatingLevel = ratingLevel;
		Level = level;
	}
}

