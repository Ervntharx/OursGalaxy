using System;
using Godot;
namespace OursGalaxy;

/// <summary>
/// 游戏物品，不具唯一性的一类
/// </summary>
[GlobalClass]
internal partial class GameItem : ItemBase {
	public new static readonly GameItem Empty = new(ItemBase.INVALID_ID);
	/// <summary>
	/// 数量
	/// </summary>
	[Export]
	public int Num { get; set; }

	[Obsolete("")]
	public GameItem() : base() { }

	public GameItem(int id, int num = 0) : base(id) {
		Num = num;
	}

	public GameItem(string name, int num = 0) : base(name) {
		Num = num;
	}

	/// <summary>
	/// 是否是临时数据。依靠数量进行判断。
	/// </summary>
	/// <returns></returns>
	public bool IsTemp() => Num == 0;

	/// <summary>
	/// 查询物品名
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public static string QueryName(int id) {
		throw new NotImplementedException();
	}

	/// <summary>
	/// 查询物品ID
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public static int QueryID(string name) {
		throw new NotImplementedException();
	}

	/// <summary>
	/// 查询数据据
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="itemName"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public T Query<T>(string itemName) {
		throw new NotImplementedException();
	}
}
