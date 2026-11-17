using Godot;
using OursGalaxy.ViewModel;
using System;
using System.Linq;
namespace OursGalaxy.UI;

internal partial class HangerPanel : MarginContainer {
	public static HangerPanel Load() => (HangerPanel)ResourceLoader.Load<PackedScene>("res://Scene/HangerPanel.tscn").Instantiate();

	[Export] private Button QuitButton { get; set; } = null!;
	[Export] private HFlowContainer ShipsPanel { get; set; } = null!;
	[Export] public SpaceshipOverviewVM VM { get; set; } = null!;

	public override void _EnterTree() {
		base._EnterTree();
		QuitButton.Pressed += OnQuitPressed;
		foreach (var ship in GlobalDataSource.Source.GetShips()) {
			var shipBox = SpaceshipBox.Load();
			shipBox.Render(ship);
			ShipsPanel.AddChild(shipBox);
		}

		GlobalDataSource.Source.RepositoryAddShipEvent += OnShipAdd;
		GlobalDataSource.Source.RepositoryRemoveShipEvent += OnShipRemove;

		// 初始绘制
		foreach (var box in ShipsPanel.GetChildren().OfType<SpaceshipBox>()) {
			box.QueueFree();
		}
		foreach (var ship in GlobalDataSource.Source.GetShips()) {
			var box = SpaceshipBox.Load();
			box.Render(VM, ship);
			ShipsPanel.AddChild(box, true);
		}
	}

	public override void _ExitTree() {
		GlobalDataSource.Source.RepositoryAddShipEvent -= OnShipAdd;
		GlobalDataSource.Source.RepositoryRemoveShipEvent -= OnShipRemove;
		base._ExitTree();
	}

	private void OnShipAdd(SpaceshipData ship) {
		var shipBox = SpaceshipBox.Load();
		shipBox.Render(VM, ship);
		ShipsPanel.AddChild(shipBox, true);
	}

	private void OnShipRemove(SpaceshipData ship) {
		foreach (var shipBox in ShipsPanel.GetChildren().OfType<SpaceshipBox>()) {
			if (shipBox.Ship?.Equals(ship) is true) {
				shipBox.RemoveChild(shipBox);
			}
		}
	}

	private void OnQuitPressed() {
		PanelsStack.PopPanel(this);
	}
}
