using System;
using System.Collections.Generic;
using Godot;
namespace OursGalaxy;

/// <summary>
/// 装备接口
/// </summary>
[Obsolete("Equip instead")]
internal interface IEquip {
	/// <summary>
	/// 尺寸
	/// </summary>
	public OutletSizeType SizeType { get; }

	/// <summary>
	/// 冷却时长
	/// </summary>
	public ulong CDMisc { get; }

	/// <summary>
	/// 效果
	/// </summary>
	public IReadOnlyCollection<IPromote>? Promotes { get; }
}
