using Godot;
namespace OursGalaxy.UI;

internal partial class NewFleetBox : Button {
	public static NewFleetBox Load() => (NewFleetBox)ResourceLoader.Load<PackedScene>("res://Scene/NewFleetBox.tscn").Instantiate();

	public override void _EnterTree() {
		base._EnterTree();
		Pressed += OnPressed;
	}

	public override void _ExitTree() {
		Pressed -= OnPressed;
		base._ExitTree();
	}

	private void OnPressed() {
		PanelsStack.PushPanel(FleetEditorPanel.Load());
	}
}
