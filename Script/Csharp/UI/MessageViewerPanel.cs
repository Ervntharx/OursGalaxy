using Godot;
using System;
namespace OursGalaxy.UI;

/// <summary>
/// 消息查看器界面
/// </summary>
internal partial class MessageViewerPanel : MarginContainer {

	[Export]
	protected RichTextLabel? MsgHeadLabel { get; set; }
	[Export]
	protected RichTextLabel? MsgBodyLabel { get; set; }

	// public string Head { get; set; } = string.Empty;
	// public string Content { get; set; } = string.Empty;

	public string Head {
		get => head; set {
			head = value;
			if (GodotObject.IsInstanceValid(MsgHeadLabel) && MsgHeadLabel.IsNodeReady()) {
				MsgHeadLabel.Text = Head;
			}
		}
	}

	public string Body {
		get => body; set {
			body = value;
			if (GodotObject.IsInstanceValid(MsgBodyLabel) && MsgBodyLabel.IsNodeReady()) {
				MsgBodyLabel.Text = Body;
			}
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		if (GodotObject.IsInstanceValid(MsgHeadLabel) && MsgHeadLabel.IsNodeReady()) {
			MsgHeadLabel.Text = Head;
		}
		if (GodotObject.IsInstanceValid(MsgBodyLabel) && MsgBodyLabel.IsNodeReady()) {
			MsgBodyLabel.Text = Body;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private string head = string.Empty;
	private string body = string.Empty;
}
