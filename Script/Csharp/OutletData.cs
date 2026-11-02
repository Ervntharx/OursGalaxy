using Godot;
namespace OursGalaxy;

/// <summary>
/// 插槽数据类型
/// </summary>
[GlobalClass]
internal partial class OutletData : Resource {
	[Export] public string UniqueName { get; set; } = string.Empty;

	[Export] public OutletSizeType SizeType { get; set; } = OutletSizeType.Air;

	[Export] public EquipData? InstalledEquip { get; set; }
}
