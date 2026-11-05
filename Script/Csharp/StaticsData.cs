using System;
using System.Collections.Generic;
using Godot;
using OursGalaxy.ResourceConfig;
using OursGalaxy.UI;
namespace OursGalaxy;

internal partial class StaticsData {
	public Dictionary<CombatStage.TeamEnum, int> FullHP { get; } = new() {
		{CombatStage.TeamEnum.Left, 0},
		{CombatStage.TeamEnum.Right, 0}
	};

	public Dictionary<CombatStage.TeamEnum, int> HP { get; } = new() {
		{CombatStage.TeamEnum.Left, 0},
		{CombatStage.TeamEnum.Right, 0}
	};

	public Dictionary<CombatStage.TeamEnum, Dictionary<SpaceshipConfig.ScaleSizeEnum, int>> ShipsCount { get; } = new() {
		{
			CombatStage.TeamEnum.Left,
			new() {
				{SpaceshipConfig.ScaleSizeEnum.BB, 0},
				{SpaceshipConfig.ScaleSizeEnum.CA, 0},
				{SpaceshipConfig.ScaleSizeEnum.CL, 0},
				{SpaceshipConfig.ScaleSizeEnum.DD, 0},
				{SpaceshipConfig.ScaleSizeEnum.FF, 0},
			}
		},
		{
			CombatStage.TeamEnum.Right,
			new() {
				{SpaceshipConfig.ScaleSizeEnum.BB, 0},
				{SpaceshipConfig.ScaleSizeEnum.CA, 0},
				{SpaceshipConfig.ScaleSizeEnum.CL, 0},
				{SpaceshipConfig.ScaleSizeEnum.DD, 0},
				{SpaceshipConfig.ScaleSizeEnum.FF, 0},
			}
		}
	};

	public Dictionary<CombatStage.TeamEnum, Dictionary<SpaceshipConfig.ScaleSizeEnum, int>> ShipsFullCount { get; } = new() {
		{
			CombatStage.TeamEnum.Left,
			new() {
				{SpaceshipConfig.ScaleSizeEnum.BB, 0},
				{SpaceshipConfig.ScaleSizeEnum.CA, 0},
				{SpaceshipConfig.ScaleSizeEnum.CL, 0},
				{SpaceshipConfig.ScaleSizeEnum.DD, 0},
				{SpaceshipConfig.ScaleSizeEnum.FF, 0},
			}
		},
		{
			CombatStage.TeamEnum.Right,
			new() {
				{SpaceshipConfig.ScaleSizeEnum.BB, 0},
				{SpaceshipConfig.ScaleSizeEnum.CA, 0},
				{SpaceshipConfig.ScaleSizeEnum.CL, 0},
				{SpaceshipConfig.ScaleSizeEnum.DD, 0},
				{SpaceshipConfig.ScaleSizeEnum.FF, 0},
			}
		}
	};

	public Dictionary<CombatStage.TeamEnum, bool> IsFlagshipAlive { get; } = new() {
		{CombatStage.TeamEnum.Left,false},
		{CombatStage.TeamEnum.Right,false}
	};

	public void Track(Spaceship ship) {
		FullHP[ship.Team] += ship.HP;
		HP[ship.Team] += ship.CurrentHP;
		GD.Print("Tracking	", HP[ship.Team]);
		ShipsFullCount[ship.Team][ship.ScaleSize] += 1;
		ShipsCount[ship.Team][ship.ScaleSize] += 1;
		if (ship.IsFlagShip) {
			IsFlagshipAlive[ship.Team] = true;
		}

		ship.OnEntityDestory += OnShipDestory;
		ship.OnHPChanged += OnShipHPChanged;

		StaticsDataChanged?.Invoke(this);
	}

	public event Action<StaticsData>? StaticsDataChanged;

	private void OnShipHPChanged(SpaceshipSnapshot shot, int currentHP, int hpChanged) {
		HP[shot.Team] += hpChanged;
		StaticsDataChanged?.Invoke(this);
	}

	private void OnShipDestory(SpaceshipSnapshot shot) {
		ShipsCount[shot.Team][shot.ScaleSize] -= 1;
		StaticsDataChanged?.Invoke(this);
	}
}
