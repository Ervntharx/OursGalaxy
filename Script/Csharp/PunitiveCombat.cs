using System;
using Godot;
namespace OursGalaxy;

/// <summary>
/// 讨伐
/// </summary>
[GlobalClass]
[Obsolete("CombatData instead")]
internal partial class PuntiveCombat : Resource, ICombat {
	[Obsolete]
	public readonly static PuntiveCombat? Empty = null;

	public readonly static PuntiveCombat Template = new("Template", 1);

	public T ToResource<T>() where T : Resource {
		return (this as T)!;
	}

	public Resource AsResource() {
		return this;
	}

	[Export]
	public string Name { get; set; }

	[Export]
	public ushort Level { get; set; }

	public PuntiveCombat(string name, ushort level) {
		Name = name;
		Level = level;
	}
}

