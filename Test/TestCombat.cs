using Godot;
using OursGalaxy.ResourceConfig;
using OursGalaxy.UI;
namespace OursGalaxy;

internal partial class TestCombat : Node3D {
	[Export]
	protected CombatStage Stage { get; set; } = null!;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		var fleet = MakeFleetData("left");
		var mem3 = new SpaceshipData(SpaceshipConfig.TEMPLATE_NAMECODE, ItemBaseConfig.QulityEnum.White) {
			CustomName = $"{fleet.Name} mem 3"
		};
		fleet.Ships.Add(mem3, new() {
			OffsetPosition = Godot.Vector3.Back * 5
		});
		Stage.PutOnFleet(CombatStage.TeamEnum.Left, fleet);
		Stage.PutOnFleet(CombatStage.TeamEnum.Right, MakeFleetData("right"));
		Stage.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private FleetData MakeFleetData(string fleetName) {
		var flagship = new SpaceshipData(SpaceshipConfig.TEMPLATE_NAMECODE, ItemBaseConfig.QulityEnum.White) {
			CustomName = $"{fleetName} flag ship!"
		};
		var fleet = new FleetData(flagship) {
			Name = fleetName
		};
		var mem1 = new SpaceshipData(SpaceshipConfig.TEMPLATE_NAMECODE, ItemBaseConfig.QulityEnum.White) {
			CustomName = $"{fleetName} mem 1"
		};
		mem1.EquipsLs.Add("L0", new TurretData("Equip/Turret/Photon", "Template", ItemBaseConfig.QulityEnum.White));
		fleet.Ships.Add(mem1, new() {
			OffsetPosition = Godot.Vector3.Right * 5
		});
		var mem2 = new SpaceshipData(SpaceshipConfig.TEMPLATE_NAMECODE, ItemBaseConfig.QulityEnum.White) {
			CustomName = $"{fleetName} mem 2"
		};
		mem2.EquipsLs.Add("L0", new TurretData("Equip/Turret/Photon", "Template", ItemBaseConfig.QulityEnum.White));
		fleet.Ships.Add(mem2, new() {
			OffsetPosition = Godot.Vector3.Left * 5
		});

		return fleet;
	}
}
