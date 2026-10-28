using Godot;
namespace OursGalaxy;

/// <summary>
/// 弱引用，仅记录静态GUID
/// </summary>
[GlobalClass]
internal partial class UniqueItemWeakReference : Resource {
	[Export]
	public GUID IDRefer { get; set; }

	[Export]
	public string Code { get; set; }

	public UniqueItemWeakReference(UniqueItem uItem) {
		IDRefer = uItem.ID;
		Code = uItem.Code;
	}
}
