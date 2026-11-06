using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class MissileConfig : TurretConfig {
	[Export] public bool VLF { get; set; } = false;

	[Export] public int ShootTime { get; set; } = 1;
	[Export] public long ShootGapTimeMisc { get; set; } = 200;

	[Export] public float Step1TrackCapability { get; set; } = 45;
	[Export] public float Step2TrackCapability { get; set; } = 15;
}
