namespace OursGalaxy;

internal record class SpaceshipSnapshot {
	public static readonly SpaceshipSnapshot Empty = new();

	public long UniqueID { get; init; }

	public string TypeName { get; init; }

	public string? CustomName { get; init; }

	public int HP { get; init; }

	private SpaceshipSnapshot() {
		UniqueID = 0;
		TypeName = string.Empty;
	}

	public SpaceshipSnapshot(Spaceship ship) {
		UniqueID = ship.UniqueID;
		TypeName = ship.TypeName;
		CustomName = ship.CustomName;
		HP = ship.CurrentHealth;
	}

	public bool IsFrom(Spaceship ship) => UniqueID == ship.UniqueID;
}
