using System;
using Godot;
/// <summary>
/// 飞船数据类, 用于存储飞船的基本信息和状态
/// </summary>
[GlobalClass]
internal partial class SpaceshipData : Resource {
	public static readonly SpaceshipData Empty = new();
	[Export]
	public string TypeName { get; set; } = string.Empty;

	[Export]
	public string UniqueID { get; set; }

	[Export]
	public string CustomName { get; set; } = string.Empty;

	[Export]
	public int CurrentHealth { get; set; }

	public SpaceshipData(string typeName, string? customName = null) {
		UniqueID = GenerateUniqueID();
		TypeName = typeName;
		CustomName = customName ?? string.Empty;
	}

	public SpaceshipData(Spaceship ship) {
		TypeName = ship.TypeName;
		CustomName = ship.CustomName;
		UniqueID = ship.UniqueID;
		CurrentHealth = ship.CurrentHealth;
	}

	/// <summary>
	/// ⚠️避免使用无参构造函数构造无意义量
	/// </summary>
	protected SpaceshipData() { UniqueID = string.Empty; }

	/// <summary>
	/// 生成舰船唯一ID
	/// 通过当前时间戳生成一个唯一的ID
	/// </summary>
	/// <returns></returns>
	private static string GenerateUniqueID() {
		return Time.GetDateStringFromSystem(true);
	}
}
