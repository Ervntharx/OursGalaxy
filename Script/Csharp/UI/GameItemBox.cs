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

	public GameItem? Item {
		get => item; set {
			if (item is not null) {
				item.NumChangedEvent -= UpdateNum;
			}
			item = value;
			if (item is not null) {
				item.NumChangedEvent += UpdateNum;
			}
		}
	}

	public void Render(StorehouseVM vm, GameItem item) {
		this.vm = vm;
		Item = item;
		var config = ResourceLib.GetConfig(item);
		NameLabel.Text = config.DisplayName;
		NumLabel.Text = $"{item.Num}";
		Pressed += OnPressed;
	}

	private void UpdateNum(GameItem _, int num) {
		NumLabel.Text = $"{num}";
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
