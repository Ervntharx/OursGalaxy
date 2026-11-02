using Godot;
using Godot.Collections;
namespace OursGalaxy;

[GlobalClass]
internal partial class EquipData : UniqueItem {
	[Export]
	public Dictionary<string, ushort> Strengthen { get; set; }

	public EquipData(string code, GUID? id = null, Dictionary<string, ushort>? strengthen = null) : base(code, id) {
		Strengthen = strengthen ?? [];
	}

	protected EquipData() : base() { Strengthen = []; }
}
