using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class AssignedTaskEventBox : MarginContainer {
	public static AssignedTaskEventBox Load() => (AssignedTaskEventBox)ResourceLoader.Load<PackedScene>("res://Scene/AssignedTaskEventBox.tscn").Instantiate();

	[Export]
	protected RichTextLabel TitleLable { get; set; } = null!;
	[Export]
	protected RichTextLabel BodyLabel { get; set; } = null!;

	public void Make(TaskEvent taskEvent) {
		task = taskEvent;
		task.TaskEventStateChanged += OnTaskStateChanged;
		OnTaskStateChanged(taskEvent);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		BodyLabel.MetaClicked += OnUrlPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private void OnUrlPressed(Variant variant) {
		if (variant.ToString() == task?.ReactUrl) {
			task.OnMatchUrl(variant);
		}
	}

	private void OnTaskStateChanged(TaskEvent taskEvent) {
		TitleLable.Text = taskEvent.Code;
		if (taskEvent.ReactButtonName is not null) {
			BodyLabel.Text = taskEvent.Desc + $"[right][url={taskEvent.ReactUrl}][lb] > {taskEvent.ReactButtonName} < [rb][/url][/right]";
		}
		else {
			BodyLabel.Text = taskEvent.Desc;
		}
	}

	private TaskEvent? task;
}
