using Godot;
namespace OursGalaxy.Test;

internal partial class RotateSolutionBackPack {
	protected Node3D? TurretBasis { get; set; }
	protected Node3D? RotatablePart { get; set; }
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
		RotatablePart!.GlobalBasis = RotatablePart!.GlobalBasis.Slerp(Basis.LookingAt(-targetForward.Normalized(), Vector3.Up), t).Orthonormalized();
	}

	protected void Solution_RotateInAllDirectionWithoutLimit(double delta) {
		Quaternion current = new(RotatablePart!.GlobalBasis);
		Quaternion target = new(TargetBasis);
		// for testing: camera dir is -z
		target = -target;
		var angleDiff = current.AngleTo(target);
		var maxAngle = RotateAngleVelocity * (float)delta;
		if (angleDiff > maxAngle) {
			RotatablePart.GlobalBasis = new(current.Slerp(target, (float)(maxAngle / angleDiff)));
		}
		else {
			RotatablePart.GlobalBasis = new(target);
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
		var angleDiff = new Quaternion(RotatablePart!.GlobalBasis).AngleTo(clampedQ);
		var maxAngle = Mathf.DegToRad(RotateAngleVelocity) * (float)delta;

		RotatablePart.GlobalBasis = new Basis(new Quaternion(RotatablePart!.GlobalBasis).Slerp(clampedQ, (float)(angleDiff > 0.001f ? Mathf.Min(maxAngle / angleDiff, 1.0f) : 1.0f)));
	}

	protected void Solution_RotateInLocalSpaceWithLimit(double delta) {
		Vector3 localTargetDir = RotatablePart!.GlobalBasis.Inverse() * TargetBasis.Z.Normalized();
		var targetYaw = Mathf.Atan2(localTargetDir.X, localTargetDir.Z);    // 左右
		var targetPitch = Mathf.Atan2(-localTargetDir.Y, localTargetDir.Z);  // 上下

		// 4. 限制角度
		targetYaw = Mathf.Clamp(targetYaw, Mathf.DegToRad(MinYaw), Mathf.DegToRad(MaxYaw));
		targetPitch = Mathf.Clamp(targetPitch, Mathf.DegToRad(MinPitch), Mathf.DegToRad(MaxPitch));

		//    先偏航（Y轴），再俯仰（X轴)
		var targetBasis = RotatablePart.Basis * Basis.Identity.Rotated(Vector3.Up, targetYaw).Rotated(Vector3.Right, targetPitch);

		Quaternion curQ = new(RotatablePart.Basis);
		Quaternion targetQ = new(targetBasis);
		var angleDiff = curQ.AngleTo(targetQ);
		var maxAngle = Mathf.DegToRad(RotateAngleVelocity) * (float)delta;
		var t = angleDiff > 0.0001f ? Mathf.Min(maxAngle / angleDiff, 1.0f) : 1.0f;

		RotatablePart.Basis = new Basis(curQ.Slerp(targetQ, t)).Orthonormalized();
	}

	/// <summary>
	/// 旋转到给定全局Basis方向到本地平面的投影。只旋转Yaw
	/// </summary>
	/// <param name="delta"></param>
	/// <param name="turretBase">炮基</param>
	/// <param name="rotatablePart">炮可旋转部分</param>
	/// <param name="targetGlobalBasis">目标全局Basis</param>
	/// <param name="leftYawClampAngleInDegree">左限制角度，角度单位</param>
	/// <param name="rightYawClampAngleInDegree">右限制角度，角度单位</param>
	private void RotateYaw(double delta, Node3D turretBase, Node3D rotatablePart, Basis targetGlobalBasis, float leftYawClampAngleInDegree, float rightYawClampAngleInDegree) {
		var currentLocalBasis = rotatablePart.Basis;
		var localDir = -(turretBase.GlobalBasis.Inverse() * targetGlobalBasis).Z;
		localDir.Y = 0;
		if (localDir.Length() <= 0.005f) {
			return;
		}
		var targetLocalBasis = Basis.LookingAt(localDir, Vector3.Up);
		var currentLocalQ = new Quaternion(currentLocalBasis);
		var targetLocalQ = new Quaternion(targetLocalBasis);

		targetLocalQ = ClampAngle(targetLocalQ, leftYawClampAngleInDegree, rightYawClampAngleInDegree);

		if (currentLocalQ.AngleTo(targetLocalQ) > float.DegreesToRadians(RotateAngleVelocity) * (float)delta) {
			currentLocalQ = currentLocalQ.Slerp(targetLocalQ, float.DegreesToRadians(RotateAngleVelocity) * (float)delta / currentLocalQ.AngleTo(targetLocalQ));
		}
		else {
			currentLocalQ = targetLocalQ;
		}
		rotatablePart.Basis = new Basis(currentLocalQ);
		return;

		Quaternion ClampAngle(Quaternion dir, float leftClampAngle, float rightClampAngle) {
			var euler = dir.GetEuler(EulerOrder.Yxz);
			euler.Y = Mathf.Clamp(euler.Y, float.DegreesToRadians(-leftClampAngle), float.DegreesToRadians(rightClampAngle));
			return Quaternion.FromEuler(euler);
		}
	}

	/// <summary>
	/// 朝向给定targetEntty Node3D。只旋转Yaw
	/// </summary>
	/// <param name="delta"></param>
	/// <param name="turretBase"></param>
	/// <param name="rotatablePart"></param>
	/// <param name="targetEntity"></param>
	/// <param name="leftYawClampAngle"></param>
	/// <param name="rightYawClampAngle"></param>
	private void RotateV2(double delta, Node3D turretBase, Node3D rotatablePart, Node3D targetEntity, float leftYawClampAngle, float rightYawClampAngle) {
		var currentLocalBasis = rotatablePart.Basis;
		if (GodotObject.IsInstanceValid(targetEntity)) {
			var localDir = turretBase.GlobalTransform.Inverse() * targetEntity.GlobalPosition;
			localDir.Y = 0;
			var targetLocalBasis = Basis.LookingAt(localDir.Normalized(), Vector3.Up);
			var currentLocalQ = new Quaternion(currentLocalBasis);
			var targetLocalQ = new Quaternion(targetLocalBasis);

			targetLocalQ = ClampAngle(targetLocalQ, leftYawClampAngle, rightYawClampAngle);

			if (currentLocalQ.AngleTo(targetLocalQ) > float.DegreesToRadians(RotateAngleVelocity) * (float)delta) {
				currentLocalQ = currentLocalQ.Slerp(targetLocalQ, float.DegreesToRadians(RotateAngleVelocity) * (float)delta / currentLocalQ.AngleTo(targetLocalQ));
			}
			else {
				currentLocalQ = targetLocalQ;
			}
			rotatablePart.Basis = new Basis(currentLocalQ);
		}
		Quaternion ClampAngle(Quaternion dir, float leftClampAngle, float rightClampAngle) {
			var euler = dir.GetEuler(EulerOrder.Yxz);
			euler.Y = Mathf.Clamp(euler.Y, float.DegreesToRadians(-leftClampAngle), float.DegreesToRadians(rightClampAngle));
			return Quaternion.FromEuler(euler);
		}
	}
}
