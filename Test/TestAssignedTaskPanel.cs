using Godot;
using OursGalaxy.UI;
using System;
namespace OursGalaxy;

internal partial class TestAssignedTaskPanel : MarginContainer {
	[Export]
	protected AssignedTaskViewer Viewer { get; set; } = null!;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		var taskData = new AssignedTaskData("normal", AssignedTaskData.RatingLevelEnum.AA, 4);
		taskData.Events = [
			new TaskEvent("normal"),
			new RepairTaskEvent("repair"),
			new RewardTaskEvent("Reward"),
			new SignCombatTaskEvent("Sign"),
			new FightBackTaskEvent("FightBack")
		];
		Viewer.Make(taskData);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
