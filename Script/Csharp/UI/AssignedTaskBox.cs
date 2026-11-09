using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class AssignedTaskBox : Button {
	public AssignedTaskBox Load() => (AssignedTaskBox)ResourceLoader.Load<PackedScene>("res://Scene/MissionBox.tscn").Instantiate();

	[Export] private RichTextLabel? NameLabel { get; set; }
	[Export] private Label? TimeLabel { get; set; }
	[Export] private ProgressBar? TimeProgressBar { get; set; }
	[Export] private RichTextLabel? RatingLevelLabel { get; set; }

	public void Make(AssignedTaskData missionData) {
		NameLabel!.Text = $"[color=white]{missionData.NameCode}[/color]";
		RatingLevelLabel!.Text = $"{missionData.RatingLevel}";
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
