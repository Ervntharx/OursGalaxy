using Godot;
using Godot.Collections;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 装备数据基类
/// </summary>
[GlobalClass]
internal partial class EquipData : UniqueItem {
	/// <summary>
	/// 强化数据
	/// </summary>
	[Export] public Dictionary<int, ushort> Strengthen { get; set; }

	public EquipData(string typeCode, string nameCode, ItemBaseConfig.QulityEnum qulity, GUID? id = null, Dictionary<int, ushort>? strengthen = null) :
		base(typeCode, nameCode, qulity, id) {
		Strengthen = strengthen ?? [];
	}

	/// <summary>
	/// ⚠️无参构造提供予godot inespector使用。应避免主动无参构造产生空值
	/// </summary>
	protected EquipData() : base() { Strengthen = []; }
}
