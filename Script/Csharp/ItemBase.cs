using System;
using System.Diagnostics.CodeAnalysis;
using Godot;

/// <summary>
/// 游戏物品基类。不具有数量/唯一性标识能力
/// </summary>
[GlobalClass]
internal partial class ItemBase : Resource {
	public const int INVALID_ID = -1;
	public static readonly ItemBase Empty = new(INVALID_ID);

	[Export]
	public int ID { get; protected set; }

	[Export]
	public string? Name { get; protected set; }

	/// <summary>
	/// ⚠️避免调用以产生空值
	/// </summary>
	protected ItemBase() {
		ID = INVALID_ID;
	}

	public ItemBase(int id) {
		ID = id;
	}

	public ItemBase(string name) {
		Name = name;
		if (TryQueryID(name, out var id)) {
			ID = id.Value;
		}
		else {
			throw new ArgumentException();
		}
	}

	public bool TryQueryID(string name, [NotNullWhen(true)] out int? id) {
		id = null;
		return false;
	}

	public bool TryQueryName(int id, [NotNullWhen(true)] out string? name) {
		name = null;
		return false;
	}

	public bool TryQuery<T>(string itemName, [NotNullWhen(true)] out T? result) {
		result = default(T);
		return false;
	}
}
