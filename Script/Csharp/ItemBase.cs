using System;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 游戏物品基类。不具有数量/唯一性标识能力.持有一个ItemBase用作校对是安全的
/// </summary>
[GlobalClass]
internal partial class ItemBase : Resource {
	/// <summary>
	/// 物品类型code
	/// </summary>
	[Export]
	public string ITypeCode { get; protected set; } = string.Empty;
	/// <summary>
	/// 名字code
	/// </summary>
	[Export]
	public string NameCode { get; protected set; } = string.Empty;
	/// <summary>
	/// 品质
	/// </summary>
	[Export]
	public ItemBaseConfig.QulityEnum Qulity { get; set; } = ItemBaseConfig.QulityEnum.White;

	public Action<ItemBase, string>? PropertyChanged;

	/// <summary>
	/// ⚠️予Godot设施提供的构造函数。继承类型中应包含，但不应主动使用以产生空值
	/// </summary>
	protected ItemBase() { }

	public ItemBase(string typeCode, string nameCode, ItemBaseConfig.QulityEnum qulityEnum = ItemBaseConfig.QulityEnum.White) {
		ITypeCode = typeCode;
		NameCode = nameCode;
		Qulity = qulityEnum;
	}

	public static bool operator ==(ItemBase left, ItemBase right) => !(left != right);
	public static bool operator !=(ItemBase left, ItemBase right) => left.ToString() != right.ToString();

	public override bool Equals(object? obj) {
		if (ReferenceEquals(this, obj)) {
			return true;
		}
		return obj is not null && obj is ItemBase objItemBase && this == objItemBase;
	}

	// override object.GetHashCode
	public override int GetHashCode() => ITypeCode.GetHashCode() + NameCode.GetHashCode() + Qulity.GetHashCode();

	public override string ToString() => $"{ITypeCode}_{NameCode}_{(int)Qulity}";

}
