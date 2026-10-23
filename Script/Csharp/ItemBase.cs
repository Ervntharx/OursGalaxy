using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Godot;

/// <summary>
/// 游戏物品基类。不具有数量/唯一性标识能力
/// </summary>
[GlobalClass]
internal partial class ItemBase : Resource {
	public static readonly ItemBase Empty = new(string.Empty);

	[Export]
	[Obsolete]
	public int ID { get; protected set; }

	/// <summary>
	/// 辨识id码, 显式使用[表名_物品名_id]的格式
	/// </summary>
	[Export]
	public string Code { get; private set; }

	/// <summary>
	/// ⚠️予Godot设施提供的构造函数。继承类型中应包含，但不应主动使用以产生空值
	/// </summary>
	protected ItemBase() { Code = string.Empty; }

	public ItemBase(string code) {
		Code = code;
	}

	protected string GetTableName() => Code.Split("_", false)[0];

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
}
