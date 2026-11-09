using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class SignCombatTaskEventConfig : TaskEventConfig {
	[Export] public override string DealButtonName { get; set; } = "标记";

	[Export] public override string DealUrl { get; set; } = "Sign";
}
