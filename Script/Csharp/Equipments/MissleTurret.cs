using Godot;
using System;
using System.Collections.Generic;
namespace OursGalaxy;

internal partial class MissleTurret : Node3D, ITurret {
	public static MissleTurret Load() => (MissleTurret)ResourceLoader.Load<PackedScene>("res://Scene/MissleTurret.tscn").Instantiate();

	public OutletSizeType SizeType { get; protected set; } = new(OutletSizeType.SizeTypeEnum.S);

	public long CDMisc { get; set; }

	public IReadOnlyCollection<IPromote> Promotes => promotes.AsReadOnly();

	public ITurret.StateEnum State => throw new NotImplementedException();

	public bool IsAimmed => throw new NotImplementedException();

	public float Range { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public Basis TargetBasis { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

	public void Install(Outlet outlet) { }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private List<IPromote> promotes = [];
}
