using Godot;
using OursGalaxy.ResourceConfig;
using System;
namespace OursGalaxy.UI;

internal partial class SpaceshipBox : Button {
	public static SpaceshipBox Load() => (SpaceshipBox)ResourceLoader.Load<PackedScene>("res://Scene/SpaceshipBox.tscn").Instantiate();

	[Export] protected RichTextLabel NameLabel { get; set; } = null!;
	[Export] protected TextureRect Avater { get; set; } = null!;

	public event Action<SpaceshipData>? Selected;

	public override void _Ready() {
		base._Ready();
		Pressed += OnPressed;
	}

	public void Make(SpaceshipData data) {
		dataRefer = data;
		var config = SpaceshipLib.GetConfig(data);
		NameLabel.Text = config.DisplayName;
	}

	private void OnPressed() {
		if (dataRefer is not null) {
			Selected?.Invoke(dataRefer);
		}
	}

	private SpaceshipData? dataRefer;
}
