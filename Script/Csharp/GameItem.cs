using System;
using System.Linq;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 游戏物品，不具唯一性的一类
/// </summary>
[GlobalClass]
internal partial class GameItem : ItemBase {
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

	/// <summary>
	/// ⚠️予Godot设施提供的构造函数。继承类型中应包含，但不应主动使用以产生空值
	/// </summary>
	protected GameItem() : base() { }

	public GameItem(string typeCode, string nameCode, ItemBaseConfig.QulityEnum qulity, int num = 0) : base(typeCode, nameCode, qulity) => Num = num;

	/// <summary>
	/// 是否是临时数据。依靠数量进行判断。
	/// </summary>
	/// <returns></returns>
	public bool IsTemp() => Num == 0;

	public override string ToString() => base.ToString() + $":{Num}";

	private int num;
}
