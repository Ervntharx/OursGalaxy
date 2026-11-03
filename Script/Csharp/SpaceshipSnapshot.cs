using Godot;
using OursGalaxy.UI;
namespace OursGalaxy;

internal record class SpaceshipSnapshot {
	public static readonly SpaceshipSnapshot Empty = new();

	public GUID UniqueID { get; init; }

	public string TypeName { get; init; }

	public string? CustomName { get; init; }

	public GUID FromFleetID { get; init; }

	public int HP { get; init; }

	public Transform3D Transform { get; init; }
	public Spaceship.ScaleSizeEnum ScaleSize { get; init; }

	public bool IsFlagShip { get; init; }

	private SpaceshipSnapshot() {
		UniqueID = GUID.Empty;
		FromFleetID = GUID.Empty;
		TypeName = string.Empty;
	}

	public SpaceshipSnapshot(Spaceship ship) {
		UniqueID = ship.ID;
		TypeName = ship.TypeName;
		CustomName = ship.CustomName;
		FromFleetID = ship.FromFleetID;
		HP = ship.CurrentHP;
		Transform = ship.Transform;
		ScaleSize = ship.ScaleSize;
		IsFlagShip = ship.IsFlagShip;
	}

	public bool IsFrom(Spaceship ship) => UniqueID == ship.ID;
}
