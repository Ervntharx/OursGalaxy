using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class PowerUpItemBox : Button {
	public PowerUpItemBox Load() => (PowerUpItemBox)ResourceLoader.Load<PackedScene>("res://Scene/PowerUpItemBox.tscn").Instantiate();

	[Export]
	protected RichTextLabel TitleLabel { get; set; } = null!;
	[Export]
	protected ProgressBar PgrsBar { get; set; } = null!;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
