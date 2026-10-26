using Godot;
using System;
namespace OursGalaxy.UI;

/// <summary>
/// 准备战斗界面
/// </summary>
internal partial class ReadyCombatPanel : MarginContainer {
	public ReadyCombatPanel Load() => (ReadyCombatPanel)ResourceLoader.Load<PackedScene>("res://Scene/ReadyCombatPanel.tscn").Instantiate();
	[Export]
	protected TextureRect? LeftAvatar { get; set; }
	[Export]
	protected RichTextLabel? LeftUserNameLabel { get; set; }
	[Export]
	protected RichTextLabel? LeftFleetPointsLabel { get; set; }
	[Export]
	protected RichTextLabel? LeftFleetInformationLabel { get; set; }

	[Export]
	protected TextureRect? RightAvatar { get; set; }
	[Export]
	protected RichTextLabel? RightUserNameLabel { get; set; }
	[Export]
	protected RichTextLabel? RightFleetPointsLabel { get; set; }
	[Export]
	protected RichTextLabel? RightFleetInformationLabel { get; set; }


	[Export]
	protected Button? QuitButton { get; set; }
	[Export]
	protected Button? ChangeFleetButton { get; set; }
	[Export]
	protected Button? EditFleetButtton { get; set; }
	[Export]
	protected Button? StartButton { get; set; }

	/// <summary>
	/// 刷新数据。舰队和阵营必须一同给出
	/// </summary>
	/// <param name="leftCamp"></param>
	/// <param name="leftFleet"></param>
	/// <param name="rightCamp"></param>
	/// <param name="rightFleet"></param>
	public void Make(ProfileData? leftCamp, FleetData? leftFleet, ProfileData? rightCamp, FleetData? rightFleet) {
		if (leftCamp is not null && leftFleet is not null) {
			LeftAvatar!.Texture = leftCamp.Avatar;
			LeftUserNameLabel!.Text = leftCamp.Name;
			LeftFleetPointsLabel!.Text = $"{leftCamp.Points}";

			LeftFleetInformationLabel!.Text = MakeStringFleetInformation(leftFleet.Name, leftFleet.Size, leftFleet.ShipsCount, leftFleet.HP, leftFleet.Raid);
		}
		if (rightCamp is not null && rightFleet is not null) {
			RightAvatar!.Texture = rightCamp.Avatar;
			RightUserNameLabel!.Text = rightCamp.Name;
			RightFleetPointsLabel!.Text = $"{rightCamp.Points}";

			RightFleetInformationLabel!.Text = MakeStringFleetInformation(rightFleet.Name, rightFleet.Size, rightFleet.ShipsCount, rightFleet.HP, rightFleet.Raid);
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private string MakeStringFleetInformation(string name, int size, int shipsCount, int fleetHP, int fleetRaid) {
		return $"{name} [color=gold]舰队[/color]\n\n[color=gold]舰队规模[/color]\t\t{size}[color=gold] / 300[/color]\n[color=gold]舰船数量[/ color]\t\t{shipsCount}[color=gold] / 20[/color]\n[color=gold]舰队HP[/ color]\n\n{fleetHP}[color=gold] / 20000[/ color]\n\n[color=gold]舰队奇袭[/color]\t\t{fleetRaid}";
	}
}
