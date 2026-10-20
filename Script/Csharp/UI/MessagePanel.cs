using Godot;
using System;

internal partial class MessagePanel : Panel {
	[Export]
	protected Button? BackButton { get; set; }

	[Export]
	protected RichTextLabel? MessageHeadLabel { get; set; }

	[Export]
	protected RichTextLabel? MessageBodyLabel { get; set; }

	public string Head { get; set; } = string.Empty;
	public string Content { get; set; } = string.Empty;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		BackButton!.Pressed += () => Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
