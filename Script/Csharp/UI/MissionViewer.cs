using Godot;
using System;
using System.ComponentModel;
namespace OursGalaxy.UI;

internal partial class MissionViewer : Window {
	public MissionViewer Load() => (MissionViewer)ResourceLoader.Load<PackedScene>("res://Scene/MissionViewer.tscn").Instantiate();

	[Export]
	protected RichTextLabel? NameLabel { get; set; }

	[Export]
	protected RichTextLabel? RatingLevelLabel { get; set; }

	[Export]
	protected VBoxContainer? EventsPanel { get; set; }

	[Export]
	protected Button? Accept { get; set; }

	[Export]
	protected Button? FightBack { get; set; }

	public void Make(MissionData missionData) {
		NameLabel!.Text = missionData.Name;
		RatingLevelLabel!.Text = $"{missionData.RatingLevel}";
		foreach (var missionEvent in missionData.Events) {
			var eventDescLabel = new Label();
			eventDescLabel.Text = $"{missionEvent.Code}\n{"no description for now"}";
			EventsPanel!.AddChild(eventDescLabel);
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
