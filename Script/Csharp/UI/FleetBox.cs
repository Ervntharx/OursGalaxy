using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class FleetBox : Button {
	public static FleetBox Load() => (FleetBox)ResourceLoader.Load<PackedScene>("res://Scene/FleetBox.tscn").Instantiate();

	[Export] private RichTextLabel TitleLabel { get; set; } = null!;
	[Export] private RichTextLabel DescLabel { get; set; } = null!;
	[Export] private TextureRect Pic { get; set; } = null!;

	public FleetData? Fleet { get; set; }

	public void Render(int index, FleetData fleetData) {
		Fleet = fleetData;
		TitleLabel!.Text = MakeTitleString(index, fleetData.Name);
		DescLabel.Text = new RichTextBuilder("\n")
			.Add("旗舰", fleetData.Repository[fleetData.FlagshipReference].CustomName + " " + fleetData.Repository[fleetData.FlagshipReference].GetShipTypeName())
			.Add("舰队HP", 123.ToString() + " / " + 321.ToString(), ":")
			.Add("舰船数量", 10.ToString() + " / " + 20.ToString(), ":")
			.Add("舰队规模", 180.ToString() + " / " + 200.ToString(), ":")
			.ToString();

		Pressed += OnPressed;
	}

	public override void _ExitTree() {
		Pressed -= OnPressed;
	}

	private string MakeTitleString(long index, string name) {
		return $"[color=gold]{index}[/color] | {name}";
	}

	private void OnPressed() {
		GlobalViewModel.CachedSelectFleet = Fleet;
	}
}
