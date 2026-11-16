using System;
using System.Linq;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 游戏物品，不具唯一性的一类
/// </summary>
[GlobalClass]
internal partial class GameItem : ItemBase, IEquatable<GameItem> {
	/// <summary>
	/// 数量
	/// </summary>
	[Export]
	public int Num {
		get => num; set {
			num = Math.Max(value, 0);
			NumChangedEvent?.Invoke(this, num);
		}
	}

	public event Action<GameItem, int>? NumChangedEvent;

	/// <summary>
	/// ⚠️予Godot设施提供的构造函数。继承类型中应包含，但不应主动使用以产生空值
	/// </summary>
	protected GameItem() : base() { }

	public GameItem(string nameCode, ItemBaseConfig.QulityEnum qulity, int num = 0) : base(ItemBaseConfig.GAME_ITEM_CODE_PREFIX, nameCode, qulity) => Num = num;
	public GameItem(string itemTypeCode, string nameCode, ItemBaseConfig.QulityEnum qulity, int num = 0) : base(itemTypeCode, nameCode, qulity) { Num = num; }

	/// <summary>
	/// 是否是临时数据。依靠数量进行判断。
	/// </summary>
	/// <returns></returns>
	public bool IsTemp() => Num == 0;

	public override string ToString() => base.ToString() + $":{Num}";

	public bool Equals(GameItem? other) {
		GD.Print($"test-Equals:{NameCode} and {other.NameCode} {base.Equals(other)}");
		return base.Equals(other);
	}

	public override bool Equals(object? obj) {
		GD.Print($"test-Equals:{NameCode} and {(obj is GameItem item ? item.NameCode : "null")} {base.Equals(obj as GameItem)}");
		return Equals(obj as GameItem);
	}

	public override int GetHashCode() => base.GetHashCode();

	private int num;
}
