using System;
using System.Text;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy.UI;

internal partial class CombatPanel : Control {
	public static CombatPanel Load() => (CombatPanel)ResourceLoader.Load<PackedScene>("res://Scene/CombatPanel.tscn").Instantiate();

	[Export]
	protected ProgressBar LeftProgressBar { get; set; } = null!;
	[Export]
	protected ProgressBar RightProgressBar { get; set; } = null!;

	[Export]
	protected RichTextLabel LeftStaticsLabel { get; set; } = null!;
	[Export]
	protected RichTextLabel RightStaticsLabel { get; set; } = null!;

	[Export]
	protected Label TimeLabel { get; set; } = null!;

	[Export]
	protected Button HideUIButton { get; set; } = null!;

	[Export]
	protected Button Retreat { get; set; } = null!;

	public event Action? RetreatEvent;

	public void OnStaticsSpawnerUpdated(StaticsData statics) {
		LeftProgressBar.MaxValue = statics.FullHP[CombatStage.TeamEnum.Left];
		RightProgressBar.MaxValue = statics.FullHP[CombatStage.TeamEnum.Right];
		LeftProgressBar.Value = statics.HP[CombatStage.TeamEnum.Left];
		RightProgressBar.Value = statics.HP[CombatStage.TeamEnum.Right];

		LeftStaticsLabel.Text = MakeStaticsShipsCountRichText(CombatStage.TeamEnum.Left, statics);
		RightStaticsLabel.Text = MakeStaticsShipsCountRichText(CombatStage.TeamEnum.Right, statics);
	}

	public void UpdateLeftTime(double leftTime) {
		TimeLabel.Text = $"{(int)Math.Round(leftTime)}";
	}

	public override void _Ready() {
		base._Ready();
		Retreat.Pressed += () => RetreatEvent?.Invoke();
	}

	private static string MakeStaticsShipsCountRichText(CombatStage.TeamEnum team, StaticsData statics) {
		StringBuilder builder = new();
		builder.AppendFormat("[color={0}]{1}[/color] |", statics.IsFlagshipAlive[team] ? "gold" : "white", statics.IsFlagshipAlive[team] ? "★" : "☆");
		if (statics.ShipsCount[team][SpaceshipConfig.ScaleSizeEnum.BB] != 0) {
			builder.AppendFormat("BB {0} [color=gold]/ {1}[/color]", statics.ShipsCount[team][SpaceshipConfig.ScaleSizeEnum.BB], statics.ShipsFullCount[team][SpaceshipConfig.ScaleSizeEnum.BB]);
		}
		if (statics.ShipsCount[team][SpaceshipConfig.ScaleSizeEnum.CA] != 0) {
			builder.AppendFormat("CA {0} [color=gold]/ {1}[/color]", statics.ShipsCount[team][SpaceshipConfig.ScaleSizeEnum.CA], statics.ShipsFullCount[team][SpaceshipConfig.ScaleSizeEnum.CA]);
		}
		if (statics.ShipsCount[team][SpaceshipConfig.ScaleSizeEnum.CL] != 0) {
			builder.AppendFormat("CL {0} [color=gold]/ {1}[/color]", statics.ShipsCount[team][SpaceshipConfig.ScaleSizeEnum.CL], statics.ShipsFullCount[team][SpaceshipConfig.ScaleSizeEnum.CL]);
		}
		if (statics.ShipsCount[team][SpaceshipConfig.ScaleSizeEnum.DD] != 0) {
			builder.AppendFormat("DD {0} [color=gold]/ {1}[/color]", statics.ShipsCount[team][SpaceshipConfig.ScaleSizeEnum.DD], statics.ShipsFullCount[team][SpaceshipConfig.ScaleSizeEnum.DD]);
		}
		if (statics.ShipsCount[team][SpaceshipConfig.ScaleSizeEnum.FF] != 0) {
			builder.AppendFormat("FF {0} [color=gold]/ {1}[/color]", statics.ShipsCount[team][SpaceshipConfig.ScaleSizeEnum.FF], statics.ShipsFullCount[team][SpaceshipConfig.ScaleSizeEnum.FF]);
		}
		return builder.ToString();
	}
}
