using Godot;
using OursGalaxy.ResourceConfig;
using System;
namespace OursGalaxy.UI;

/// <summary>
/// 飞船装配编辑面板
/// </summary>
internal partial class ShipConfigPanel : MarginContainer {
	public static ShipConfigPanel Load() => (ShipConfigPanel)ResourceLoader.Load<PackedScene>("res://Scene/ShipConfigPanel.tscn").Instantiate();

	[Export] protected Button QuitButton { get; set; } = null!;
	[Export] protected SubViewport ShipOverview { get; set; } = null!;
	[Export] protected Node3D ShipStage { get; set; } = null!;
	[Export] protected VBoxContainer EquipsListPanel { get; set; } = null!;

	public void Make(SpaceshipReference ship, SubRepository subRepository) {
		var shipData = subRepository.GetSpaceshipData(ship);
		var shipConfig = SpaceshipLib.GetConfig(shipData);
		foreach (var outlet in shipData.EquipsLs) {
			var outletUI = ShipConfigOutletBox.Load();
			outletUI.Make(shipConfig.OutletSet[outlet.Key]);

		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		base._Ready();
		QuitButton.Pressed += () => PanelsStack.PopPanel(this);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
