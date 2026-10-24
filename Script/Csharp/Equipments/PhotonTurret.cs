using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
namespace OursGalaxy;

internal partial class PhotonTurret : Node3D, ITurret {
	public static PhotonTurret Load() => (PhotonTurret)ResourceLoader.Load<PackedScene>("res://Scene/PhotonTurret.tscn").Instantiate();

	public IEquip.OutletTypeEnum SizeType { get; set; } = IEquip.OutletTypeEnum.M;
	public string? SpecialTypeName { get; set; }

	public long CDMisc { get; set; }

	public IReadOnlyCollection<IPromote> Promotes => promotes.AsReadOnly();

	[Export]
	protected NodePath? RotateComponent { get; set; }

	/// <summary>
	/// 目标方向，注意是全局的Basis。内部将自动转换为局部方向
	/// </summary>
	public Basis TargetBasis { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private List<IPromote> promotes = [];
}
