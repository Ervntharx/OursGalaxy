using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class MessageItemBox : Button {
	public bool HasRead {
		get => hasRead; set {
			hasRead = value;
			// 第一次设置时可能在Ready之前
			if (ReadReferenceRect is not null) {
				ReadReferenceRect.Visible = hasRead;
			}
		}
	}

	[Export]
	protected RichTextLabel? TitleLabel { get; set; }

	/// <summary>
	/// 用于标记是否已读的描边框（暂借用ReferenceRect）
	/// </summary>
	[Export]
	protected ReferenceRect? ReadReferenceRect { get; set; }

	public void Make(MessageData msgData) {
		TitleLabel!.Text = msgData.Head;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private bool hasRead = false;
}
