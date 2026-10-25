using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Godot;
namespace OursGalaxy;

/// <summary>
/// 游戏物品基类。不具有数量/唯一性标识能力
/// </summary>
[GlobalClass]
internal partial class ItemBase : Resource {
	public static readonly ItemBase Empty = new(string.Empty);

	public enum QulityEnum { White, Green, Purple, Gold }

	/// <summary>
	/// 辨识id码, 显式使用[表名_物品名_质量]的格式
	/// </summary>
	[Export]
	public string Code {
		get => code; private set {
			code = value;
			PropertyChanged?.Invoke(this, new("Code"));
		}
	}

	public Action<ItemBase, string>? PropertyChanged;

	/// <summary>
	/// ⚠️予Godot设施提供的构造函数。继承类型中应包含，但不应主动使用以产生空值
	/// </summary>
	protected ItemBase() { code = string.Empty; }

	public ItemBase(string code) {
		this.code = code;
	}

	public ItemBase(string table, string name, QulityEnum qulityEnum = QulityEnum.White) {
		this.code = $"{table}_{name}_{(int)qulityEnum}";
	}

	public ItemBase(ItemBase other) : this(other.Code) { }

	protected string GetTableName() => Code.Split("_", false)[0];

	public QulityEnum GetQulity() {
		if (int.TryParse(Code.Split("_", false)[2], out int reusult)) {
			return (QulityEnum)reusult;
		}
		throw new ArgumentOutOfRangeException("Read Qulity Error");
	}

	/// <summary>
	/// ⚠️得到的是db的id,不是id code
	/// </summary>
	/// <param name="name"></param>
	/// <param name="id"></param>
	/// <returns></returns>
	public bool TryQueryID([NotNullWhen(true)] out int? id) {
		id = null;
		return false;
	}

	/// <summary>
	/// 得到显示名
	/// </summary>
	/// <param name="id"></param>
	/// <param name="displayName"></param>
	/// <returns></returns>
	public bool TryQueryDisplayName([NotNullWhen(true)] out string? displayName) {
		displayName = null;
		return false;
	}

	public bool TryQuery<T>(string itemName, [NotNullWhen(true)] out T? result) {
		result = default(T);
		return false;
	}

	public bool TryQueryMore(string[] itemName, [NotNullWhen(true)] out Dictionary<string, object?>? result) {
		result = null;
		return false;
	}

	public static bool operator ==(ItemBase left, ItemBase right) {
		return !(left != right);
	}

	public static bool operator !=(ItemBase left, ItemBase right) {
		return left.Code != right.Code;
	}

	public override bool Equals(object? obj) {
		if (ReferenceEquals(this, obj)) {
			return true;
		}
		return obj is not null && obj is ItemBase objItemBase && this == objItemBase;
	}

	// override object.GetHashCode
	public override int GetHashCode() {
		return Code.GetHashCode() + 17;
	}

	public override string ToString() {
		return $"ItemBase: {Code}";
	}

	private string code;
}
