using Godot;
using System;
namespace OursGalaxy;

/// <summary>
/// 远征任务。包括已在跑的和刷新出来没有跑的
/// </summary>
[GlobalClass]
internal partial class AssignedTaskData : Resource {
	/// <summary>
	/// 名称
	/// </summary>
	[Export]
	public string Name { get; set; } = "Expedition";

	public enum RatingLevelEnum { D, C, B, A, AA, S, SS }
	[Export]
	public RatingLevelEnum RatingLevel { get; set; } = RatingLevelEnum.AA;

	/// <summary>
	/// The level of the expedition. ranges from 1 to MaxLevel
	/// </summary>
	[Export]
	public ushort Level { get; set; } = 1;

	/// <summary>
	/// The minimum level of the expedition
	/// </summary>
	public const ushort MinLevel = 1;
	/// <summary>
	/// The maximum level of the expedition
	/// </summary>
	public const ushort MaxLevel = 30;

	// [Export]
	// public GameTimeSpan Duration { get; set; } = new(TimeSpan.FromHours(1));

	public enum StateEnum {
		Unaccepted,  // 未接受任务的状态
		Running,   // 正在跑任务
		Completed //已完成
	}

	[Export]
	public StateEnum State { get; protected set; } = StateEnum.Unaccepted;

	[Export]
	public DateTime? StartDate { get; set; }

	[Export]
	protected long ProgressTime { get; set; }
	public TimeSpan ProgressTimeSpan => TimeSpan.FromSeconds(ProgressTime);

	[Export]
	public Godot.Collections.Array<TaskEvent> Events { get; set; } = [];

	public AssignedTaskData(string name, RatingLevelEnum ratingLevel, ushort level) {
		Name = name;
		RatingLevel = ratingLevel;
		Level = level;
	}

	//不允许无参构造
	private AssignedTaskData() { }
}

