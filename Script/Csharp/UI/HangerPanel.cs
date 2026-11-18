using Godot;
using OursGalaxy.ViewModel;
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
		VM.SelectShipEvent += OnSelectShip;
		GlobalDataSource.Source.RepositoryAddShipEvent += OnShipAdd;
		GlobalDataSource.Source.RepositoryRemoveShipEvent += OnShipRemove;
		RefreshShips();
	}

	public override void _ExitTree() {
		QuitButton.Pressed -= OnQuitPressed;
		VM.SelectShipEvent -= OnSelectShip;
		GlobalDataSource.Source.RepositoryAddShipEvent -= OnShipAdd;
		GlobalDataSource.Source.RepositoryRemoveShipEvent -= OnShipRemove;
		base._ExitTree();
	}

	private void RefreshShips() {
		foreach (var child in ShipsPanel.GetChildren()) {
			ShipsPanel.RemoveChild(child);
			child.QueueFree();
		}
		foreach (var ship in GlobalDataSource.Source.GetShips()) {
			var box = SpaceshipBox.Load();
			box.Render(VM, ship);
			ShipsPanel.AddChild(box, true);
		}
	}

	private void OnSelectShip(SpaceshipData? ship) {
		if (ship is null) {
			return;
		}
		var editor = ShipEditorPanel.Load();
		editor.Render(ship, GlobalDataSource.Source.GetMainRepository());
		PanelsStack.PushPanel(editor);
	}

	private void OnShipAdd(SpaceshipData ship) {
		var box = SpaceshipBox.Load();
		box.Render(VM, ship);
		ShipsPanel.AddChild(box, true);
	}

	private void OnShipRemove(SpaceshipData ship) {
		foreach (var box in ShipsPanel.GetChildren().OfType<SpaceshipBox>()) {
			if (box.Ship?.Equals(ship) is true) {
				ShipsPanel.RemoveChild(box);
				box.QueueFree();
			}
		}
	}

	private void OnQuitPressed() => PanelsStack.PopPanel(this);
}
