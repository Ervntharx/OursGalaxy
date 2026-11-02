using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class EquipBase : Node3D {
	public static EquipBase Load() => (EquipBase)ResourceLoader.Load<PackedScene>("res://Res/Equips/TemplateEquip.tscn").Instantiate();

	[Export]
	public string Code { get; set; }
	[Export]
	public GUID ID { get; set; }

	protected EquipBase() { Code = string.Empty; ID = GUID.Empty; }
}
