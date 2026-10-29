using Godot;
namespace OursGalaxy;

internal partial class TemplateShip : Spaceship {
	public static TemplateShip Load() => (TemplateShip)ResourceLoader.Load<PackedScene>("res://Script/Csharp/Ships/TemplateShip.tscn").Instantiate();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
