using Godot;
using OursGalaxy.ResourceConfig;
using OursGalaxy.UI;
using System;
namespace OursGalaxy;

internal partial class TestAssignedTaskPanel : MarginContainer {

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		var taskData = new AssignedTaskData("Template", AssignedTaskConfig.RatingLevelEnum.AA, 4);
		taskData.Events = [
			new TaskEventData(TaskEventConfig.EventTypeEnum.Genearal,TaskEventConfig.TEMPLATE_EVENT_NAME_CODE),
			new TaskEventData(TaskEventConfig.EventTypeEnum.SignCombat,TaskEventConfig.TEMPLATE_EVENT_NAME_CODE),
			new TaskEventData(TaskEventConfig.EventTypeEnum.Genearal,TaskEventConfig.TEMPLATE_EVENT_NAME_CODE),
			new TaskEventData(TaskEventConfig.EventTypeEnum.SignCombat,TaskEventConfig.TEMPLATE_EVENT_NAME_CODE),
		];

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
