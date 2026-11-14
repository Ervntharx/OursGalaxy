using System.Collections.Generic;
using System.Linq;
using Godot;
namespace OursGalaxy.UI;

/// <summary>
/// 主界面上的仓库界面. 设计该位置只展示GameItem这类可数物品
/// </summary>
internal partial class Storehouse : MarginContainer {
	public static Storehouse Load() => (Storehouse)ResourceLoader.Load<PackedScene>("res://Scene/Storehouse.tscn").Instantiate();

	[Export] private Button QuitButton { get; set; } = null!;
	[Export] private HFlowContainer ItemsListPanel { get; set; } = null!;

	public override void _EnterTree() {
		base._EnterTree();
		QuitButton.Pressed += () => PanelsStack.PopPanel(this);
		// GlobalViewModel.GameItems.CollectionChanged += Render;
		Render();
	}

	public override void _ExitTree() {
		// GlobalViewModel.GameItems.CollectionChanged -= Render;
		base._ExitTree();
	}

	private void Render() {
		GD.Print(GlobalViewModel.GameItems.Count);
		foreach (var item in GlobalViewModel.GameItems) {
			var box = GameItemBox.Load();
			box.Render(item);
			ItemsListPanel.AddChild(box);
		}
	}

	private void Render(object? _, System.Collections.Specialized.NotifyCollectionChangedEventArgs args) {
		if (args.NewItems is not null) {
			foreach (var newItem in args.NewItems.OfType<GameItem>()) {
				var box = GameItemBox.Load();
				box.Render(newItem);
				ItemsListPanel.AddChild(box);
			}
		}
		if (args.OldItems is not null) {
			var oldItemsLs = new LinkedList<GameItem>();
			foreach (var oldItem in args.OldItems.OfType<GameItem>()) {
				oldItemsLs.AddLast(oldItem);
			}
			foreach (var box in ItemsListPanel.GetChildren().OfType<GameItemBox>()) {
				if (box.Item is not null) {
					if (oldItemsLs.Contains(box.Item)) {
						box.QueueFree();
					}
				}
				else {
					GD.PushError("<Storehouse> GameItemBox.Item is null");
				}
			}
		}
	}
}
