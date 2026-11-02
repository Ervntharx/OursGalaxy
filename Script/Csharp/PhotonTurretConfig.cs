using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class PhotonTurretConfig : TurretConfig {
	[Export]
	public float RotateCapacibility { get; set; } = 45;
}
