using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class CombatData : Resource {
	public static readonly CombatData Empty = new();
	public static CombatData Template => ResourceLoader.Load<CombatData>("res://Res/Combats/Template.tscn");

	[Export]
	public string Code { get; set; }
	[Export]
	public int Level { get; set; }

	[Export]
	public string Desc { get; set; } = string.Empty;

	protected CombatData() { Code = string.Empty; }
	public CombatData(string code, int level) {
		Code = code;
		Level = Mathf.Max(1, level);
	}
}
