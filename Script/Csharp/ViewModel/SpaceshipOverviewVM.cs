using System;
using Godot;
namespace OursGalaxy.ViewModel;

[GlobalClass]
internal partial class SpaceshipOverviewVM : Node {
	public event Action<SpaceshipData?>? SelectShipEvent;

	public SpaceshipData? SelectShip {
		get => selectShip;
		set {
			selectShip = value;
			SelectShipEvent?.Invoke(selectShip);
		}
	}

	public override void _EnterTree() {
		GlobalDataSource.Source.RepositoryAddShipEvent += CheckSelectShipHasRemoved;
		base._EnterTree();
	}

	public override void _ExitTree() {
		GlobalDataSource.Source.RepositoryAddShipEvent -= CheckSelectShipHasRemoved;
		base._ExitTree();
	}

	private void CheckSelectShipHasRemoved(SpaceshipData ship) {
		if (selectShip?.Equals(ship) is true) {
			SelectShip = null;
		}
	}

	private SpaceshipData? selectShip;
}
