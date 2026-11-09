using System;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy.UI;

internal partial class ShipConfigEquipBox : Button {
	public static ShipConfigEquipBox Load() => (ShipConfigEquipBox)ResourceLoader.Load<PackedScene>("res://Scene/ShipConfigPanel.tscn").Instantiate();

	[Export] private TextureRect ICONDisplayer { get; set; } = null!;
	[Export] private RichTextLabel EquipNameLabel { get; set; } = null!;
	[Export] private RichTextLabel InfEquipLabel { get; set; } = null!;
	[Export] private Button UninstallButton { get; set; } = null!;

	public event Action? PressedEvent;
	public event Action? UninstallEvent;

	public void Make(EquipData data) {
		var config = ResourceLib.GetConfig(data);
		EquipNameLabel.Text = config.DisplayName;
		Pressed += () => PressedEvent?.Invoke();
		UninstallButton.Pressed += () => UninstallEvent?.Invoke();
	}
}
