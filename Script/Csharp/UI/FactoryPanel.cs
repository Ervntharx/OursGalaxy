using Godot;
using OursGalaxy.UI;
namespace OursGalaxy;

internal partial class FactoryPanel : Panel {
	[Export]
	protected HFlowContainer? ShipsProduceLinesPanel { get; set; }

	[Export]
	protected HFlowContainer? EquipProduceLinePanel { get; set; }

	// [Export]
	// protected TabContainer? TabPanel { get; set; }

	[Export]
	protected PackedScene? ProduceLineUI { get; set; }

	// protected const int SHIP_FACTORY_TAB_ID = 0;
	// protected const int EQUIP_FACTORY_TAB_ID = 1;

	public override void _Ready() {
		base._Ready();
		var tasks = Archive.Data.ShipsProduceTasks;
		for (var i = 0; i < tasks.Count; i++) {
			var line = ProduceLineUI!.Instantiate() as ProduceLine;
			line!.Make(i, tasks[i]);
			ShipsProduceLinesPanel!.AddChild(line);
		}

		tasks = Archive.Data.EquipProduceTasks;
		for (var i = 0; i < tasks.Count; i++) {
			var line = ProduceLineUI!.Instantiate() as ProduceLine;
			line!.Make(i, tasks[i]);
			EquipProduceLinePanel!.AddChild(line);
		}

	}
}
