using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class Equip : Node3D {
	public Equip(EquipData data) {
		uniqueID = data.UniqueID;
	}

	private readonly int uniqueID;
}
