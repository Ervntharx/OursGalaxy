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

	public void OnStaticsSpawnerUpdated(StaticsSpawner statics) {
		LeftProgressBar.MaxValue = statics.FleetFullHP[CombatStage.TeamEnum.Left];
		LeftProgressBar.MaxValue = statics.FleetFullHP[CombatStage.TeamEnum.Left];
		RightProgressBar.MaxValue = statics.FleetFullHP[CombatStage.TeamEnum.Right];
		RightProgressBar.Value = statics.FleetFullHP[CombatStage.TeamEnum.Right];

		LeftStaticsLabel.Text = MakeStaticsShipsCountRichText(CombatStage.TeamEnum.Left, statics);
		RightStaticsLabel.Text = MakeStaticsShipsCountRichText(CombatStage.TeamEnum.Right, statics);
	}

	public void UpdateTimer(float leftTime) {
		TimeLabel.Text = $"{(int)Math.Round(leftTime)}";
	}

	public override void _Ready() {
		base._Ready();
		Retreat.Pressed += () => RetreatEvent?.Invoke();
	}

	private static string MakeStaticsShipsCountRichText(CombatStage.TeamEnum team, StaticsSpawner statics) {
		StringBuilder builder = new();
		builder.AppendFormat("[color={0}]{1}[/color] |", statics.FlagAliveStatus[team] ? "gold" : "white", statics.FlagAliveStatus[team] ? "★" : "☆");
		if (statics.ShipsCount[team].ContainsKey(SpaceshipConfig.ScaleSizeEnum.BB)) {
			builder.AppendFormat("{0} [color=gold]/ BB[/color]", statics.ShipsCount[team][SpaceshipConfig.ScaleSizeEnum.BB]);
		}
		if (statics.ShipsCount[team].ContainsKey(SpaceshipConfig.ScaleSizeEnum.CA)) {
			builder.AppendFormat("{0} [color=gold]/ CA[/color]", statics.ShipsCount[team][SpaceshipConfig.ScaleSizeEnum.CA]);
		}
		if (statics.ShipsCount[team].ContainsKey(SpaceshipConfig.ScaleSizeEnum.CL)) {
			builder.AppendFormat("{0} [color=gold]/ CL[/color]", statics.ShipsCount[team][SpaceshipConfig.ScaleSizeEnum.CL]);
		}
		if (statics.ShipsCount[team].ContainsKey(SpaceshipConfig.ScaleSizeEnum.DD)) {
			builder.AppendFormat("{0} [color=gold]/ DD[/color]", statics.ShipsCount[team][SpaceshipConfig.ScaleSizeEnum.DD]);
		}
		if (statics.ShipsCount[team].ContainsKey(SpaceshipConfig.ScaleSizeEnum.FF)) {
			builder.AppendFormat("{0} [color=gold]/ FF[/color]", statics.ShipsCount[team][SpaceshipConfig.ScaleSizeEnum.FF]);
		}
		return builder.ToString();
	}
}
