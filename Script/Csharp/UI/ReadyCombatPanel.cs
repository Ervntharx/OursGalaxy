using System;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy.UI;

/// <summary>
/// 准备战斗界面
/// </summary>
internal partial class ReadyCombatPanel : MarginContainer {
	public static ReadyCombatPanel Load() => (ReadyCombatPanel)ResourceLoader.Load<PackedScene>("res://Scene/ReadyCombatPanel.tscn").Instantiate();
	[Export] private TextureRect LeftAvatar { get; set; } = null!;
	[Export] private RichTextLabel LeftUserNameLabel { get; set; } = null!;
	[Export] private RichTextLabel LeftFleetPointsLabel { get; set; } = null!;
	[Export] private RichTextLabel LeftFleetInformationLabel { get; set; } = null!;

	[Export] private HBoxContainer EditLeftBox { get; set; } = null!;

	[Export] private Button ChangeLeftFleetButton { get; set; } = null!;
	[Export] private Button EditLeftFleetButton { get; set; } = null!;

	[Export] private TextureRect RightAvatar { get; set; } = null!;
	[Export] private RichTextLabel RightUserNameLabel { get; set; } = null!;
	[Export] private RichTextLabel RightFleetPointsLabel { get; set; } = null!;
	[Export] private RichTextLabel RightFleetInformationLabel { get; set; } = null!;

	[Export] private HBoxContainer EditRightBox { get; set; } = null!;

	[Export] private Button ChangeRightFleetButton { get; set; } = null!;
	[Export] private Button EditRightFleetButton { get; set; } = null!;

	[Export] private Button QuitButton { get; set; } = null!;
	[Export] private Button StartButton { get; set; } = null!;

	public bool LockLeft { get => EditLeftBox.Visible; set => EditLeftBox.Visible = value; }
	public bool LockRight { get => EditRightBox.Visible; set => EditRightBox.Visible = value; }

	/// <summary>
	/// 刷新数据。舰队和阵营必须一同给出
	/// </summary>
	/// <param name="leftCamp"></param>
	/// <param name="leftFleet"></param>
	/// <param name="rightCamp"></param>
	/// <param name="rightFleet"></param>
	[Obsolete]
	public void Make(ProfileData? leftCamp, FleetData? leftFleet, ProfileData? rightCamp, FleetData? rightFleet) {
		if (leftCamp is not null && leftFleet is not null) {
			LeftAvatar!.Texture = leftCamp.Avatar;
			LeftUserNameLabel!.Text = leftCamp.Name;
			LeftFleetPointsLabel!.Text = $"{leftCamp.RankPoints}";

			// LeftFleetInformationLabel!.Text = MakeStringFleetInformation(leftFleet.Name, leftFleet.Size, leftFleet.ShipsCount, leftFleet.HP, leftFleet.Raid);
		}
		if (rightCamp is not null && rightFleet is not null) {
			RightAvatar!.Texture = rightCamp.Avatar;
			RightUserNameLabel!.Text = rightCamp.Name;
			RightFleetPointsLabel!.Text = $"{rightCamp.RankPoints}";

			// RightFleetInformationLabel!.Text = MakeStringFleetInformation(rightFleet.Name, rightFleet.Size, rightFleet.ShipsCount, rightFleet.HP, rightFleet.Raid);
		}
	}

	public ReadyCombatPanel Render(CombatStage.TeamEnum team, ProfileData profile) {
		switch (team) {
			case CombatStage.TeamEnum.Left:
				LeftAvatar!.Texture = profile.Avatar;
				LeftUserNameLabel!.Text = profile.Name;
				LeftFleetPointsLabel!.Text = $"{profile.RankPoints}";
				break;
			case CombatStage.TeamEnum.Right:
				RightAvatar!.Texture = profile.Avatar;
				RightUserNameLabel!.Text = profile.Name;
				RightFleetPointsLabel!.Text = $"{profile.RankPoints}";
				break;
			default:
				break;
		}
		return this;
	}

	public ReadyCombatPanel Render(CombatStage.TeamEnum team, FleetData fleet, bool lockEdit) {
		var fleetSize = 0;
		var fleetShipsCount = 0;
		var fleetHP = 0;
		var fleetFullHP = 0;
		var fleetRaid = 0;
		foreach (var ship in fleet.Repository.Ships) {
			var config = SpaceshipLib.GetConfig(ship);
			fleetSize += config.Size;
			fleetShipsCount += 1;
			fleetHP += ship.CurrentHP;
			fleetFullHP += config.HP;
			fleetRaid += config.Raid;
		}

		switch (team) {
			case CombatStage.TeamEnum.Left:
				LeftFleetInformationLabel!.Text = MakeStringFleetInformation(fleet.Name, fleetSize, fleetShipsCount, fleetHP, fleetRaid);
				LockLeft = lockEdit;
				break;
			case CombatStage.TeamEnum.Right:
				RightFleetInformationLabel!.Text = MakeStringFleetInformation(fleet.Name, fleetSize, fleetShipsCount, fleetHP, fleetRaid);
				LockRight = lockEdit;
				break;
			default:
				break;
		}
		return this;
	}

	public override void _EnterTree() {
		base._EnterTree();
		ChangeLeftFleetButton.Pressed += OnChangeLeftFleet;
		ChangeRightFleetButton.Pressed += OnChangeRightFleet;
		EditLeftFleetButton.Pressed += OnEditLeftFleet;
		EditRightFleetButton.Pressed += OnEditRightFleet;

		QuitButton.Pressed += OnQuitPressed;
	}

	public override void _ExitTree() {
		ChangeLeftFleetButton.Pressed -= OnChangeLeftFleet;
		ChangeRightFleetButton.Pressed -= OnChangeRightFleet;
		EditLeftFleetButton.Pressed -= OnEditLeftFleet;
		EditRightFleetButton.Pressed -= OnEditRightFleet;

		QuitButton.Pressed -= OnQuitPressed;
		base._ExitTree();
	}

	private void OnChangeLeftFleet() {
		PanelsStack.PushPanel(FleetsViewerPanel.Load());
	}

	private void OnChangeRightFleet() {
		PanelsStack.PushPanel(FleetsViewerPanel.Load());
	}

	private void OnEditLeftFleet() {
		var editor = FleetEditorPanel.Load();
		editor.Render(GlobalDataSource.GetReady2CombatFleet(CombatStage.TeamEnum.Left));
		PanelsStack.PushPanel(editor);
	}

	private void OnEditRightFleet() {
		var editor = FleetEditorPanel.Load();
		editor.Render(GlobalDataSource.GetReady2CombatFleet(CombatStage.TeamEnum.Right));
		PanelsStack.PushPanel(editor);
	}

	private void OnQuitPressed() {
		PanelsStack.PopPanel(this);
	}

	private string MakeStringFleetInformation(string name, int size, int shipsCount, int fleetHP, int fleetRaid) {
		return $"{name} [color=gold]舰队[/color]\n\n[color=gold]舰队规模[/color]\t\t{size}[color=gold] / 300[/color]\n[color=gold]舰船数量[/ color]\t\t{shipsCount}[color=gold] / 20[/color]\n[color=gold]舰队HP[/ color]\n\n{fleetHP}[color=gold] / 20000[/ color]\n\n[color=gold]舰队奇袭[/color]\t\t{fleetRaid}";
	}
}
