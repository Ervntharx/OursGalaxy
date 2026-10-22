using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class ProduceLine : Button {
	public enum StateEnum { Idle, Producing, Completed }
	public StateEnum State {
		get {
			if (taskDate is null) {
				return StateEnum.Idle;
			}
			if (!taskDate.EndDate().Over(DateTime.Now())) {
				return StateEnum.Completed;
			}
			return StateEnum.Producing;
		}
	}

	public enum OperationEnum { Start, Cancel, Complete }

	public event Action<OperationEnum>? OperatePressed;

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

	[Export]
	protected Timer? ProgressUpdateTimer { get; set; }

	public void Make(int line_id, ProduceTaskData task) {
		lineID = line_id;
		taskDate = task;
		UpdateUI();
	}

	public ProduceTaskData? Save() {
		return taskDate is null ? null : new(taskDate);
	}

	public void UpdateUI() {
		switch (State) {
			case StateEnum.Producing:
				TitleLabel!.Text = $"[color=green]{lineID}[/color] | [{taskDate?.Target.Code ?? "null"}] |进行中...";
				TimeLabel!.Show();
				ProduceProgressBar!.Show();
				ProduceProgressBar!.MaxValue = taskDate!.ProgressTimeSpan.TotalSeconds;
				ProduceProgressBar!.Value = taskDate!.StartDate.To(DateTime.Now()).TotalSeconds;
				cache_left_time = DateTime.Now().To(taskDate!.EndDate());
				ProduceTimer!.Start(DateTime.Now().To(taskDate!.EndDate()).Seconds);
				ProgressUpdateTimer!.Start();
				OperationButton!.Text = "取消";
				break;
			case StateEnum.Completed:
				TitleLabel!.Text = $"[color=green]{lineID}[/color] | [{taskDate?.Target.Code ?? "null"}]";
				TimeLabel!.Show();
				TimeLabel!.Text = "完成！";
				ProduceProgressBar!.Show();
				ProduceProgressBar!.Value = ProduceProgressBar!.MaxValue;
				ProduceTimer!.Stop();
				ProgressUpdateTimer!.Stop();
				OperationButton!.Text = "完成";
				break;
			case StateEnum.Idle:
				TitleLabel!.Text = $"[color=green]{lineID}[/color] | 空闲";
				TimeLabel!.Hide();
				ProduceProgressBar!.Hide();
				ProduceTimer!.Stop();
				ProgressUpdateTimer!.Stop();
				OperationButton!.Text = "建造";
				break;
		}
	}



	public override void _Ready() {
		base._Ready();
		ProgressUpdateTimer!.Timeout += UpdateProgress;
		ProduceTimer!.Timeout += UpdateUI;
		OperationButton!.Pressed += OnOperationButtonPressed;
		//test:
		ProduceTimer!.Timeout += () => GD.Print($"ProduceTimer update{Time.GetTicksMsec()}");
		ProgressUpdateTimer!.Timeout += () => GD.Print($"ProgressUpdateTimer update{Time.GetTicksMsec()}");
	}

	public override void _ExitTree() {
		ProgressUpdateTimer!.Timeout -= UpdateProgress;
		ProduceTimer!.Timeout -= UpdateUI;
		base._ExitTree();
	}

	private void UpdateProgress() {
		if (State == StateEnum.Producing) {
			ProduceProgressBar!.Value += 1;
			cache_left_time = cache_left_time - TimeSpan.FromSeconds(1);
			TimeLabel!.Text = $"{cache_left_time.Days} : {cache_left_time.Hours} : {cache_left_time.Minutes} : {cache_left_time.Seconds}";
		}
	}

	private void OnOperationButtonPressed() {
		OperatePressed?.Invoke(State switch {
			StateEnum.Idle => OperationEnum.Start,
			StateEnum.Producing => OperationEnum.Cancel,
			StateEnum.Completed => OperationEnum.Complete,
			_ => throw new InvalidOperationException(),
		});
	}

	private ProduceTaskData? taskDate;

	private TimeSpan cache_left_time;

	private int lineID;
}
