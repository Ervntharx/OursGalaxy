using Godot;
namespace OursGalaxy.UI;

/// <summary>
/// 通用的GameItemBox
/// </summary>
internal partial class GameItemBox : Button {
	public static GameItemBox Load() => (GameItemBox)ResourceLoader.Load<PackedScene>("res://Scene/GameItemBox.tscn").Instantiate();

	[Export]
	protected TextureRect PicTexture { get; set; } = null!;
	[Export]
	protected RichTextLabel NameLabel { get; set; } = null!;
	[Export]
	protected Label NumLabel { get; set; } = null!;

	public GameItem? Item { get; private set; }

	public void Make(GameItem item) {
		Item = item;
		Update(Item, string.Empty);
		Item.PropertyChanged += Update;
	}

	private void Update(ItemBase itemBs, string _) {
		NameLabel.Text = itemBs.NameCode;
		NumLabel.Text = $"{((GameItem)itemBs).Num}";
	}
}
