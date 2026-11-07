using Godot;
namespace OursGalaxy.Test;

internal partial class TestItemViewer : Panel {
	[Export]
	protected Button? OpenButton { get; set; }


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
