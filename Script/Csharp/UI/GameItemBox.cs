using System.Diagnostics.Tracing;
using System.Net;
using Godot;
namespace OursGalaxy.UI;

/// <summary>
/// 通用的GameItemBox
/// </summary>
internal partial class GameItemBox : Button {
	public static GameItemBox Load() => (GameItemBox)ResourceLoader.Load<PackedScene>("res://Scene/GameItemBox.tscn").Instantiate();

	[Export] private TextureRect PicTexture { get; set; } = null!;
	[Export] private RichTextLabel NameLabel { get; set; } = null!;
	[Export] private Label NumLabel { get; set; } = null!;

	public GameItem? Item {
		get => item; set {
			if (item is not null) {
				item.PropertyChanged -= Update;
			}
			item = value;
			if (item is not null) {
				item.PropertyChanged += Update;
			}
		}
	}

	public void Render(GameItem item) {
		Item = item;
		Update(Item, string.Empty);
		Item.PropertyChanged += Update;
	}

	private void Update(ItemBase itemBs, string _) {
		NameLabel.Text = itemBs.NameCode;
		NumLabel.Text = $"{((GameItem)itemBs).Num}";
	}

	private GameItem? item;
}
