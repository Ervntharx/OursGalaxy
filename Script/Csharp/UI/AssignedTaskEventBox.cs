using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class AssignedTaskEventBox : MarginContainer {
	[Export]
	protected RichTextLabel TitleLable { get; set; } = null!;
	[Export]
	protected RichTextLabel BodyLabel { get; set; } = null!;    // TODO:	根据远征事件在此绘制

	public void Make(AssignedTaskEvent taskEvent) {
		TitleLable.Text = taskEvent.Code;
		switch (taskEvent.EventType) {
			case AssignedTaskEvent.EventTypeEnum.None:
				// OperationButton.Hide();
				break;
			case AssignedTaskEvent.EventTypeEnum.SignedPunitiveCombat:
				// OperationButton.Text = "标记";
				// OperationButton.Show();
				break;
			case AssignedTaskEvent.EventTypeEnum.FightBack:
				// TODO:	直接调用CombatStage，ReadyCombat开始打击退
				break;
				// TODO:	其他远征事件类型响应
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		BodyLabel.MetaClicked += OnUrlPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private void OnUrlPressed(Variant variant) {

	}
}
