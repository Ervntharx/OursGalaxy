using System;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 炮台基
/// </summary>
internal partial class TurretBase : EquipBase {
	public IBattleEntity? Target { get; set; }

	/// <summary>
	/// 炮台状态机枚举
	/// </summary>
	public enum StateEnum { IsReady, OnFire, CoolDown }
	public StateEnum State { get; set; } = StateEnum.IsReady;

	/// <summary>
	/// 射程
	/// </summary>
	public virtual float Range { get; set; } = 12.5f;

	public float LeftClampAngle { get; set; } = TurretConfig.DEFAULT_LEFT_CLAMP_ANGLE;
	public float RightClampAngle { get; set; } = TurretConfig.DEFAULT_RIGHT_CLAMP_ANGLE;
	public float UpClampAngle { get; set; } = TurretConfig.DEFAULT_UP_CLAMP_ANGLE;
	public float DownClampAngle { get; set; } = TurretConfig.DEFAULT_DOWN_CLAMP_ANGLE;

	/// <summary>
	/// 目标是否在可击范围内
	/// </summary>
	/// <param name="battleEntity"></param>
	/// <returns></returns>
	public virtual bool IsTargetInRange() {
		GD.PushWarning("<TurretBase> Never override the IsTargetRInRange.");
		return false;
	}

	/// <summary>
	/// 单次伤害
	/// </summary>
	public DamageSet Damage { get; set; }
}
