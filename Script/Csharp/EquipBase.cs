using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

internal abstract partial class EquipBase : Node3D {
	[Export] public string ITypeCode { get; set; } = string.Empty;
	[Export] public string NameCode { get; set; } = string.Empty;
	[Export] public ItemBaseConfig.QulityEnum Qulity { get; set; }

	[Export] public GUID ID { get; set; } = GUID.Empty;
	[Export] public GUID From { get; set; } = GUID.Empty;

	[Export] public OutletSizeType SizeType { get; set; } = OutletSizeType.Air;

	protected EquipBase() { }
}
