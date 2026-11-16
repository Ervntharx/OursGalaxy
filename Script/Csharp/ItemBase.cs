using System;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 游戏物品基类。不具有数量/唯一性标识能力.持有一个ItemBase用作校对是安全的。
/// ⚠️但没有测试过裸ItemBase与ResourceLib,Factory系的交互能力
/// </summary>
[GlobalClass]
internal partial class ItemBase : Resource, IEquatable<ItemBase> {
	/// <summary>
	/// 物品类型code
	/// </summary>
	[Export] public string TypeCode { get; protected set; } = string.Empty;

	/// <summary>
	/// 名字code
	/// </summary>
	[Export] public string NameCode { get; protected set; } = string.Empty;

	/// <summary>
	/// 品质
	/// </summary>
	[Export] public ItemBaseConfig.QulityEnum Qulity { get; set; } = ItemBaseConfig.QulityEnum.White;

	/// <summary>
	/// ⚠️予Godot设施提供的构造函数。继承类型中应包含，但不应主动使用以产生空值
	/// </summary>
	protected ItemBase() { }

	public ItemBase(string typeCode, string nameCode, ItemBaseConfig.QulityEnum qulityEnum = ItemBaseConfig.QulityEnum.White) {
		TypeCode = typeCode;
		NameCode = nameCode;
		Qulity = qulityEnum;
	}

	public static bool operator ==(ItemBase left, ItemBase right) => !(left != right);
	public static bool operator !=(ItemBase left, ItemBase right) => left.ToString() != right.ToString();

	public bool Equals(ItemBase? other) {
		GD.Print($"test-Equals itembase:{NameCode} and {other?.NameCode} {base.Equals(other)}");
		return other is not null && other.TypeCode == TypeCode && other.NameCode == NameCode && other.Qulity == Qulity;
	}

	public override bool Equals(object? obj) => Equals(obj as ItemBase);

	// override object.GetHashCode
	public override int GetHashCode() => HashCode.Combine(TypeCode.GetHashCode(), NameCode.GetHashCode(), Qulity.GetHashCode());

	public override string ToString() => $"{TypeCode}_{NameCode}_{(int)Qulity}";

}
