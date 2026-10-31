using Godot;
using System;
namespace OursGalaxy;

internal partial class PhotonBullet : BulletBase {
	public new static PhotonBullet Load() => (PhotonBullet)ResourceLoader.Load<PackedScene>("res://Scene/Bullets/PhotonBullet.tscn").Instantiate();
}
