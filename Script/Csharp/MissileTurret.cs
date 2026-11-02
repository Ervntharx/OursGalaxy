using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class MissileTurretConfig : TurretConfig {
	[Export] public bool VLF = false;
	[Export] public float Step1TrackCapability { get; set; } = 45;
	[Export] public float Step2TrackCapability { get; set; } = 15;
}
