using System;
using Godot;
namespace OursGalaxy;

/// <summary>
/// 炮台接口
/// </summary>
[Obsolete("Turret base instead")]
internal interface ITurret : IEquip {
	/// <summary>
	/// 状态机枚举
	/// </summary>
	public enum StateEnum { Ready, Fire, CoolDown }
	public StateEnum State { get; }
	/// <summary>
	/// 已瞄准
	/// </summary>
	public bool IsAimmed { get; }

	/// <summary>
	/// 射程
	/// </summary>
	public float Range { get; set; }

	/// <summary>
	/// 打击目标
	/// </summary>
	public Node3D? TargetEntity { get; set; }

	/// <summary>
	/// 初始化适配。并不是物理安装动作
	/// </summary>
	/// <param name="outlet"></param>
	public void Install(Outlet outlet);
}
