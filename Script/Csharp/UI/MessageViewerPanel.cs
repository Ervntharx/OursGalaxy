using Godot;
using System;
namespace OursGalaxy.UI;

/// <summary>
/// 消息查看器界面
/// </summary>
internal partial class MessageViewerPanel : MarginContainer {
	public MessageViewerPanel Load() => (MessageViewerPanel)ResourceLoader.Load<PackedScene>("res://Scene/MessageViewPanel.tscn").Instantiate();
	[Export]
	protected Button? QuitButton { get; set; }

	[Export]
	protected RichTextLabel? MsgHeadLabel { get; set; }
	[Export]
	protected RichTextLabel? MsgBodyLabel { get; set; }

	/// <summary>
	/// 加载了消息或没有
	/// </summary>
	public bool IsLoadedMessgae { get; protected set; }

	public event Action<MessageViewerPanel>? Quit;

	public void Make(MessageData msg) {
		Head = msg.Head;
		Body = msg.Content;
		IsLoadedMessgae = true;
	}

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
		QuitButton!.Pressed += OnQuitPressed;
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

	public override void _ExitTree() {
		QuitButton!.Pressed -= OnQuitPressed;
		base._ExitTree();
	}

	private void OnQuitPressed() {
		Head = "邮件头";
		Body = "内容";
		IsLoadedMessgae = false;
		Quit?.Invoke(this);
	}

	private string head = string.Empty;
	private string body = string.Empty;
}
