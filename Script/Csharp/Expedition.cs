using Godot;
using System;
/// <summary>
/// Represents expeditions in the game.
/// </summary>
[GlobalClass]
internal partial class Expedition : Resource, ICombat {
	public T ToResource<T>() where T : Resource {
		return (this as T)!;
	}

	public Resource AsResource() {
		return this;
	}

	/// <summary>
	/// The name of the expedition.
	/// </summary>
	[Export]
	public string Name { get; set; } = "Expedition";

	/// <summary>
	/// The level of the expedition. ranges from 1 to MaxLevel
	/// </summary>
	[Export]
	public ushort Level { get; set; } = 1;

	/// <summary>
	/// The minimum level of the expedition
	/// </summary>
	public const ushort MinLevel = 1;
	/// <summary>
	/// The maximum level of the expedition
	/// </summary>
	public const ushort MaxLevel = 30;

	[Export]
	public GameTimeSpan Duration { get; set; } = new(TimeSpan.FromHours(1));
}
