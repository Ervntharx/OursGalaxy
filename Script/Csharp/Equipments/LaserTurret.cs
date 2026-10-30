using Godot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Formats.Tar;
using System.Linq;
using System.Threading.Tasks;
namespace OursGalaxy;

internal partial class LaserTurret : Node3D, ITurret {
	public static LaserTurret Load() => (LaserTurret)ResourceLoader.Load<PackedScene>("res://Scene/LaserTurret.tscn").Instantiate();

	[Export]
	public Node3D RotatableComponent { get; set; } = null!;

	[Export]
	protected ShapeCast3D? RayShape { get; set; }

	public const double APPROACHING_EQUAL_ANGLE = 0.1f;

	public OutletSizeType SizeType { get; protected set; } = new(OutletSizeType.SizeTypeEnum.S);

	public ulong CDMisc { get; set; }

	public IReadOnlyCollection<IPromote> Promotes => promotes.AsReadOnly();

	public float RotateAngleVelocity { get; set; } = 45;

	public ITurret.StateEnum State { get; protected set; } = ITurret.StateEnum.Ready;

	public float LeftYawMax { get; set; } = (180);
	public float RightYawMax { get; set; } = (180);

	public bool IsAimmed {
		get {
			return false;

			var localTargetZ = (GlobalBasis.Inverse() * TargetBasis).Z;
			localTargetZ.Y = 0;
			if (localTargetZ.LengthSquared() < 0.001f) {
				// 目标在正上方或正下方
				return false;
			}
			localTargetZ = localTargetZ.Normalized();
			var localTargetQ = Basis.LookingAt(localTargetZ, Vector3.Up).GetRotationQuaternion();
			var currentQ = RotatableComponent.Basis.GetRotationQuaternion();
			var angle = currentQ.AngleTo(localTargetQ);
			return angle <= Mathf.DegToRad(APPROACHING_EQUAL_ANGLE);
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

	public override void _Ready() {
		base._Ready();
		// var shape = ((CylinderShape3D)(RayShape.Shape));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		// rotate
		ProcessRotation(delta);
		// fire
		if (State == ITurret.StateEnum.Ready) {
			if (IsAimmed) {
				RayShape!.Enabled = true;
				State = ITurret.StateEnum.Fire;
			}

		}
		else if (State == ITurret.StateEnum.Fire) {
			// TODO: laser开火伤害判定次数
			if (Time.GetTicksMsec() - last_damage_time >= CDMisc) {
				var targets_ls = new List<GodotObject>();
				for (int i = 0, range = RayShape!.GetCollisionCount(); i < range; i++) {
					var target = RayShape!.GetCollider(i);
					targets_ls.Add(target);
				}
				// TODO: damage
				// GD.Print(string.Join(",", targets_ls.Select(gdobj => ((Node3D)gdobj).GlobalPosition)));
				last_damage_time = Time.GetTicksMsec();
			}

		}
	}

	public override void _PhysicsProcess(double delta) {
		RayShape.ForceShapecastUpdate();

		base._PhysicsProcess(delta);
	}

	private void ProcessRotation(double delta) {
		// if (IsAimmed) return;

		// 1. 将目标方向直接转换到基座的局部空间
		// 假设目标前向是 -Z（如果是其他，请换成 TargetBasis.X 或 Y）
		// GD.Print(TargetBasis);
		Vector3 targetForwardWorld = -TargetBasis.Z;

		Vector3 localDir = GlobalBasis.Inverse() * targetForwardWorld;

		// GD.Print(localDir);
		// 2. 在局部空间打平 (丢弃 Y 轴)
		localDir.Y = 0;
		if (localDir.LengthSquared() < 0.001f) return; // 目标在正上/正下方

		// 3. 纯数学计算目标偏航角（Yaw），不会有任何万向锁或翻滚干扰
		// Godot 的前向是 -Z。所以 Yaw 是绕着 Y 轴转。
		float targetYaw = Mathf.Atan2(-localDir.X, -localDir.Z);


		// 4. 限制偏航角范围 (度转弧度)
		targetYaw = Mathf.Clamp(targetYaw, -Mathf.DegToRad(LeftYawMax), Mathf.DegToRad(RightYawMax));
		GD.Print(targetYaw);
		// 5. 构造仅绕局部 Y 轴旋转的四元数 (绝对纯净)
		Quaternion targetQ = new Quaternion(Vector3.Up, targetYaw);
		Quaternion currentQ = RotatableComponent.Basis.GetRotationQuaternion();

		// 6. 计算插值
		float angleDiff = currentQ.AngleTo(targetQ);
		float maxAngle = Mathf.DegToRad(RotateAngleVelocity) * (float)delta; // 记得速度转弧度

		// GD.Print(angleDiff);
		if (angleDiff > 0.001f) {
			if (angleDiff > maxAngle) {
				currentQ = currentQ.Slerp(targetQ, maxAngle / angleDiff);
			}
			else {
				currentQ = targetQ;
			}
			// 7. 赋值给子节点的局部 Basis
			RotatableComponent.Basis = new Basis(currentQ.Normalized());
		}
	}

	private List<IPromote> promotes = [];
}


