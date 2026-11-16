using Godot;
using OursGalaxy.ViewModel;
namespace OursGalaxy.UI;

/// <summary>
/// 通用的GameItemBox
/// </summary>
internal partial class GameItemBox : Button {
	public static GameItemBox Load() => (GameItemBox)ResourceLoader.Load<PackedScene>("res://Scene/GameItemBox.tscn").Instantiate();

	[Export] private TextureRect PicTexture { get; set; } = null!;
	[Export] private RichTextLabel NameLabel { get; set; } = null!;
	[Export] private Label NumLabel { get; set; } = null!;

	public GameItem? Item { get => item; set { item = value; } }

	public void Render(StorehouseVM vm, GameItem item) {
		this.vm = vm;
		Item = item;
		var config = ResourceLib.GetConfig(item);
		NameLabel.Text = config.DisplayName;
		NumLabel.Text = $"{item.Num}";
		Pressed += OnPressed;
		GlobalDataSource.Source.GameItemNumChangedEvent += UpdateNum;
		GlobalDataSource.Source.GameItemRemovedEvent += OnRemove;
	}

	public override void _ExitTree() {
		GlobalDataSource.Source.GameItemNumChangedEvent -= UpdateNum;
		GlobalDataSource.Source.GameItemRemovedEvent -= OnRemove;
	}

	private void UpdateNum(GameItem item, int _) {
		if (item.Equals(this.item)) {
			NumLabel.Text = $"{item.Num}";
		}
	}

	private void OnRemove(GameItem item) {
		if (item.Equals(this.item)) {
			QueueFree();
		}
	}

	private void OnPressed() {
		if (vm is not null) {
			if (vm.SelectedItem?.Equals(Item) is true) {
				vm.SelectedItem = null;
				return;
			}
			vm.SelectedItem = Item;
		}
		else {
			GD.PushError("<GameItemBox> StorehouseVM is null");
		}
	}

	private StorehouseVM? vm;

	private GameItem? item;
}
