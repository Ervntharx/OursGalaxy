using System.Reflection;
using Godot;
namespace OursGalaxy.UI;

/// <summary>
/// 设置界面UI
/// </summary>
internal partial class SettingPanel : MarginContainer {
	public static SettingPanel Load() => (SettingPanel)ResourceLoader.Load<PackedScene>("res://Scene/SettingPanel.tscn").Instantiate();

	[Export]
	protected Button? QuitButton { get; set; }

	[Export]
	protected RichTextLabel? AboutPage { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		base._Ready();
		QuitButton!.Pressed += QueueFree;
		AboutPage!.MetaClicked += OnAboutPageMetaClicked;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	public override void _ExitTree() {
		QuitButton!.Pressed -= QueueFree;
		base._ExitTree();
	}

	private void OnAboutPageMetaClicked(Variant variant) {
		var str = variant.ToString();
		if (str.StartsWith("open_url:")) {
			var url = str.Replace("open_url:", string.Empty);
			GD.Print(url);
			OS.ShellOpen(url);
		}
	}
}
