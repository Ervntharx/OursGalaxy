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
		fleet.Repository.Add(mem3);
		fleet.Ships.Add(mem3.MakeReference(), new() {
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
		var mem1 = new SpaceshipData(SpaceshipConfig.TEMPLATE_NAMECODE, ItemBaseConfig.QulityEnum.White) {
			CustomName = $"{fleetName} mem 1"
		};
		var mem2 = new SpaceshipData(SpaceshipConfig.TEMPLATE_NAMECODE, ItemBaseConfig.QulityEnum.White) {
			CustomName = $"{fleetName} mem 2"
		};

		var ph1 = new TurretData("Equip/Turret/Photon", "Template", ItemBaseConfig.QulityEnum.White);
		var ph2 = new TurretData("Equip/Turret/Photon", "Template", ItemBaseConfig.QulityEnum.White);

		var fleet = new FleetData(flagship) {
			Name = fleetName
		};

		fleet.Repository.Add(mem1);
		fleet.Repository.Add(mem2);

		fleet.Repository.Add(ph1);
		fleet.Repository.Add(ph2);

		mem1.EquipsLs.Add("L0", ph1.MakeReference());
		fleet.Ships.Add(mem1.MakeReference(), new() {
			OffsetPosition = Godot.Vector3.Right * 5
		});
		mem2.EquipsLs.Add("L0", ph2.MakeReference());
		fleet.Ships.Add(mem2.MakeReference(), new() {
			OffsetPosition = Godot.Vector3.Left * 5
		});

		return fleet;
	}
}
