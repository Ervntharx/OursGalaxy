using Godot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
namespace OursGalaxy.UI;

/// <summary>
/// Item查看器。在Show的时候绘制。需要在Show之前制定Items和Opts.Items是可变的，Opts是不可变的
/// </summary>
internal partial class ItemViewerPanel : Control {
	public ItemViewerPanel Load() => (ItemViewerPanel)ResourceLoader.Load<PackedScene>("res://Scene/ItemViewer.tscn").Instantiate();

	[Export]
	protected Button? QuitButton { get; set; }
	[Export]
	protected Button? SettingsButton { get; set; }

	[Export]
	protected TabContainer? Tabs { get; set; }

	[Export]
	protected HFlowContainer? ItemsPanel { get; set; }


	public ObservableCollection<ItemBase> Items => items;

	// 暂时抛去可变Options
	// public ObservableCollection<Option> Options {
	// 	get => opts; set {
	// 		opts = value;

	// 	}
	// }

	public IReadOnlyList<string> Opts = [];

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		VisibilityChanged += Redraw;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private ObservableCollection<ItemBase> items = [];

	private void Redraw() {
		if (Visible) {
			foreach (var child in ItemsPanel?.GetChildren() ?? []) {
				child.QueueFree();
			}
			foreach (var item in items) {
				var itemBox = ItemBox.Load();
				itemBox.Make(item, "default");
				ItemsPanel?.AddChild(itemBox);
			}
		}
	}
}
