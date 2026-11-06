using System;
using System.Linq;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 飞船数据类, 用于存储飞船的基本信息和状态
/// </summary>
[GlobalClass]
internal partial class SpaceshipData : UniqueItem {
	[Export] public string? CustomName { get; set; }

	[Export] public int CurrentHP { get; set; }

	[Export]
	public Godot.Collections.Dictionary<string, EquipData> EquipsLs { get; set; } = [];

	public SpaceshipData(string typeName, ItemBaseConfig.QulityEnum qulity, GUID? ID = null, ItemLocationInf? loc = null) :
		base("Spaceship", typeName, qulity, ID, loc) { }

	public string GetShipTypeName() => NameCode;

	/// <summary>
	/// ⚠️避免使用无参构造函数构造无意义量
	/// </summary>
	protected SpaceshipData() : base() {

	}
}
