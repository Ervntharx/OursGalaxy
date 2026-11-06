using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class EquipConfig : ItemBaseConfig {
	public const string ITEM_CODE_PREIX = "Equip";

	[Export] public OutletSizeType SizeType { get; set; } = OutletSizeType.Air;

	[Export] public Godot.Collections.Array<string> StrengthenItemMap { get; set; } = [];
}
