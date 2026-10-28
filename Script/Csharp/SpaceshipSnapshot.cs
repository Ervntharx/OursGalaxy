using OursGalaxy.UI;
namespace OursGalaxy;

internal record class SpaceshipSnapshot {
	public static readonly SpaceshipSnapshot Empty = new();

	public GUID UniqueID { get; init; }

	public string TypeName { get; init; }

	public string? CustomName { get; init; }

	public CombatStage.CampEnum Camp { get; init; }

	public int HP { get; init; }

	private SpaceshipSnapshot() {
		UniqueID = GUID.Empty;
		TypeName = string.Empty;
	}

	public SpaceshipSnapshot(Spaceship ship) {
		UniqueID = ship.ID;
		TypeName = ship.TypeName;
		CustomName = ship.CustomName;
		Camp = ship.Camp;
		HP = ship.CurrentHealth;
	}

	public bool IsFrom(Spaceship ship) => UniqueID == ship.ID;
}
