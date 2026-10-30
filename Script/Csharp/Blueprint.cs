using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class Blueprint : GameItem {
	protected Blueprint() : base() { }

	public Blueprint(string name, ItemBase.QulityEnum qulity, int num = 0) : base("Blueprint", name, qulity, num) { }

	public Godot.Collections.Array<ItemBase> Source { get; set; } = [];
	public Godot.Collections.Array<ItemBase> Result { get; set; } = [];
}
