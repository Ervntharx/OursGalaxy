using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class LaserConfig : TurretConfig {
	[Export] public float RotateCapacibility { get; set; } = 45;

	[Export] public int DamageStepCount { get; set; } = 1;
	[Export] public long DamageStepGapTimeMisc { get; set; } = 200;
}
