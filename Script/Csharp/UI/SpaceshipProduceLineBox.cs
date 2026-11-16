using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class SpaceshipProduceLineBox : Button {
	public static SpaceshipProduceLineBox Load() => (SpaceshipProduceLineBox)ResourceLoader.Load<PackedScene>("res://Scene/SpaceshipProduceLineBox.tscn").Instantiate();

	[Export] private RichTextLabel TitleLabel { get; set; } = null!;
	[Export] private RichTextLabel TimeLabel { get; set; } = null!;
	[Export] private ProgressBar ProduceProgressBar { get; set; } = null!;
	[Export] private Button OperationButton { get; set; } = null!;
	[Export] private Timer ProduceTimer { get; set; } = null!;
	[Export] private Timer ProgressUpdateTimer { get; set; } = null!;

	public enum StateEnum { Idle, Producing, Completed }
	public StateEnum State {
		get {
			var taskDate = GlobalDataSource.GetShipProduceLine(lineID);
			if (taskDate.IsEmpty) {
				return StateEnum.Idle;
			}
			if (!taskDate.EndDate()!.Over(DateTime.Now())) {
				return StateEnum.Completed;
			}
			return StateEnum.Producing;
		}
	}

	public void Render(int line_id) {
		lineID = line_id;
		UpdateUI();
	}

	public void UpdateUI() {
		var taskDate = GlobalDataSource.GetShipProduceLine()[lineID];
		switch (State) {
			case StateEnum.Producing:
				TitleLabel.Text = $"[color=green]{lineID}[/color] | [{taskDate.Target?.ToString() ?? "null"}] |进行中...";
				TimeLabel.Show();
				ProduceProgressBar.Show();
				ProduceProgressBar.MaxValue = taskDate.ProgressSeconds;
				ProduceProgressBar.Value = taskDate.StartDate!.To(DateTime.Now()).TotalSeconds;
				cache_left_time = DateTime.Now().To(taskDate.EndDate()!);
				ProduceTimer.Start(DateTime.Now().To(taskDate.EndDate()!).Seconds);
				ProgressUpdateTimer.Start();
				OperationButton.Text = "取消";
				break;
			case StateEnum.Completed:
				TimeLabel.Show();
				TimeLabel.Text = "完成！";
				ProduceProgressBar.Show();
				ProduceProgressBar.Value = ProduceProgressBar.MaxValue;
				ProduceTimer.Stop();
				ProgressUpdateTimer.Stop();
				OperationButton.Text = "完成";
				break;
			case StateEnum.Idle:
				TitleLabel.Text = $"[color=green]{lineID}[/color] | 空闲";
				TimeLabel.Hide();
				ProduceProgressBar.Hide();
				ProduceTimer.Stop();
				ProgressUpdateTimer.Stop();
				OperationButton.Text = "建造";
				break;
		}

	}

	public override void _EnterTree() {
		base._EnterTree();
		ProgressUpdateTimer.Timeout += UpdateProgress;
		ProduceTimer.Timeout += UpdateUI;
		OperationButton.Pressed += OnOperationButtonPressed;
	}

	public override void _ExitTree() {
		ProgressUpdateTimer.Timeout -= UpdateProgress;
		ProduceTimer.Timeout -= UpdateUI;
		base._ExitTree();
	}

	private void UpdateProgress() {
		if (State == StateEnum.Producing) {
			ProduceProgressBar.Value += 1;
			cache_left_time = cache_left_time - TimeSpan.FromSeconds(1);
			TimeLabel.Text = $"{cache_left_time.Days} : {cache_left_time.Hours} : {cache_left_time.Minutes} : {cache_left_time.Seconds}";
		}
	}

	private void OnOperationButtonPressed() {
		switch (State) {
			case StateEnum.Idle:
				// choose blueprint
				break;
			case StateEnum.Producing:
				// cancel
				break;
			case StateEnum.Completed:
				GlobalDataSource.ClaimProduceTask(lineID, out SpaceshipData data);
				break;
		}
	}

	private int lineID = -1;

	private TimeSpan cache_left_time;
}
