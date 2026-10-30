using System;
using Godot;
namespace OursGalaxy;

/// <summary>
/// 代表游戏中一场战斗的数据
/// </summary>
[Obsolete("CombatData instead")]
internal interface ICombat {
	public T ToResource<T>() where T : Resource;
	public Resource AsResource();

	/// <summary>
	/// 名字
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// 等级
	/// </summary>
	public ushort Level { get; set; }
}
