using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class CombatBox : Button {
	public CombatBox Load() => (CombatBox)ResourceLoader.Load<PackedScene>("res://Scene/CombatBox.tscn").Instantiate();

	[Export]
	protected TextureRect? Avater { get; set; }
	[Export]
	protected RichTextLabel? NameLabel { get; set; }
	[Export]
	protected RichTextLabel? LevelLabel { get; set; }
	[Export]
	protected ProgressBar? PgrsBar { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
