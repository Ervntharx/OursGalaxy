using Godot;
using System;
using System.Collections.Generic;
namespace OursGalaxy;

internal partial class MissleTurret : Node3D, ITurret {
	public static MissleTurret Load() => (MissleTurret)ResourceLoader.Load<PackedScene>("res://Scene/MissleTurret.tscn").Instantiate();

	[Export]
	public Area3D RangeArea { get; set; } = null!;
	[Export]
	public CollisionShape3D RangeAreaShape { get; set; } = null!;

	public OutletSizeType SizeType { get; protected set; } = new(OutletSizeType.SizeTypeEnum.S);

	public ulong CDMisc { get; set; } = 3000;

	public IReadOnlyCollection<IPromote> Promotes => promotes.AsReadOnly();

	public ITurret.StateEnum State => throw new NotImplementedException();

	public Node3D? TargetEntity { get; set; }

	public bool IsAimmed => GodotObject.IsInstanceValid(TargetEntity) && TargetEntity.GlobalPosition.DistanceTo(GlobalPosition) <= Range;

	public float Range { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public Basis TargetBasis { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

	public PackedScene? BulletRes { get; set; }

	public void Install(Outlet outlet) { }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private List<IPromote> promotes = [];
}
