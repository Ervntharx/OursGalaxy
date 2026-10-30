using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class CombatData : Resource {
	public static readonly CombatData Empty = new();
	public static CombatData Template => ResourceLoader.Load<CombatData>("res://Res/Combats/Template.tres");

	public static CombatData? Load(string Code) => ResourceLoader.Exists($"res://Res/{Code}.tres") ? ResourceLoader.Load<CombatData>($"res://Res/{Code}.tres") : null;

	[Export]
	public string Code { get; set; }

	[Export]
	public string? DisplayName { get; set; }

	[Export(PropertyHint.Range)]
	public int Level { get; set; }

	[Export]
	public string Desc { get; set; } = string.Empty;

	protected CombatData() { Code = string.Empty; }
	public CombatData(string code, int level) {
		Code = code;
		Level = Mathf.Max(1, level);
	}
}
