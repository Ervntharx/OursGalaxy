using Godot;
using System;
using System.Collections.Generic;
namespace OursGalaxy;

internal partial class MissleTurret : Node3D, ITurret {
	public static MissleTurret Load() => (MissleTurret)ResourceLoader.Load<PackedScene>("res://Scene/MissleTurret.tscn").Instantiate();

	public IEquip.OutletTypeEnum SizeType { get; set; } = IEquip.OutletTypeEnum.M;
	public string? SpecialTypeName { get; set; }

	public long CDMisc { get; set; }

	public IReadOnlyCollection<IPromote> Promotes => promotes.AsReadOnly();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private List<IPromote> promotes = [];
}
