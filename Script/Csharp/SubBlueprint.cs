using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class SubBlueprint : GameItem {
	public static SubBlueprint Load(string code) => ResourceLoader.Load<SubBlueprint>($"res://Res/DamageControlData/{code}.tres");

	[Export]
	public int RequireNum { get; set; }
}
