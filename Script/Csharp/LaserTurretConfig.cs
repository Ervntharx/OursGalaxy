using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class LaserTurretConfig : TurretConfig {
	[Export] public float RotateCapacibility { get; set; } = 45;
}
