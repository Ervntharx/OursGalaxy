using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class ProduceLine : Button {
	public enum StateEnum { Idle, Producing, Completed }
	public StateEnum State { get; protected set; } = StateEnum.Idle;

	[Export]
	protected RichTextLabel? TitleLabel { get; set; }

	[Export]
	protected RichTextLabel? TimeLabel { get; set; }

	[Export]
	protected ProgressBar? ProduceProgressBar { get; set; }

	[Export]
	protected Button? OperationButton { get; set; }


}
