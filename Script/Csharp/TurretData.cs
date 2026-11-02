using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class TurretData : EquipData {
	[Export] public float Range { get; set; }
}
