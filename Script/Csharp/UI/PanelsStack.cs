using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class PanelsStack : CanvasLayer {
	public PanelsStack Load() => (PanelsStack)ResourceLoader.Load<PackedScene>("res://Script/Csharp/UI/PanelsStack.cs").Instantiate();

	[Export]
	protected CanvasLayer PanelsRoot { get; set; } = null!;

	[Export]
	protected CanvasLayer ExcludsiveLayer { get; set; } = null!;

	[Export]
	protected ConfirmationDialog QuitGameDialog { get; set; } = null!;

	public Control? ActivePanel {
		get {
			return GodotObject.IsInstanceValid(value) && ActivePanelIdx != -1 ? (Control)value.PanelsRoot.GetChild(ActivePanelIdx) : null;
		}
	}
	public int ActivePanelIdx { get; protected set; } = -1;

	public PanelsStack() {
		if (GodotObject.IsInstanceValid(value)) {
			return;
		}
		value = this;
	}

	public static void PushPanel(Control newPanel) {
		if (GodotObject.IsInstanceValid(value)) {
			value.PanelsRoot.AddChild(newPanel);
			newPanel.Show();
			if (GodotObject.IsInstanceValid(value.ActivePanel)) {
				GD.Print(value.ActivePanelIdx);
				value.ActivePanel.Hide();
			}
			value.ActivePanelIdx += 1;
			GD.Print(value.ActivePanelIdx);
			return;
		}
		throw new InvalidOperationException("PanelsStack is null");
	}

	public static void PopPanel(Control oldPanel) {
		if (GodotObject.IsInstanceValid(value)) {
			value.PanelsRoot.RemoveChild(oldPanel);
			value.ActivePanelIdx -= 1;
			if (GodotObject.IsInstanceValid(value.ActivePanel)) {
				value.ActivePanel.Show();
			}
			return;
		}
		throw new InvalidOperationException("PanelsStack is null");
	}

	public static void PushExcludsivePanel(Control excludsivePanel) {
		if (GodotObject.IsInstanceValid(value)) {
			value.ExcludsiveLayer.AddChild(excludsivePanel);
			return;
		}
		throw new InvalidOperationException("PanelsStack is null");
	}

	public static void RequireQuitGame() {
		if (GodotObject.IsInstanceValid(value)) {
			value.QuitGameDialog.Show();
			return;
		}
		throw new InvalidOperationException("PanelsStack is null");
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		QuitGameDialog.Confirmed += () => GetTree().Root.QueueFree(); ;
	}

	private static PanelsStack? value;
}
