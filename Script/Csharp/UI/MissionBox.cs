using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class MissionBox : Button {
	public MissionBox Load() => (MissionBox)ResourceLoader.Load<PackedScene>("res://Scene/MissionBox.tscn").Instantiate();

	[Export]
	protected RichTextLabel? NameLabel { get; set; }
	[Export]
	protected Label? TimeLabel { get; set; }
	[Export]
	protected ProgressBar? TimeProgressBar { get; set; }
	[Export]
	protected RichTextLabel? RatingLevelLabel { get; set; }

	public void Make(MissionData missionData) {
		NameLabel!.Text = $"[color=white]{missionData.Name}[/color]";
		RatingLevelLabel!.Text = $"{missionData.RatingLevel}";
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
