using Godot;
using System.Collections.Generic;
namespace OursGalaxy;

internal partial class LaserTurret : Node3D, ITurret {
	public static LaserTurret Load() => (LaserTurret)ResourceLoader.Load<PackedScene>("res://Scene/LaserTurret.tscn").Instantiate();

	[Export]
	public Node3D RotatableComponent { get; set; } = null!;

	// [Export]
	// protected ShapeCast3D? RayShape { get; set; }

	[Export]
	protected Area3D DamageArea { get; set; } = null!;
	[Export]
	protected CollisionShape3D DamageAreaShape { get; set; } = null!;

	public const double APPROACHING_EQUAL_ANGLE = 0.1f;

	public OutletSizeType SizeType { get; protected set; } = new(OutletSizeType.SizeTypeEnum.S);

	public ulong CDMisc { get; set; }

	public IReadOnlyCollection<IPromote> Promotes => promotes.AsReadOnly();

	public float RotateAngleVelocity { get; set; } = 45;

	public ITurret.StateEnum State { get; protected set; } = ITurret.StateEnum.Ready;

	public float LeftYawMax { get; set; } = (180);
	public float RightYawMax { get; set; } = (180);

	public bool IsAimmed => RotatableComponent.GlobalBasis.Z.AngleTo(-TargetBasis.Z) <= float.DegreesToRadians(2f);

	public float Range {
		// get => ((CylinderShape3D)RayShape!.Shape).Height; set {
		// 	((CylinderShape3D)RayShape!.Shape).Height = value;
		// 	var targetPos = RayShape!.TargetPosition;
		// 	targetPos.Y = value / 2;
		// 	RayShape.TargetPosition = targetPos;
		// }
		get {
			return ((CylinderShape3D)(DamageAreaShape.Shape)).Height;
		}
		set {
			((CylinderShape3D)(DamageAreaShape.Shape)).Height = Range;
			var pos = Position;
			pos.Z = -value / 2;
		}
	}

	public Basis TargetBasis { get; set; }

	public ulong last_damage_time = 0;

	public void Install(Outlet outlet) {
		LeftYawMax = outlet.LimitYawLeft;
		RightYawMax = outlet.LimitYawRight;
	}

	public override void _Ready() {
		base._Ready();
		// var shape = ((CylinderShape3D)(RayShape.Shape));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		ProcessRotate(delta);
		// fire
		if (State == ITurret.StateEnum.Ready) {
			if (IsAimmed) {
				// RayShape!.Enabled = true;
				State = ITurret.StateEnum.Fire;
			}

		}
		else if (State == ITurret.StateEnum.Fire) {
			// TODO: laser开火伤害判定次数
			if (Time.GetTicksMsec() - last_damage_time >= CDMisc) {
				GD.Print("try fire");
				// var targets_ls = new List<GodotObject>();
				// for (int i = 0, range = RayShape!.GetCollisionCount(); i < range; i++) {
				// 	var target = RayShape!.GetCollider(i);
				// 	targets_ls.Add(target);
				// }

				var targets_ls = DamageArea.GetOverlappingBodies();
				if (targets_ls.Count != 0) {
					GD.Print(targets_ls);
				}
				// TODO: damage
				// GD.Print(string.Join(",", targets_ls.Select(gdobj => ((Node3D)gdobj).GlobalPosition)));
				last_damage_time = Time.GetTicksMsec();
			}

		}
	}

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);
	}

	private void ProcessRotate(double delta) {
		var currentLocalBasis = RotatableComponent.Basis;
		var localDir = (GlobalBasis.Inverse() * TargetBasis).Z;
		localDir.Y = 0;
		if (localDir.Length() <= 0.005f) {
			return;
		}
		var targetLocalBasis = Basis.LookingAt(localDir, Vector3.Up);
		var currentLocalQ = new Quaternion(currentLocalBasis);
		var targetLocalQ = new Quaternion(targetLocalBasis);

		targetLocalQ = ClampAngle(targetLocalQ, 180, 180);

		if (currentLocalQ.AngleTo(targetLocalQ) > float.DegreesToRadians(RotateAngleVelocity) * (float)delta) {
			currentLocalQ = currentLocalQ.Slerp(targetLocalQ, float.DegreesToRadians(RotateAngleVelocity) * (float)delta / currentLocalQ.AngleTo(targetLocalQ));
		}
		else {
			currentLocalQ = targetLocalQ;
		}
		RotatableComponent.Basis = new Basis(currentLocalQ);
	}

	private Quaternion ClampAngle(Quaternion dir, float leftClampAngle, float rightClampAngle) {
		var euler = dir.GetEuler(EulerOrder.Yxz);
		euler.Y = Mathf.Clamp(euler.Y, float.DegreesToRadians(-leftClampAngle), float.DegreesToRadians(rightClampAngle));
		return Quaternion.FromEuler(euler);
	}

	private List<IPromote> promotes = [];
}


