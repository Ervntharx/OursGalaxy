using Godot;
namespace OursGalaxy.UI;

internal partial class Storehouse : MarginContainer {
	public static Storehouse Load() => (Storehouse)ResourceLoader.Load<PackedScene>("res://Scene/Storehouse.tscn").Instantiate();

	[Export] protected Button QuitButton { get; set; } = null!;
	[Export] protected HFlowContainer ItemsListPanel { get; set; } = null!;

	public override void _Ready() {
		base._Ready();
		QuitButton.Pressed += () => PanelsStack.PopPanel(this);
		Redraw();
		VisibilityChanged += Redraw;
	}

	public void Redraw() {
		foreach (var child in ItemsListPanel.GetChildren()) {
			child.QueueFree();
		}
		foreach (var item in GlobalViewModel.Data.GameItems) {
			var box = GameItemBox.Load();
			box.Make(item);
			ItemsListPanel.AddChild(box);
		}
	}
}
