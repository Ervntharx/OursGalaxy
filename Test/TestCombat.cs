using Godot;
using OursGalaxy.ResourceConfig;
using OursGalaxy.UI;
using System;
using System.Numerics;
namespace OursGalaxy;

internal partial class TestCombat : Node3D {
	[Export]
	protected CombatStage Stage { get; set; } = null!;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		var fleet = MakeFleetData("left");
		var mem3 = SpaceshipData.From(SpaceshipLib.GetTemplate());
		mem3.CustomName = $"{fleet.Name} mem 3";
		fleet.Ships.Add(mem3, new() {
			OffsetPosition = Godot.Vector3.Back * 5
		});
		Stage.Make(CombatStage.CampEnum.Left, fleet);
		Stage.Make(CombatStage.CampEnum.Right, MakeFleetData("right"));
		Stage.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private FleetData MakeFleetData(string fleetName) {
		var flagship = SpaceshipData.From(SpaceshipLib.GetTemplate());
		flagship.CustomName = $"{fleetName} flag ship!";
		var fleet = new FleetData(flagship) {
			Name = fleetName
		};
		var mem1 = SpaceshipData.From(SpaceshipLib.GetTemplate());
		mem1.CustomName = $"{fleetName} mem 1";
		fleet.Ships.Add(mem1, new() {
			OffsetPosition = Godot.Vector3.Right * 5
		});
		var mem2 = SpaceshipData.From(SpaceshipLib.GetTemplate());
		mem2.CustomName = $"{fleetName} mem 2";
		fleet.Ships.Add(mem2, new() {
			OffsetPosition = Godot.Vector3.Left * 5
		});

		return fleet;
	}
}
