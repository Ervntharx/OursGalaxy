using Godot;
namespace OursGalaxy;

/// <summary>
/// 实体插槽类型
/// </summary>
[GlobalClass]
internal partial class Outlet : Marker3D {
	[Export]
	public string UniqueName { get; set; } = string.Empty;

	[Export]
	public OutletSizeType SizeType { get; set; } = OutletSizeType.Air;

	public EquipBase GetEquip() => GetChild<EquipBase>(0);
}
