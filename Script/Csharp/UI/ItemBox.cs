using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
namespace OursGalaxy.UI;

internal partial class ItemBox : Button {
	public static ItemBox Load() {
		return (ItemBox)ResourceLoader.Load<PackedScene>("res://Scene/ItemBox.tscn").Instantiate();
	}

	[Export]
	protected Button? DefaultButton { get; set; }
	[Export]
	protected TextureRect? PicTexture { get; set; }
	[Export]
	protected RichTextLabel? NameLabel { get; set; }
	[Export]
	protected Label? NumLabel { get; set; }
	[Export]
	protected FoldableContainer? FC { get; set; }
	[Export]
	protected VBoxContainer? FoldOptionsVBox { get; set; }

	public ItemBase Item { get => item; set => Make(value, "选择"); }

	public event Action<string>? PressedOption;

	public void Make(ItemBase itemValue, string defaultOpt, params string[] opts) {
		item = itemValue;
		itemValue.PropertyChanged += Update;
		NameLabel!.Text = $"[color=gold]{itemValue.Code}[/color]";
		if (itemValue is OursGalaxy.GameItem mItem) {
			NumLabel!.Show();
			NumLabel!.Text = $"{mItem.Num}";
		}
		else {
			NumLabel!.Hide();
			NumLabel!.Text = string.Empty;
		}

		foreach (var connection in DefaultButton!.GetSignalConnectionList(Button.SignalName.Pressed)) {
			if (connection["callable"].Obj is Callable callable) {
				DefaultButton!.Disconnect(Button.SignalName.Pressed, callable);
			}
		}

		DefaultButton!.Pressed += OnDefaultButtonPressed;
		DefaultButton!.Text = defaultOpt;
		FoldOptionsVBox!.GetChildren().ToList().ForEach(child => child.QueueFree());
		foreach (var opt in opts) {
			var optButton = new Button {
				Text = opt
			};
			optButton.Pressed += () => PressedOption?.Invoke(opt);
			FoldOptionsVBox!.AddChild(optButton);
		}

		void OnDefaultButtonPressed() {
			PressedOption?.Invoke(defaultOpt);
		}
	}

	private void Update(ItemBase itemBase, string _) {
		NameLabel!.Text = itemBase.Code;
		if (itemBase is GameItem gameItem) {
			NumLabel!.Text = $"{gameItem.Num}";
		}
	}

	// private void Update(ItemBase itemBase, System.ComponentModel.PropertyChangedEventArgs _) {
	// 	NameLabel!.Text = itemBase.Code;
	// 	if (itemBase is GameItem gameItem) {
	// 		NumLabel!.Text = $"{gameItem.Num}";
	// 	}
	// }

	// // Called when the node enters the scene tree for the first time.
	// public override void _Ready() {

	// 	// Make(new GameItem("code", 22), "确定", "A", "B");
	// 	// Make(new GameItem("new!", 11), "new确定", "newA", "newB");
	// 	// PressedOption += (opt) => GD.Print(opt);

	// }

	// // Called every frame. 'delta' is the elapsed time since the previous frame.
	// public override void _Process(double delta) {
	// }

	public override void _ExitTree() {
		base._ExitTree();
	}

	private ItemBase item = ItemBase.Empty;
}
