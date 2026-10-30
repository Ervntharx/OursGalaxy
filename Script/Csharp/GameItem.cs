using System;
using System.Linq;
using Godot;
namespace OursGalaxy;

/// <summary>
/// 游戏物品，不具唯一性的一类
/// </summary>
[GlobalClass]
internal partial class GameItem : ItemBase {
	public new static readonly GameItem Empty = new();
	/// <summary>
	/// 数量
	/// </summary>
	[Export]
	public int Num {
		get => num; set {
			num = value;
			PropertyChanged?.Invoke(this, new("Num"));
		}
	}

	public static GameItem FromString(string str) {
		var ls = str.Split(":", false);
		if (int.TryParse(ls[1], out var num)) {
			return new(ls[0], num);
		}
		throw new InvalidCastException("not a string of game item");
	}

	/// <summary>
	/// ⚠️予Godot设施提供的构造函数。继承类型中应包含，但不应主动使用以产生空值
	/// </summary>
	protected GameItem() : base() { }

	public GameItem(string code, int num = 0) : base(code) {
		Num = num;
	}

	public GameItem(string table, string name, ItemBase.QulityEnum qulity, int num = 0) : base(table, name, qulity) {
		Num = num;
	}

	/// <summary>
	/// 是否是临时数据。依靠数量进行判断。
	/// </summary>
	/// <returns></returns>
	public bool IsTemp() => Num == 0;

	public override string ToString() {
		return $"{Code}:{Num}";
	}

	private int num;
}
