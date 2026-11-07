using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

[GlobalClass]
internal partial class SignCombatTaskEvent : TaskEventData {
	[Export]
	public CombatConfig CBConfig { get; set; } = CombatConfig.GetConfig(new(CombatConfig.TEMPLATE_NAME_CODE, 1));

	public SignCombatTaskEvent(string code) : base(code) {
		//	TODO:	生成Combat
	}

	protected SignCombatTaskEvent() : base() { }
}
