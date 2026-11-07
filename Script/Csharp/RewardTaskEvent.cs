using System;
using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class RewardTaskEvent : TaskEventData {
	[Export]
	public Godot.Collections.Array<ItemBase> Items { get; set; } = [];

	[Export]
	public bool HasClaimed { get; set; }

	protected RewardTaskEvent() : base() { }

	public RewardTaskEvent(string code) : base(code) {
		// TODO:	生成奖励
	}
}
