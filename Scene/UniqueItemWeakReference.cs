using System;
using Godot;
namespace OursGalaxy;

/// <summary>
/// 弱引用，仅记录静态GUID
/// </summary>
[GlobalClass]
[Obsolete("不必要类型")]
internal partial class UniqueItemWeakReference : Resource {
	[Export] public GUID IDRefer { get; set; }

	public UniqueItemWeakReference(UniqueItem uItem) {
		IDRefer = uItem.ID;
	}
}
