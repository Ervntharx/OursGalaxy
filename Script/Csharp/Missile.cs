using Godot;
using System;
namespace OursGalaxy;

internal sealed partial class Missile : BulletBase {
	private const float DEFAULT_CHANGE_STEP_ANGLE = 10;
	private const float DEFAULT_STEP_1_ROTATE_ANGLE_VELOCITY = 150;
	private const float DEFAULT_STEP_2_ROTATE_ANGLE_VELOCITY = 25;
	public new static Missile Load() => (Missile)ResourceLoader.Load<PackedScene>("res://Scene/Bullets/Missile.tscn").Instantiate();

	/// <summary>
	/// 长跟踪。不会脱锁的导弹为VLF类型
	/// </summary>
	public bool VLF { get; set; }

	public IBattleEntity? Target { get; set; }

	/// <summary>
	/// 切换制导阶段的临界角度。如果不是VLF的话，与目标的夹角小于该角度时切换为第二段制导
	/// </summary>
	public float ChangeStepAngle { get; set; } = DEFAULT_CHANGE_STEP_ANGLE;

	/// <summary>
	/// 制导阶段数
	/// </summary>
	private enum StepEnum { Step1, Step2 }
	private StepEnum Step = StepEnum.Step1;

	/// <summary>
	/// 一阶段角速度
	/// </summary>
	public float Step1RotateVelocity { get; private set; } = DEFAULT_STEP_1_ROTATE_ANGLE_VELOCITY;

	/// <summary>
	/// 二阶段角速度
	/// </summary>
	public float Step2RotateVelocity { get; private set; } = DEFAULT_STEP_2_ROTATE_ANGLE_VELOCITY;

	public override void _PhysicsProcess(double delta) {
		if (GodotObject.IsInstanceValid(Target?.AsNode3D())) {
			var targetOffPos = Target.AsNode3D().GlobalTransform.Origin - GlobalTransform.Origin;
			var targetQ = new Quaternion(Vector3.Forward, targetOffPos.Normalized());
			var currentQ = GlobalBasis.Orthonormalized().GetRotationQuaternion().Normalized();

			switch (Step) {
				case StepEnum.Step1:
					if (!VLF && currentQ.AngleTo(targetQ) <= ChangeStepAngle) {
						Step = StepEnum.Step2;
						break;
					}
					GlobalBasis = new(currentQ.Slerp(targetQ, Math.Clamp(Step1RotateVelocity * (float)delta / currentQ.AngleTo(targetQ), 0f, 1f)));
					break;
				case StepEnum.Step2:
					if (!currentQ.IsEqualApprox(targetQ)) {
						GlobalBasis = new(currentQ.Slerp(targetQ, Math.Clamp(Step2RotateVelocity * (float)delta / currentQ.AngleTo(targetQ), 0f, 1f)));
					}
					break;
				default:
					break;
			}
			if (currentQ.IsEqualApprox(targetQ)) {

			}
			else {
				GlobalBasis = new(currentQ);
			}
		}
		base._PhysicsProcess(delta);
	}
}

