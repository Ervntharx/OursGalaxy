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

	public event Action<CombatStage.TeamEnum, int>? FleetHPChanged;
	public event Action<CombatStage.TeamEnum, int>? ShipsCountChanged;

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

		battleEntity.OnHPChanged += OnHPChanged;
		battleEntity.OnEntityDestory += OnEntityDead;
	}

	private void OnHPChanged(SpaceshipSnapshot shot, int currentHP, int hpChanged) {
		FleetHP[shot.Team] += hpChanged;
	}

	private void OnEntityDead(SpaceshipSnapshot shot) {
		ShipsCount[shot.Team][shot.ScaleSize] -= 1;
	}
}
