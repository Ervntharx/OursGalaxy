using Godot;
namespace OursGalaxy;

/// <summary>
/// 猎杀
/// </summary>
[GlobalClass]
internal partial class HuntData : CombatData {
	public new static readonly HuntData Empty = new();

	[Export]
	public int TrackProgress { get; set; } = 0;

	public bool IsTracked => TrackProgress >= 100;

	protected HuntData() : base() { }

	public HuntData(string code, int level, int trackProgress) : base(code, level) {
		TrackProgress = trackProgress;
	}
}
