using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class MessageViewerPanel : Window {
	// [Export]
	// protected Button? BackButton { get; set; }

	// [Export]
	// protected RichTextLabel? MessageHeadLabel { get; set; }

	[Export]
	protected RichTextLabel? MessageBodyLabel { get; set; }

	// public string Head { get; set; } = string.Empty;
	// public string Content { get; set; } = string.Empty;

	public string Head { get => Title; set => Title = value; }
	public string Content { get => MessageBodyLabel!.Text; set => MessageBodyLabel!.Text = value; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		CloseRequested += QueueFree;
		// BackButton!.Pressed += () => Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
