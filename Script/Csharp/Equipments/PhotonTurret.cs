using Godot;
namespace OursGalaxy;

internal partial class PhotonTurret : TurretBase {
	public static PhotonTurret Load() => (PhotonTurret)ResourceLoader.Load<PackedScene>("res://Scene/PhotonTurret.tscn").Instantiate();

	[Export]
	protected CollisionShape3D TrajectoryShape { get; set; } = null!;

	public override float Range {
		get => ((CylinderShape3D)TrajectoryShape.Shape).Height;
		set {
			var pos = TrajectoryShape.Position;
			pos.Z = value / 2;
			TrajectoryShape.Position = pos;
			((CylinderShape3D)TrajectoryShape.Shape).Height = value;
		}
	}

	protected override BulletBase LoadBullet() {
		var result = PhotonBullet.Load();
		result.Damage = Damage;
		return result;
	}
}
