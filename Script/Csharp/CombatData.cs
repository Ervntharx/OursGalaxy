using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class CombatData : Resource {
	[Export] public string NameCode { get; set; }

	[Export] public int Level { get; set; } = 1;

	protected CombatData() { NameCode = string.Empty; }

	public CombatData(string code, int level) {
		NameCode = code;
		Level = Mathf.Max(1, level);
	}
}
