using Godot;
using System;
using System.ComponentModel;
namespace OursGalaxy.UI;

internal partial class AssignedTaskViewer : MarginContainer {
	public AssignedTaskViewer Load() => (AssignedTaskViewer)ResourceLoader.Load<PackedScene>("res://Scene/AssignedTaskViewer.tscn").Instantiate();

	[Export]
	protected RichTextLabel AssignedTaskInfLabel { get; set; } = null!;

	[Export]
	protected VBoxContainer EventsPanel { get; set; } = null!;

	[Export]
	protected Button? OperationButton { get; set; }

	public void Make(AssignedTaskData missionData) {
		AssignedTaskInfLabel.Text = $"{missionData.Name} {missionData.Level}";
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
