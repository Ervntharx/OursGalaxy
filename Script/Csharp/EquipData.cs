using Godot;
using Godot.Collections;
[GlobalClass]
internal partial class EquipData : Resource {
	[Export]
	public int TypeID { get; set; }
	[Export]
	public int UniqueID { get; set; }

	[Export]
	public Dictionary<string, ushort> Strengthen { get; set; } = [];
}
