using Godot;
namespace OursGalaxy.UI;

internal partial class FleetsViewerPanel : MarginContainer {
	public static FleetsViewerPanel Load() => (FleetsViewerPanel)ResourceLoader.Load<PackedScene>("res://Scene/FleetsViewerPanel.tscn").Instantiate();

	[Export] private Button QuitButton { get; set; } = null!;
	[Export] private HBoxContainer FleetsPanel { get; set; } = null!;

	public void Render() {
		var i = 0;
		foreach (var fleet in GlobalViewModel.Fleets) {
			var box = FleetBox.Load();
			box.Render(i, fleet);
			FleetsPanel.AddChild(box, true);
			i++;
		}
		FleetsPanel.AddChild(NewFleetBox.Load(), true);
	}

	public override void _EnterTree() {
		base._EnterTree();
		QuitButton.Pressed += OnQuitPressed;
	}

	public override void _ExitTree() {
		QuitButton.Pressed -= OnQuitPressed;
		base._ExitTree();
	}

	private void OnQuitPressed() {
		PanelsStack.PopPanel(this);
	}
}
