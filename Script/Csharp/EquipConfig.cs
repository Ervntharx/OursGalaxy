using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class EquipConfig : ItemBaseConfig {
	[Export] public OutletSizeType SizeType { get; set; } = OutletSizeType.Air;
}
