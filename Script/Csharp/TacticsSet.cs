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
		Forward,
		Roundabot_Up,
		Roundabot_Down,
		Roundabot_Left,
		Roundabot_Right,
		Facile,
		KillFlagship,
	}

	/// <summary>
	/// 目标选择方式枚举
	/// </summary>
	public enum TargetingEnum {
		Closest,
		Biggest,
		Smallest,
		MiddleThenLargest,
		MiddleThenSmallest,
	}

	[Export]
	public MovementEnum Movement { get; set; } = MovementEnum.Forward;
	[Export]
	public TargetingEnum Targeting { get; set; } = TargetingEnum.Closest;
}
