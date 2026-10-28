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

	public PanelsStack() {
		if (GodotObject.IsInstanceValid(value)) {
			return;
		}
		value = this;
	}

	public static void PushPanel(Control newPanel) {
		if (GodotObject.IsInstanceValid(value)) {
			value.PanelsRoot.AddChild(newPanel);
		}
		throw new InvalidOperationException("PanelsStack is null");
	}

	public static void PopPanel(Control oldPanel) {
		if (GodotObject.IsInstanceValid(value)) {
			value.PanelsRoot.RemoveChild(oldPanel);
		}
		throw new InvalidOperationException("PanelsStack is null");
	}

	public static void AddExcludsivePanel(Control excludsivePanel) {
		if (GodotObject.IsInstanceValid(value)) {
			value.ExcludsiveLayer.AddChild(excludsivePanel);
		}
		throw new InvalidOperationException("PanelsStack is null");
	}

	public static void RequireQuitGame() {
		if (GodotObject.IsInstanceValid(value)) {
			value.QuitGameDialog.Show();
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		QuitGameDialog.Confirmed += OnComfirmQuitGame;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	public override void _ExitTree() {
		QuitGameDialog.Confirmed -= OnComfirmQuitGame;
		base._ExitTree();
	}

	private void OnComfirmQuitGame() {
		GetTree().Root.QueueFree();
	}

	private static PanelsStack? value;
}
