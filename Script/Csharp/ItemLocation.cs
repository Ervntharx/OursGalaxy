using Godot;
namespace OursGalaxy;

/// <summary>
/// UniqueItem被占用的具体位置信息。若需要则直接override并重写ToString。UniqueItem只检查null与否
/// </summary>
[GlobalClass]
internal partial class ItemLocationInf : Resource {
	public ItemLocationInf() { }

	public override string ToString() {
		return "被占用";
	}
}
