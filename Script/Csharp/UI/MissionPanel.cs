using Godot;
namespace OursGalaxy.UI;

internal partial class MissionPanel : MarginContainer {
	public static MissionPanel Load() => (MissionPanel)ResourceLoader.Load<PackedScene>("res://Scene/MissionPanel.tscn").Instantiate();

	[Export] protected Button QuitButton { get; set; } = null!;
	[Export] protected VBoxContainer MissionsListPanel { get; set; } = null!;
	[Export] protected RichTextLabel MissionTitleLabel { get; set; } = null!;
	[Export] protected RichTextLabel MissionDesc { get; set; } = null!;
	[Export] protected HBoxContainer RewardListPanel { get; set; } = null!;
	[Export] protected Button ClaimButton { get; set; } = null!;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		QuitButton.Pressed += () => PanelsStack.PopPanel(this);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
