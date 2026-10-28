using Godot;
namespace OursGalaxy.UI;

internal partial class FactoryPanel : Panel {
	public static FactoryPanel Load() => (FactoryPanel)ResourceLoader.Load<PackedScene>("res://Scene/FactoryPanel.tscn").Instantiate();

	[Export]
	protected Button QuitButton { get; set; } = null!;

	[Export]
	protected HFlowContainer? ShipsProduceLinesPanel { get; set; }

	[Export]
	protected HFlowContainer? EquipProduceLinePanel { get; set; }

	// [Export]
	// protected TabContainer? TabPanel { get; set; }

	// protected const int SHIP_FACTORY_TAB_ID = 0;
	// protected const int EQUIP_FACTORY_TAB_ID = 1;

	public override void _Ready() {
		base._Ready();
		QuitButton.Pressed += () => PanelsStack.PopPanel(this);

		var tasks = Archive.Data.ShipsProduceTasks;
		for (var i = 0; i < tasks.Count; i++) {
			var line = ProduceLineBox.Load();
			line!.Make(i, tasks[i]);
			ShipsProduceLinesPanel!.AddChild(line);
		}

		tasks = Archive.Data.EquipProduceTasks;
		for (var i = 0; i < tasks.Count; i++) {
			var line = ProduceLineBox.Load();
			line!.Make(i, tasks[i]);
			EquipProduceLinePanel!.AddChild(line);
		}

	}
}
