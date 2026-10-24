using Godot;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
namespace OursGalaxy.UI;

internal partial class FleetsViewer : Panel {
	[Export]
	protected HBoxContainer? FleetsPanel { get; set; }
	[Export]
	protected Button? Quit { get; set; }

	public ObservableCollection<FleetData> Fleets { get; private set; } = [];

	public void OnFleetsChanged(object? obj, System.Collections.Specialized.NotifyCollectionChangedEventArgs args) {
		foreach (var newObj in args.NewItems ?? new List<FleetData>() { }) {
			if (newObj is FleetData newFleet) {
				var newFleetBox = FleetBox.Load();
				GD.Print(Fleets.IndexOf(newFleet));
				newFleetBox.Make(Fleets.IndexOf(newFleet), newFleet);
				FleetsPanel!.AddChild(newFleetBox);
			}
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		Fleets.CollectionChanged += OnFleetsChanged;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	public override void _ExitTree() {
		Fleets.CollectionChanged -= OnFleetsChanged;
		base._ExitTree();
	}
}
