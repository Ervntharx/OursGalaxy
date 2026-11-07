using System;
using System.Linq;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

[GlobalClass]
internal partial class SignCombatTaskEvent : TaskEventData {
	[Export]
	public CombatConfig CBConfig { get; set; } = CombatConfig.GetConfig(new(CombatConfig.TEMPLATE_NAME_CODE, 1));

	[Export]
	public bool HasSign { get; set; }

	public SignCombatTaskEvent(string code) : base(code) {
		//	TODO:	生成Combat

	}

	protected SignCombatTaskEvent() : base() { }
}
