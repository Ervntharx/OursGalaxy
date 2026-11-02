using System;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 游戏物品基类。不具有数量/唯一性标识能力
/// </summary>
[GlobalClass]
internal abstract partial class ItemBase : Resource {
	/// <summary>
	/// 辨识id码, 显式使用[表名_物品名_质量]的格式
	/// </summary>
	[Export]
	public string Code {
		get => code; protected set {
			code = value;
			PropertyChanged?.Invoke(this, new("Code"));
		}
	}

	public Action<ItemBase, string>? PropertyChanged;

	/// <summary>
	/// ⚠️予Godot设施提供的构造函数。继承类型中应包含，但不应主动使用以产生空值
	/// </summary>
	protected ItemBase() { code = string.Empty; }

	public ItemBase(string code) => this.code = code;

	public ItemBase(string table, string name, ItemBaseConfig.QulityEnum qulityEnum = ItemBaseConfig.QulityEnum.White) => this.code = $"{table}_{name}_{(int)qulityEnum}";

	public ItemBase(ItemBase other) : this(other.Code) { }

	public string GetTableName() => Code.Split("_", false)[0];

	public string GetItemName() => Code.Split("_", false)[1];

	public ItemBaseConfig.QulityEnum GetQulity() {
		if (int.TryParse(Code.Split("_", false)[2], out int reusult)) {
			return (ItemBaseConfig.QulityEnum)reusult;
		}
		throw new ArgumentOutOfRangeException("Read Qulity Error");
	}

	public static bool operator ==(ItemBase left, ItemBase right) => !(left != right);
	public static bool operator !=(ItemBase left, ItemBase right) => left.Code != right.Code;

	public override bool Equals(object? obj) {
		if (ReferenceEquals(this, obj)) {
			return true;
		}
		return obj is not null && obj is ItemBase objItemBase && this == objItemBase;
	}

	// override object.GetHashCode
	public override int GetHashCode() => Code.GetHashCode() + 17;

	public override string ToString() => $"ItemBase: {Code}";

	private string code;
}
