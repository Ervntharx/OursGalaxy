using Godot;

namespace OursGalaxy;

/// <summary>
/// 舰船战斗行为状态机
/// </summary>
[GlobalClass]
internal partial class CombatBehaviorStateMachine : Timer {
	/// <summary>
	/// 行为步长。秒单位，每过一段步长时间行为状态机将尝试改变行为
	/// </summary>
	public const int BEHAVIOR_STEP_TIME = 10;

	/// <summary>
	/// 战术设置
	/// </summary>
	public TacticsSet Tactics { get; set; } = new();

	/// <summary>
	/// 移动行为状态枚举
	/// </summary>
	public enum MovementStateEnum {
		Stay,       // 停止
		Approach,   // 接近目标
		Roundabot,  // 迂回
	}

	public MovementStateEnum MovementState { get; set; } = MovementStateEnum.Stay;

	/// <summary>
	/// 锁定的目标
	/// </summary>
	public ISpaceEntity? Target { get; set; }

	/// <summary>
	/// 尝试变更行为状态。
	/// ⚠️该方法并不需要帧刷新
	/// </summary>
	public void UpdateState() {
		// TODO: 更新状态
		preTarget = Target;
		preTargetSteps += 1;
	}

	public override void _Ready() {
		base._Ready();
		OneShot = false;
		WaitTime = BEHAVIOR_STEP_TIME;
		Timeout += UpdateState;
	}

	// 上一个目标
	private ISpaceEntity? preTarget;
	// 锁定上一个目标行为所占的行为步数。用于计算行为频率
	private int preTargetSteps;
}
