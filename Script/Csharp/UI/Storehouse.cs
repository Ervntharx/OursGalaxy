using System.Collections.Generic;
using System.Linq;
using Godot;
using OursGalaxy.ViewModel;
namespace OursGalaxy.UI;

/// <summary>
/// 主界面上的仓库界面. 设计该位置只展示GameItem这类可数物品
/// </summary>
internal partial class Storehouse : MarginContainer {
	public static Storehouse Load() => (Storehouse)ResourceLoader.Load<PackedScene>("res://Scene/Storehouse.tscn").Instantiate();

	[Export] private Button QuitButton { get; set; } = null!;
	[Export] private HFlowContainer ItemsListPanel { get; set; } = null!;
	[Export] private PanelContainer ItemInfoPanel { get; set; } = null!;
	[Export] private TextureRect TextureDisplayer { get; set; } = null!;
	[Export] private RichTextLabel ItemNameLabel { get; set; } = null!;
	[Export] private RichTextLabel ItemIntroLabel { get; set; } = null!;

	public override void _EnterTree() {
		base._EnterTree();
		QuitButton.Pressed += OnQuitPressed;
		ItemRepositoryVM.SelectElementEvent += RenderSelectedItem;
		Render();
	}

	public override void _ExitTree() {
		QuitButton.Pressed -= OnQuitPressed;
		ItemRepositoryVM.SelectElementEvent -= RenderSelectedItem;
		base._ExitTree();
	}

	private void Render() {
		foreach (var item in ItemRepositoryVM.GetGameItems()) {
			var box = GameItemBox.Load();
			box.Render(item);
			ItemsListPanel.AddChild(box);
		}
	}

	private void RenderAppend(GameItem item) {
		var box = GameItemBox.Load();
		box.Render(item);
		ItemsListPanel.AddChild(box);
	}

	private void RenderRemoved(GameItem item) => ItemsListPanel.GetChildren()
		.FirstOrDefault(child => child is GameItemBox box && box.Item?.Equals(item) is true)
		?.QueueFree();

	private void RenderSelectedItem(GameItem? item) {
		ItemInfoPanel.Visible = item is not null;
		if (item is not null) {
			var config = ResourceLib.GetConfig(item);
			ItemNameLabel.Text = config.DisplayName;
			ItemIntroLabel.Text = config.Introductuon;
		}
	}

	private void OnQuitPressed() {
		PanelsStack.PopPanel(this);
	}
}
