using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class SubBlueprint : GameItem {
	public new static readonly SubBlueprint Empty = new();
	public static SubBlueprint Template => ResourceLoader.Load<SubBlueprint>("res://Res/Blueprints/Template.tres");

	public static SubBlueprint? Load(string code) => ResourceLoader.Exists($"res://Res/Blueprint/{code}.tres") ?
		ResourceLoader.Load<SubBlueprint>($"res://Res/DamageControlData/{code}.tres") : null;

	[Export]
	public int RequireNum { get; set; }
}
