using Godot;
namespace OursGalaxy;

[GlobalClass]
internal abstract partial class EquipBase : Node3D {
	[Export] public string Code { get; set; }
	[Export] public GUID ID { get; set; }

	[Export] public OutletSizeType SizeType { get; set; } = OutletSizeType.Air;

	protected EquipBase() { Code = string.Empty; ID = GUID.Empty; }
}
