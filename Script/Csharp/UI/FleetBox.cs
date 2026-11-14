using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class FleetBox : Button {
	public static FleetBox Load() => (FleetBox)ResourceLoader.Load<PackedScene>("res://Scene/FleetBox.tscn").Instantiate();

	[Export] private RichTextLabel TitleLabel { get; set; } = null!;

	[Export] private RichTextLabel DescLabel { get; set; } = null!;

	[Export] private TextureRect Pic { get; set; } = null!;

	public string TrackedFleetUniqueID { get; private set; } = string.Empty;

	public void Make(int index, FleetData fleetData) {
		// TODO:
		TrackedFleetUniqueID = fleetData.ID.ToString();
		TitleLabel!.Text = MakeTitleString(index, fleetData.Name);
		DescLabel.Text = new RichTextBuilder("\n")
			.Add("旗舰", fleetData.Repository[fleetData.FlagshipReference].CustomName + " " + fleetData.Repository[fleetData.FlagshipReference].GetShipTypeName())
			.Add("舰队HP", 123.ToString() + " / " + 321.ToString(), ":")
			.Add("舰船数量", 10.ToString() + " / " + 20.ToString(), ":")
			.Add("舰队规模", 180.ToString() + " / " + 200.ToString(), ":")
			.ToString();
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private string MakeTitleString(long index, string name) {
		return $"[color=gold]{index}[/color] | {name}";
	}
}
