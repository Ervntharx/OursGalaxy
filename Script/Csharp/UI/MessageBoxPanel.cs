using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
namespace OursGalaxy.UI;

internal partial class MessageBoxPanel : PanelContainer {
	[Export]
	protected TabContainer? MessagesTabsContainer { get; set; }
	[Export]
	protected PackedScene? MessageItemBox { get; set; }

	[Export]
	protected PackedScene? MessageViewer { get; set; }

	public IEnumerable<string> MessageTabs => MessagesTabsContainer!.GetChildren().Select(tab => tab.Name.ToString());

	public event Action<MessageData>? OpenMessage;

	public void AddTab(params string[] tabsNames) {
		foreach (var tabName in tabsNames) {
			if (!MessagesTabsContainer!.HasNode(tabName)) {
				var scroller = new ScrollContainer() { Name = tabName, };
				scroller.AddChild(new VBoxContainer() { Name = SCROLL_VBOX_CONTAINER_NODE_NAME, SizeFlagsHorizontal = SizeFlags.ExpandFill });
				MessagesTabsContainer!.AddChild(scroller);
			}
		}
	}

	public void RemoveTab(params string[] tabNames) {
		foreach (var tabName in tabNames) {
			if (MessagesTabsContainer!.HasNode(tabName)) {
				MessagesTabsContainer!.RemoveChild(MessagesTabsContainer!.GetNode(tabName));
			}
		}
	}

	public bool TryPushMessage(string tabName, params MessageData[] msgs) {
		var tab = MessagesTabsContainer!.GetNodeOrNull<VBoxContainer?>($"{tabName}/{SCROLL_VBOX_CONTAINER_NODE_NAME}");
		foreach (var msg in msgs) {
			var msgItem = MessageItemBox!.Instantiate() as MessageItemBox;
			msgItem!.Make(msg);
			msgItem!.Pressed += () => { OpenMessage?.Invoke(msg); };
			tab!.AddChild(msgItem);
		}
		return true;
	}

	private const string SCROLL_VBOX_CONTAINER_NODE_NAME = "VBox";

	public override void _Ready() {
		base._Ready();
		AddTab("Personal", "Notice");

		GD.Print(TryPushMessage("Notice",
		new MessageData() { Head = "Welcome", Content = "here" },
		 new MessageData() { Head = "Again", Content = "here" },
		 new MessageData() { Head = "Again", Content = "here" },
		 new MessageData() { Head = "ttwh", Content = "here" },
		 new MessageData() { Head = "Again", Content = "here" },
		 new MessageData() { Head = "Again", Content = "here" },
		 new MessageData() { Head = "aerg", Content = "here" },
		 new MessageData() { Head = "Again", Content = "here" },
		 new MessageData() { Head = "Again", Content = "here" },
		 new MessageData() { Head = "Again", Content = "here" },
		 new MessageData() { Head = "arb", Content = "here" },
		 new MessageData() { Head = "Again", Content = "here" },
		 new MessageData() { Head = "abaetbeynetyn", Content = "here" },
		 new MessageData() { Head = "Again", Content = "here" }
		 ));

		OpenMessage += (msgData) => {
			GD.Print("try open;");
			var msgVP = (MessageViewer!.Instantiate() as MessageViewerPanel)!;
			msgVP.Head = msgData.Head;
			msgVP.Content = msgData.Content;
			AddChild(msgVP);
		};
	}
}
