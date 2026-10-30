using System;
using Godot;
namespace OursGalaxy;
/// <summary>
/// 战术集类, 用于存储飞船的战术信息
/// </summary>
[GlobalClass]
internal partial class TacticsSet : Resource {
	/// <summary>
	/// 战术模式枚举
	/// </summary>
	public enum ModeEnum {
		None,       // 无。对应勇往直前
		Roundabot,  // 迂回
		Protect     // 护卫
	}

	/// <summary>
	/// 迂回方向枚举
	/// </summary>
	public enum RoundabotEnum {
		Rand, Up, Down, Left, Right
	}

	/// <summary>
	/// 目标选择方式枚举
	/// </summary>
	public enum TargetingEnum {
		Closest,            // 锁近
		Biggest,            // 锁大
		Smallest,           // 锁小
		MiddleThenLargest,  // 中大
		MiddleThenSmallest, // 中小
		KillFlagship,       // 斩首 
	}

	/// <summary>
	/// 目标变更频率
	/// </summary>
	public enum ConcentrationEnum {
		NeverChange,    // 从不改变目标
		FewerChange,    // 较少改变目标
		ModerateChange, // 适当改变目标
		FruquetChange   // 频繁改变目标
	}

	[Export]
	public ModeEnum Mode { get; set; } = ModeEnum.None;

	[Export]
	public RoundabotEnum RoundabotDir { get; set; } = RoundabotEnum.Rand;

	[Export]
	public bool Facile { get; set; } = false;

	[Export]
	public TargetingEnum Targeting { get; set; } = TargetingEnum.Closest;

	[Export]
	public GUID? UniqueIDOfProtectedShip { get; set; }

	/// <summary>
	/// 舰船相对位置。非护卫设置下是相对旗舰位置。护卫设置下是相对护卫目标位置.Y值暂时没有意义（未来会替代Layer吗？）
	/// </summary>
	[Export]
	public Vector3 OffsetPosition { get; set; }

	/// <summary>
	/// 所处层
	/// </summary>
	[Export]
	public int Layer { get; set; }

	public static float TacticsVigilance_Highest_Max { get; } = 0.5f;
	public static float TacticsVigilance_Highest_Min { get; } = 0.25f;

	public static float TacticsVigilance_Higher_Max { get; } = 2.5f;
	public static float TacticsVigilance_Higher_Min { get; } = 1.2f;

	public static float TacticsVigilance_Middle_Max { get; } = 5;
	public static float TacticsVigilance_Middle_Min { get; } = 4;

	public static float TacticsVigilance_Low_Max { get; } = 10;
	public static float TacticsVigilance_Low_Min { get; } = 8;
}
