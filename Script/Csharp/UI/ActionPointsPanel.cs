using System.Collections.Generic;
using Godot;
namespace OursGalaxy.UI;

internal partial class ActionPointsPanel : MarginContainer {
	public static ActionPointsPanel Load() => (ActionPointsPanel)ResourceLoader.Load<PackedScene>("res://Scene/ActionPointsPanel.tscn").Instantiate();

	[Export] protected Button QuitButton { get; set; } = null!;
	[Export] protected RichTextLabel ActionPointsOverviewLabel { get; set; } = null!;

	[Export] protected Godot.Collections.Dictionary<ActionPointsCansEnum, SpinBox> CanssSpinBox { get; set; } = [];
	[Export] protected Godot.Collections.Dictionary<ActionPointsCansEnum, Button> CanssComfirmButton { get; set; } = [];

	public enum ActionPointsCansEnum { Little, Middle, Big }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		QuitButton.Pressed += () => PanelsStack.PopPanel(this);
		foreach (var pair in CanssComfirmButton) {
			pair.Value.Pressed += () => {
				var intendSpend = (int)CanssSpinBox[pair.Key].Value;
				if (GlobalViewModel.Data.ActionPointsCans[pair.Key] >= intendSpend) {
					GlobalViewModel.Data.ActionPointsCans[pair.Key] -= intendSpend;
					GlobalViewModel.Data.ActionPoints += provePoints[pair.Key];
					Update();
					return;
				}
				GD.PushError($"<ActionPointsPanel> Try to restore action points via non existen action cans:\n {pair.Key} required {intendSpend} but {GlobalViewModel.Data.ActionPointsCans[pair.Key]} left");
			};
		}
		Update();
		GlobalViewModel.Data.ActionPointsChangedEvent += Update;
	}

	private void Update() {
		foreach (var pair in CanssSpinBox) {
			var leftActionPointCanss = GlobalViewModel.Data.ActionPointsCans[pair.Key];
			pair.Value.Suffix = $"/ {leftActionPointCanss}";
			pair.Value.MaxValue = leftActionPointCanss;

			CanssComfirmButton[pair.Key].Disabled = leftActionPointCanss == 0;
		}

		ActionPointsOverviewLabel.Text = MakeActionPointsOverviewRichText();
	}

	private static string MakeActionPointsOverviewRichText() {
		return $"[font_size=120]⚡[/font_size]  [font_size=35]{GlobalViewModel.Data.ActionPoints}[/font_size] [font_size=25][color=gold]/ {GlobalViewModel.Data.ActionPointsLimit}[/color][/font_size]";
	}

	private static readonly Dictionary<ActionPointsCansEnum, int> provePoints = new() {
		{ActionPointsCansEnum.Big, 120},
		{ActionPointsCansEnum.Middle, 80},
		{ActionPointsCansEnum.Little, 40},
	};
}
