using Godot;
namespace OursGalaxy.Test;

internal partial class RotateSolutionBackPack {
	protected Node3D? RotatableComponent { get; set; }
	protected Basis TargetBasis { get; set; }
	protected float RotateAngleVelocity = 15;

	protected float MaxPitch { get; set; } = 89.9f;
	protected float MinPitch { get; set; } = -30f;

	protected float MaxYaw { get; set; } = 150f;
	protected float MinYaw { get; set; } = -150f;

	protected void Solution_RotateYawWithDamp(double delta) {
		// for testing: camera dir is -z
		Vector3 targetForward = -TargetBasis.Z;
		// 到水平面
		targetForward.Y = 0;
		if (targetForward.LengthSquared() < 0.0001f) {
			// 目标在正上方/正下方时
			return;
		}
		var t = 1.0f - Mathf.Exp(-10.0f * (float)delta);    // 阻尼效果 slerp
		RotatableComponent!.GlobalBasis = RotatableComponent!.GlobalBasis.Slerp(Basis.LookingAt(-targetForward.Normalized(), Vector3.Up), t).Orthonormalized();
	}

	protected void Solution_RotateInAllDirectionWithoutLimit(double delta) {
		Quaternion current = new(RotatableComponent!.GlobalBasis);
		Quaternion target = new(TargetBasis);
		// for testing: camera dir is -z
		target = -target;
		var angleDiff = current.AngleTo(target);
		var maxAngle = RotateAngleVelocity * (float)delta;
		if (angleDiff > maxAngle) {
			RotatableComponent.GlobalBasis = new(current.Slerp(target, (float)(maxAngle / angleDiff)));
		}
		else {
			RotatableComponent.GlobalBasis = new(target);
		}
	}

	protected void Solution_RotateInAllDirectionWithLimit(double delta) {
		// 构造目标旋转：先算目标 Quaternion
		var lookQ = new Quaternion(Basis.LookingAt(-TargetBasis.Z.Normalized(), Vector3.Up));

		// 限制俯仰：把目标四元数转欧拉角，夹住 Pitch
		Vector3 euler = lookQ.GetEuler();
		euler.X = Mathf.Clamp(euler.X, Mathf.DegToRad(-MaxPitch), Mathf.DegToRad(MaxPitch));
		// euler.Y 是 Yaw，不限制
		euler.Z = 0; // 不滚转

		var clampedQ = Quaternion.FromEuler(euler);

		// 固定转速 Slerp
		var angleDiff = new Quaternion(RotatableComponent!.GlobalBasis).AngleTo(clampedQ);
		var maxAngle = Mathf.DegToRad(RotateAngleVelocity) * (float)delta;

		RotatableComponent.GlobalBasis = new Basis(new Quaternion(RotatableComponent!.GlobalBasis).Slerp(clampedQ, (float)(angleDiff > 0.001f ? Mathf.Min(maxAngle / angleDiff, 1.0f) : 1.0f)));
	}

	protected void Solution_RotateInLocalSpaceWithLimit(double delta) {
		Vector3 localTargetDir = RotatableComponent!.GlobalBasis.Inverse() * TargetBasis.Z.Normalized();
		var targetYaw = Mathf.Atan2(localTargetDir.X, localTargetDir.Z);    // 左右
		var targetPitch = Mathf.Atan2(-localTargetDir.Y, localTargetDir.Z);  // 上下

		// 4. 限制角度
		targetYaw = Mathf.Clamp(targetYaw, Mathf.DegToRad(MinYaw), Mathf.DegToRad(MaxYaw));
		targetPitch = Mathf.Clamp(targetPitch, Mathf.DegToRad(MinPitch), Mathf.DegToRad(MaxPitch));

		//    先偏航（Y轴），再俯仰（X轴)
		var targetBasis = RotatableComponent.Basis * Basis.Identity.Rotated(Vector3.Up, targetYaw).Rotated(Vector3.Right, targetPitch);

		Quaternion curQ = new(RotatableComponent.Basis);
		Quaternion targetQ = new(targetBasis);
		var angleDiff = curQ.AngleTo(targetQ);
		var maxAngle = Mathf.DegToRad(RotateAngleVelocity) * (float)delta;
		var t = angleDiff > 0.0001f ? Mathf.Min(maxAngle / angleDiff, 1.0f) : 1.0f;

		RotatableComponent.Basis = new Basis(curQ.Slerp(targetQ, t)).Orthonormalized();
	}
}
