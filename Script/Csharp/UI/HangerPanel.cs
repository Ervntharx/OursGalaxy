using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class HangerPanel : MarginContainer {
	public static HangerPanel Load() => (HangerPanel)ResourceLoader.Load<PackedScene>("res://Scene/HangerPanel.tscn").Instantiate();

	[Export] protected Button QuitButton { get; set; } = null!;
	[Export] protected HFlowContainer ShipsPanel { get; set; } = null!;

	public override void _Ready() {
		base._Ready();
		QuitButton.Pressed += () => PanelsStack.PopPanel(this);
		VisibilityChanged += Redraw;
	}

	public void Redraw() {
		if (Visible) {
			foreach (var child in ShipsPanel.GetChildren()) {
				child.QueueFree();
			}
			foreach (var ship in GlobalViewModel.Data.Ships) {
				var shipBox = SpaceshipBox.Load();
				shipBox.Make(ship);
				ShipsPanel.AddChild(shipBox);
			}
		}
	}
}
