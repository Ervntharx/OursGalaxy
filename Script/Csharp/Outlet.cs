using Godot;
namespace OursGalaxy;

/// <summary>
/// 实体插槽类型
/// </summary>
[GlobalClass]
internal partial class Outlet : Marker3D {
	/// <summary>
	/// Outlet Unique Name 以Node.Name替代
	/// </summary>
	[Export]
	public string UniqueName { get => Name; set => Name = value; }

	[Export]
	public OutletSizeType SizeType { get; set; } = OutletSizeType.Air;

	public void Install(EquipData data) {
		AddChild(EquipFactory.Load(data), true);
	}

	public EquipBase GetEquipOrNull() => GetChild<EquipBase>(0);

	private const string EQUIP_NODE_NAME = "Equip";
}
