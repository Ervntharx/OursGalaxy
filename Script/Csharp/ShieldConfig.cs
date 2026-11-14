using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class ShieldConfig : EquipConfig {
	[Export] public int HP { get; set; }
	[Export] public long CDMisc { get; set; }

	[Export] public int Impact { get; set; }

	[Export] public int Penetrate { get; set; }

	[Export] public int Photothermal { get; set; }

	[Export] public int Corrosion { get; set; }

	[Export] public int Electromagnetic { get; set; }
}
