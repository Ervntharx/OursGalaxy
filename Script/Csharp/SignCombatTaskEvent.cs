using System;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

[GlobalClass]
[Obsolete]
internal partial class SignCombatTaskEvent : TaskEventData {
	[Export]
	public CombatConfig CBConfig { get; set; } = CombatConfig.GetConfig(new(CombatConfig.TEMPLATE_NAME_CODE, 1));
}
