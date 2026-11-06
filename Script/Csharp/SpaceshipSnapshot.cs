using System;
using Godot;
using OursGalaxy.ResourceConfig;
using OursGalaxy.UI;
namespace OursGalaxy;

internal record class SpaceshipSnapshot {
	public static readonly SpaceshipSnapshot Empty = new();

	public GUID ID { get; init; }

	public string TypeName { get; init; }

	public string? CustomName { get; init; }

	public CombatStage.TeamEnum Team { get; init; }

	public int HP { get; init; }

	public Transform3D Transform { get; init; }
	public SpaceshipConfig.ScaleSizeEnum ScaleSize { get; init; }

	public bool IsFlagShip { get; init; }

	private SpaceshipSnapshot() {
		ID = GUID.Empty;
		Team = CombatStage.TeamEnum.None;
		// CombatStage.TeamEnum.None;
		// FromFleetID = GUID.Empty;
		Team = CombatStage.TeamEnum.None;
		TypeName = string.Empty;
	}

	public SpaceshipSnapshot(Spaceship ship) {
		ID = ship.ID;
		TypeName = ship.ShipTypeName;
		CustomName = ship.CustomName;
		Team = ship.Team;
		HP = ship.CurrentHP;
		Transform = ship.Transform;
		ScaleSize = ship.ScaleSize;
	}

	public bool IsFrom(Spaceship ship) => ID == ship.ID;
}
