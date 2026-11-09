using System;
using Godot;
namespace OursGalaxy;

[GlobalClass]
[Obsolete]
internal partial class RewardTaskEvent : TaskEventData {
	[Export] public Godot.Collections.Array<ItemBase> Items { get; private set; } = [];
}
