using Godot;
using Godot.Collections;
namespace OursGalaxy;

/// <summary>
/// 游戏中的消息数据。消息是指主界面显示的消息盒子中的内容
/// 兼容邮件携带额外物品的情况
/// </summary>
[GlobalClass]
internal partial class MessageData : Resource {
	[Export]
	public bool HasRead { get; set; }

	[Export]
	public string Head { get; set; } = string.Empty;

	[Export]
	public string Content { get; set; } = string.Empty;

	[Export]
	public int RewardSolid { get; set; }

	[Export]
	public int RewardLiquid { get; set; }

	[Export]
	public int RewardIndustrialProduct { get; set; }

	[Export]
	public int RewardHonor { get; set; }

	[Export]
	public Array Reward { get; set; } = [];
}
