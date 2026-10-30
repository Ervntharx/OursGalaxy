using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class FleetBox : Button {
	public static FleetBox Load() => (FleetBox)ResourceLoader.Load<PackedScene>("res://Scene/FleetBox.tscn").Instantiate();

	[Export]
	protected RichTextLabel? TitleLabel { get; set; }

	[Export]
	protected RichTextLabel? DescLabel { get; set; }

	[Export]
	protected TextureRect? Pic { get; set; }

	public string TrackedFleetUniqueID { get; private set; } = string.Empty;

	public void Make(int index, FleetData fleetData) {
		// TODO
		TrackedFleetUniqueID = fleetData.UniqueID;
		TitleLabel!.Text = MakeTitleString(index, fleetData.Name);
		DescLabel!.Text = MakeDescString(fleetData.Flagship.CustomName, fleetData.Flagship.GetShipTypeName(),
			123, 321, 6, 8, 250, 256);
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

	private string MakeDescString(string? flagshipCustomName, string flagshipTypeName, int hp, int fullHP, int shipsCount, int fullShipsCount, int size, int fullSize, string? presetName = null) {
		return $"[color=gold]旗舰:	[/color]{flagshipCustomName}[color=gold]{flagshipTypeName}[/color]\n\n[color=gold]舰队HP: [/color]{hp} / [color=gold]{fullHP}[/color]\n[color=gold]舰队舰船数量: [/color]{shipsCount} / [color=gold]{fullShipsCount}[/color]\n[color=gold]舰队规模: [/color]{size} / [color=gold]{fullSize}[/color]\n\n[color=gold]使用舰队预案: [/color]{$"[color=gold]{presetName}[/color]" ?? "未使用"}";
	}
}
