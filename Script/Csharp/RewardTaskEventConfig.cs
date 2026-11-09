using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class RewardTaskEventConfig : TaskEventConfig {
	[Export] public override string DealButtonName { get; set; } = "领取";

	[Export] public override string DealUrl { get; set; } = "Claim";
}
