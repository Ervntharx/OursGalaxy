using Godot;
/// <summary>
/// Represents a punitive combat in the game.
/// </summary>
[GlobalClass]
internal partial class PuntiveCombat : Resource, ICombat {
	public T ToResource<T>() where T : Resource {
		return (this as T)!;
	}

	public Resource AsResource() {
		return this;
	}

	[Export]
	public string Name { get; set; } = "Punitive Combat";

	[Export]
	public ushort Level { get; set; } = 1;


}

