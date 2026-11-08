using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class PhotonConfig : TurretConfig {
	public new const string ITEM_CODE_PREFIX = "Equip/Turret/Photon";

	public const float DEFAULT_LINEAR_VELOCITY = 5;

	[Export] public float RotateCapacibility { get; set; }

	[Export] public int ShootTime { get; set; } = 1;
	[Export] public ulong ShootGapTimeMisc { get; set; } = 200;

	[Export] public float LinearVelocity { get; set; } = DEFAULT_LINEAR_VELOCITY;
}
