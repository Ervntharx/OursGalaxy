using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class ProduceLine : Button {
	public enum StateEnum { Idle, Producing, Completed }
	public StateEnum State {
		get => state;
		set {
			switch (state) {
				case StateEnum.Idle:
					TimeLabel?.Hide();
					ProduceProgressBar?.Hide();
					break;
				case StateEnum.Producing:
					TimeLabel?.Show();
					ProduceProgressBar?.Show();
					break;
				case StateEnum.Completed:
					TimeLabel?.Show();
					ProduceProgressBar?.Show();
					break;
				default:
					break;
			}
		}
	}

	[Export]
	protected RichTextLabel? TitleLabel { get; set; }

	[Export]
	protected RichTextLabel? TimeLabel { get; set; }

	[Export]
	protected ProgressBar? ProduceProgressBar { get; set; }

	[Export]
	protected Button? OperationButton { get; set; }

	[Export]
	protected Timer? ProduceTimer { get; set; }


	// TODO:	对口ProduceTaskData的序列化和反序列化，状态变更


	private StateEnum state = StateEnum.Idle;
}
