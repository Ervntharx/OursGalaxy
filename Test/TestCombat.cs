using Godot;
using OursGalaxy.UI;
using System;
namespace OursGalaxy;

internal partial class TestCombat : Node3D {
	[Export]
	protected CombatStage Stage { get; set; } = null!;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		Stage.Make(MakeLFleetData(), MakeRFleetData());
		Stage.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private FleetData MakeLFleetData() {
		var flagship = new SpaceshipData("TemplateShip") {
			CustomName = "left flag ship!",
			EquipsConfig = new() { }
		};
		var fleet = new FleetData(flagship);
		fleet.Name = "left fleet!";
		fleet.Ships = new(){
			{new SpaceshipData("TemplateShip") {
				CustomName = "left mem 1"
			}, new TacticsSet() {
				OffsetPosition = Vector3.Back*5
			}
			},
			{new SpaceshipData("TemplateShip") {
				CustomName = "left mem 2"
			}, new TacticsSet() {
				OffsetPosition = Vector3.Left*5
			}
			},
			{new SpaceshipData("TemplateShip") {
				CustomName = "left mem 3"
			}, new TacticsSet() {
				OffsetPosition = Vector3.Right*5
			}
			},
		};
		return fleet;
	}

	private FleetData MakeRFleetData() {
		var flagship = new SpaceshipData("TemplateShip");
		flagship.CustomName = "right flag ship!";
		var fleet = new FleetData(flagship);
		fleet.Name = "right fleet!";
		fleet.Ships = new() {
			{new SpaceshipData("TemplateShip") {
				CustomName = "right mem 1"
			},new TacticsSet() {
				OffsetPosition = Vector3.Right*5
			}
			},
			{new SpaceshipData("TemplateShip") {
				CustomName = "right mem 2"
			},new TacticsSet() {
				OffsetPosition = Vector3.Left*5
			}
			},
		};
		return fleet;
	}
}
