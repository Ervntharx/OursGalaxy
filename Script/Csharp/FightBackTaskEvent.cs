using System;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

[GlobalClass]
internal partial class FightBackTaskEvent : TaskEventData {

	[Export]
	public CombatConfig CBConfig { get; set; }

	public FightBackTaskEvent(string code) {
		Time = DateTime.Now();
		NameCode = code;
		CBConfig = CombatConfig.GetConfig(new(CombatConfig.TEMPLATE_NAME_CODE, 1));
	}

	protected FightBackTaskEvent() {
		Time = DateTime.Now();
		NameCode = string.Empty;
		CBConfig = CombatConfig.GetConfig(new(CombatConfig.TEMPLATE_NAME_CODE, 1));
	}

	private bool _hasFightBack;
}
