using Godot;

[GlobalClass]
internal partial class DamageControlData : Resource {
	public static readonly DamageControlData Empty = new();
	public static DamageControlData Template => ResourceLoader.Load<DamageControlData>("res://Res/DamageControlData/Template.tres");

	public static DamageControlData? Load(string code) => ResourceLoader.Exists($"res://Res/DamageControlData/{code}.tres") ? ResourceLoader.Load<DamageControlData>($"res://Res/DamageControlData/{code}.tres") : null;

	[Export]
	public string Code { get; set; } = string.Empty;

	[Export]
	public float CDMisc { get; set; }

	[Export]
	public int Repair { get; set; }

	[Export]
	public int RemoveBadPromoteCount { get; set; }
}
