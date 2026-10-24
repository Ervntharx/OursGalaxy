using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class FleetBox : Button {
	[Export]
	protected RichTextLabel? TitleLabel { get; set; }

	[Export]
	protected RichTextLabel? DescLabel { get; set; }

	[Export]
	protected TextureRect? Pic { get; set; }

	public void Make(FleetData fleetData) {
		// TODO
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private string MakeTitleString(int index, string name) {
		return $"[color=gold]{index:2}[/color] | {name}";
	}

	private string MakeDescString(string flagshipName, int hp, int fullHP, int shipsCount, int fullShipsCount, int size, int fullSize, string? presetName = null) {
		return @$"[color=gold]旗舰:	[/color]{flagshipName}
[color = gold]舰队HP: [/ color]{hp} / [color = gold]{fullHP}[/ color]
[color = gold]舰队舰船数量: [/ color]{shipsCount} / [color = gold]{fullShipsCount}[/ color]
[color = gold]舰队规模: [/ color]{size} / [color = gold]{fullSize}[/ color]

[color = gold]使用舰队预案: [/ color]{$"[color=gold]{presetName}[/color]" ?? "未使用"}
";
	}
}
