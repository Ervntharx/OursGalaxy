using System;
using Godot;
namespace OursGalaxy.UI;

/// <summary>
/// 战斗结果。允许序列化，提供给演习玩法参考结果。
/// </summary>
[GlobalClass]
internal partial class CombatResult : Resource {
	[Export]
	public DateTime Time { get; set; }

	[Export]
	public long TotalSeconds { get; set; }
	public TimeSpan ProgressTime => TimeSpan.FromSeconds(TotalSeconds);

	// TODO: 是否需要Fleet的更多衍生类型？
	[Export]
	public FleetData? LeftResult { get; set; }

	[Export]
	public FleetData? RightFleetResult { get; set; }
}
