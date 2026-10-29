using Godot;
namespace OursGalaxy;

/// <summary>
/// 猎杀
/// </summary>
[GlobalClass]
internal partial class Hunt : Resource, ICombat {
	public T ToResource<T>() where T : Resource {
		return (this as T)!;
	}

	public Resource AsResource() {
		return this;
	}
	[Export]
	public string Name { get; set; } = "Hunt";
	/// <summary>
	/// The level of the hunt. ranges from 1 to MaxLevel
	/// </summary>
	[Export]
	public ushort Level { get; set; } = 1;

	public ushort TrackProgress { get; set; } = 0;

	public bool IsTracked => TrackProgress >= 100;

	/// <summary>
	/// The minimum level of the hunt
	/// </summary>
	public const ushort MinLevel = 1;
	/// <summary>
	/// The maximum level of the hunt
	/// </summary>
	public const ushort MaxLevel = 30;
}
