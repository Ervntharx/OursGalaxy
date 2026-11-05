using Godot;
using Godot.Collections;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

[GlobalClass]
internal partial class EquipData : UniqueItem {
	[Export]
	public Dictionary<string, ushort> Strengthen { get; set; }

	public EquipData(string typeCode, string nameCode, ItemBaseConfig.QulityEnum qulity, GUID? id = null, Dictionary<string, ushort>? strengthen = null) :
		base(typeCode, nameCode, qulity, id) {
		Strengthen = strengthen ?? [];
	}

	protected EquipData() : base() { Strengthen = []; }
}
