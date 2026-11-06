using Godot;
using Godot.Collections;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

[GlobalClass]
internal partial class EquipData : UniqueItem {
	[Export] public OutletSizeType SizeType { get; set; } = OutletSizeType.Air;
	[Export] public Dictionary<int, ushort> Strengthen { get; set; }

	public EquipData(string typeCode, string nameCode, ItemBaseConfig.QulityEnum qulity, GUID? id = null, Dictionary<int, ushort>? strengthen = null) :
		base(typeCode, nameCode, qulity, id) {
		Strengthen = strengthen ?? [];
	}

	protected EquipData() : base() { Strengthen = []; }
}
