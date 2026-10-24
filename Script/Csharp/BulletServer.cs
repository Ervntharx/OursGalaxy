using Godot;
namespace OursGalaxy;

/// <summary>
/// 子弹服务器。但目前只提供共享静态场景根以快速实例化子弹的功能
/// </summary>
[GlobalClass]
internal partial class BulletServer : Node {
	/// <summary>
	/// 场景根。⚠️期望外部对之赋值
	/// </summary>
	public static Node3D? SceneRoot { get; set; }

	/// <summary>
	/// 场景根节点是否已准备好
	/// </summary>
	public static bool IsReadyToShoot => GodotObject.IsInstanceValid(SceneRoot);

	/// <summary>
	/// 添加子弹
	/// </summary>
	/// <param name="from">用于联动StaticsServer</param>
	/// <param name="bullet">子弹</param>
	/// <param name="initGlobalTranform">初始的3D变换，位置和方向</param>
	/// <param name="velocity">子弹速度</param>
	public static void Add(ISpaceEntity from, IBullet bullet, Transform3D initGlobalTranform, Vector3 velocity) {
		if (GodotObject.IsInstanceValid(SceneRoot)) {
			bullet.AsArea3D().GlobalTransform = initGlobalTranform;
			bullet.Velocity = velocity;
			SceneRoot.AddChild(bullet.AsArea3D());
		}
	}
}
