using Godot;
/// <summary>
/// 战术集类, 用于存储飞船的战术信息
/// </summary>
[GlobalClass]
internal partial class TacticsSet : Resource {
	/// <summary>
	/// 移动方式枚举
	/// </summary>
	public enum MovementEnum {
		Forward,        // 勇往直前
		Roundabot_Up,   // 上迂回
		Roundabot_Down, // 下迂回
		Roundabot_Left, // 左迂回
		Roundabot_Right,    //右迂回
		Facile,         // 高机动
		Protect         // 护卫
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
	public MovementEnum Movement { get; set; } = MovementEnum.Forward;
	[Export]
	public TargetingEnum Targeting { get; set; } = TargetingEnum.Closest;

	[Export]
	public long UniqueIDOfProtectedShip { get; set; }
}
