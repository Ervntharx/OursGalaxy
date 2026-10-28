using Godot;
using OursGalaxy;
using System;
namespace OursGalaxy;

internal partial class TestLifeVar : Control {
	private Lazy<int> a = new(() => 2);
	public override void _Ready() {
		base._Ready();
		GD.Print(a.IsValueCreated);
		GD.Print(a.Value);
		GD.Print(a.IsValueCreated);

	}
}
