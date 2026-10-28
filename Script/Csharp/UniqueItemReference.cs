using System.Runtime.CompilerServices;
using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class UniqueItemReference : Resource {
	[Export]
	public GUID IDReference { get; protected set; }
	[Export]
	public string Code { get; protected set; }

	public UniqueItemReference(UniqueItem uItem) {
		IDReference = uItem.ID;
		Code = uItem.Code;
	}

	/// <summary>
	/// 得到的是副本！
	/// </summary>
	/// <returns></returns>
	public ItemBase GetItem() => new(Code);
}
