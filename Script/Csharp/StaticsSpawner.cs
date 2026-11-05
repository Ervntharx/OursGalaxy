using System;
using System.Collections.Generic;
using Godot;
using OursGalaxy.ResourceConfig;
using OursGalaxy.UI;
namespace OursGalaxy;

internal partial class StaticsSpawner {
	public Dictionary<CombatStage.TeamEnum, int> FleetFullHP { get; } = [];
	public Dictionary<CombatStage.TeamEnum, int> FleetHP { get; } = [];

	public Dictionary<CombatStage.TeamEnum, int> ShipsFullCount { get; } = [];
	public Dictionary<CombatStage.TeamEnum, Dictionary<SpaceshipConfig.ScaleSizeEnum, int>> ShipsCount { get; } = [];

	public Dictionary<CombatStage.TeamEnum, bool> FlagAliveStatus { get; set; } = [];

	public event Action<StaticsSpawner>? Updated;

	public void Track(IBattleEntity battleEntity) {
		if (FleetFullHP.Keys.Contains(battleEntity.Team)) {
			FleetFullHP[battleEntity.Team] += 1;
		}
		else {
			FleetFullHP.Add(battleEntity.Team, 1);
		}
		if (ShipsFullCount.Keys.Contains(battleEntity.Team)) {
			ShipsFullCount[battleEntity.Team] += 1;
		}
		else {
			ShipsFullCount.Add(battleEntity.Team, 1);
		}

		if (!FlagAliveStatus.ContainsKey(battleEntity.Team)) {
			FlagAliveStatus[battleEntity.Team] = false;
		}
		if (battleEntity.IsFlagShip) {
			FlagAliveStatus[battleEntity.Team] = true;
		}

		battleEntity.OnHPChanged += OnHPChanged;
		battleEntity.OnEntityDestory += OnEntityDead;
	}

	private void OnHPChanged(SpaceshipSnapshot shot, int currentHP, int hpChanged) {
		FleetHP[shot.Team] += hpChanged;
		Updated?.Invoke(this);
	}

	private void OnEntityDead(SpaceshipSnapshot shot) {
		ShipsCount[shot.Team][shot.ScaleSize] -= 1;
		if (shot.IsFlagShip) {
			FlagAliveStatus[shot.Team] = false;
		}
		Updated?.Invoke(this);
	}
}
