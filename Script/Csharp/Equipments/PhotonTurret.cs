using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
namespace OursGalaxy;

internal partial class PhotonTurret : Node3D, ITurret {
	public static PhotonTurret Load() => (PhotonTurret)ResourceLoader.Load<PackedScene>("res://Scene/PhotonTurret.tscn").Instantiate();

	public OutletSizeType SizeType { get; protected set; } = new(OutletSizeType.SizeTypeEnum.S);

	public ulong CDMisc { get; set; }

	public IReadOnlyCollection<IPromote> Promotes => promotes.AsReadOnly();

	[Export]
	protected NodePath? RotateComponent { get; set; }

	public float LeftYawMax { get; set; }
	public float RightYawMax { get; set; }

	/// <summary>
	/// 目标方向，注意是全局的Basis。内部将自动转换为局部方向
	/// </summary>
	public Basis TargetBasis { get; set; }

	public ITurret.StateEnum State => throw new NotImplementedException();

	public bool IsAimmed => throw new NotImplementedException();

	public float Range { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


	public void Install(Outlet outlet) {
		LeftYawMax = outlet.LimitYawLeft;
		RightYawMax = outlet.LimitYawRight;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private List<IPromote> promotes = [];
}
