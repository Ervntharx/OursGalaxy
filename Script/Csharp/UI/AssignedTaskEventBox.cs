using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy.UI;

internal partial class AssignedTaskEventBox : MarginContainer {
	public static AssignedTaskEventBox Load() => (AssignedTaskEventBox)ResourceLoader.Load<PackedScene>("res://Scene/AssignedTaskEventBox.tscn").Instantiate();

	[Export] private RichTextLabel TitleLable { get; set; } = null!;
	[Export] private RichTextLabel BodyLabel { get; set; } = null!;
	[Export] private RichTextLabel ReactLabel { get; set; } = null!;

	public void Make(TaskEventData taskEvent) {
		config = TaskEventLib.GetConfig(taskEvent);
		TitleLable.Text = config.DisplayName;
		BodyLabel.Text = config.Desc;
		UpdateTaskStateDisplay(taskEvent.Deal);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		ReactLabel.MetaClicked += OnUrlPressed;
	}

	private void OnUrlPressed(Variant variant) {
		if (config is not null && variant.ToString() == config.DealUrl) {
			config.Deal();
			UpdateTaskStateDisplay(true);
		}
	}

	private void UpdateTaskStateDisplay(bool deal) {
		if (config is not null) {
			if (!deal) {
				ReactLabel.Text = $"[right][url={config.DealUrl}][lb] > {config.DealButtonName} < [rb][/url][/right]";
			}
			else {
				ReactLabel.Text = $"[right][lb] > {config.HandledButtonName} < [rb][/right]";
			}
		}
	}

	private TaskEventConfig? config;
}
