using Godot;
using OursGalaxy;

[GlobalClass]
internal partial class UniqueItem : ItemBase {
	[Export]
	public int UniqueID { get; protected set; }

	public UniqueItem(int id, int? uniqueID = null) {
		ID = id;
		if (uniqueID is null) {
			uniqueID
		}
		UniqueID = uniqueID.Value;

	}

	public UniqueItem(int id) {
		ID = id;

	}

	public static int GenerateUniqueID() {
		return
	}
}
