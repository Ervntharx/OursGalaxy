using System;
using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class RewardTaskEvent : TaskEvent {
	public override string? ReactButtonName => "领取";

	public override string? ReactUrl => "Claim";
	[Export]
	public Godot.Collections.Array<ItemBase> Items { get; set; } = [];

	[Export]
	public bool HasClaimed { get; set; }

	protected RewardTaskEvent() : base() { }

	public RewardTaskEvent(string code) : base(code) {
		// TODO:	生成奖励
	}

	public override void OnMatchUrl(Variant variant) {
		// TODO:	放入存档
	}
}
