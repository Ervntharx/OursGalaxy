using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

[GlobalClass]
internal partial class Blueprint : GameItem {
	protected Blueprint() : base() { }

	public Blueprint(string name, ItemBaseConfig.QulityEnum qulity, int num = 0) : base(BlueprintConfig.ITEM_CODE_PREFIX, name, qulity, num) { }

	public Godot.Collections.Array<ItemBase> Source { get; set; } = [];
	public Godot.Collections.Array<ItemBase> Result { get; set; } = [];
}
