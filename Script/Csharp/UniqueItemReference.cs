using System.Runtime.CompilerServices;
using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class UniqueItemReference : Resource {
	[Export]
	public GUID IDReference { get; protected set; }

	[Export]
	public ItemBase IBase { get; set; }

	public UniqueItemReference(UniqueItem uItem) {
		IDReference = uItem.ID;
		IBase = new(uItem.ITypeCode, uItem.NameCode, uItem.Qulity);
	}
}
