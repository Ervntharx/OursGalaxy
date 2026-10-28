using Godot;
namespace OursGalaxy;

/// <summary>
/// 加速度图。当前使用最简实现
/// </summary>
internal class AccelerationMap {
	public float MaxVelocity { get; set; }

	public float GetAcceleration(float velocity, float delta) {
		var t = 1.0f - Mathf.Exp(-10.0f * delta);
		return float.Lerp(velocity, MaxVelocity, t);
	}
}
