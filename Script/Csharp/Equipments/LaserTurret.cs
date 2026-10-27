using Godot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace OursGalaxy;

internal partial class LaserTurret : Node3D, ITurret {
	public static LaserTurret Load() => (LaserTurret)ResourceLoader.Load<PackedScene>("res://Scene/LaserTurret.tscn").Instantiate();

	[Export]
	protected Node3D? RotatableComponent { get; set; }

	[Export]
	protected ShapeCast3D? RayShape { get; set; }

	public const double APPROACHING_EQUAL_ANGLE = 0.05f;

	public OutletSizeType SizeType { get; protected set; } = new(OutletSizeType.SizeTypeEnum.S);

	public long CDMisc { get; set; }

	public IReadOnlyCollection<IPromote> Promotes => promotes.AsReadOnly();

	public float RotateAngleVelocity { get; set; } = 45;

	public ITurret.StateEnum State { get; protected set; } = ITurret.StateEnum.Ready;

	public float LeftYawMax { get; set; } = 180;
	public float RightYawMax { get; set; } = 180;

	public bool IsAimmed {
		get {
			return (float)new Quaternion(RotatableComponent!.GlobalBasis).AngleTo(new Quaternion(TargetBasis)) <= APPROACHING_EQUAL_ANGLE;
		}
	}

	public float Range {
		get => ((CylinderShape3D)RayShape!.Shape).Height; set {
			((CylinderShape3D)RayShape!.Shape).Height = value;
			var targetPos = RayShape!.TargetPosition;
			targetPos.Y = value / 2;
			RayShape.TargetPosition = targetPos;
		}
	}

	public Basis TargetBasis { get; set; }

	public ulong last_damage_time = 0;

	public void Install(Outlet outlet) {
		LeftYawMax = outlet.LimitYawLeft;
		RightYawMax = outlet.LimitYawRight;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		// rotate
		if (!IsAimmed) {
			var lkQ = new Quaternion(Basis.LookingAt(TargetBasis.Z.Normalized(), Vector3.Up));
			var euler = lkQ.GetEuler();
			euler.X = 0;
			euler.Y = Mathf.Clamp(euler.Y, Mathf.DegToRad(LeftYawMax), Mathf.DegToRad(RightYawMax));
			var clampedQ = Quaternion.FromEuler(euler);

			var angleDiff = new Quaternion(RotatableComponent!.Basis).AngleTo(clampedQ);
			var maxAngle = Mathf.DegToRad(RotateAngleVelocity) * (float)delta;

			RotatableComponent!.Basis = new(new Quaternion(RotatableComponent!.Basis).Slerp(clampedQ, (float)(angleDiff > 0.001f ? Mathf.Min(maxAngle / angleDiff, 1.0f) : 1.0f)));
		}
		// fire
		if (State == ITurret.StateEnum.Ready) {
			if (IsAimmed) {
				State = ITurret.StateEnum.Fire;
				GD.Print("try fire");
				RayShape!.Enabled = true;
			}

		}
		else if (State == ITurret.StateEnum.Fire) {
			// TODO: laser开火伤害判定次数
			var targets_ls = new List<GodotObject>();
			for (int i = 0, range = RayShape!.GetCollisionCount(); i < range; i++) {
				var target = RayShape!.GetCollider(i);
				targets_ls.Add(target);
				// TODO: damage
			}
			if (targets_ls.Count != 0)
				GD.Print(string.Join(",", targets_ls.Select<GodotObject, string>(gdobject => gdobject.ToString())));
			last_damage_time = Time.GetTicksMsec();
		}
	}

	private List<IPromote> promotes = [];
}


