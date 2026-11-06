using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Godot;
using OursGalaxy.UI;
namespace OursGalaxy;

/// <summary>
/// 子弹服务器。但目前只提供共享静态场景根以快速实例化子弹的功能
/// </summary>
[GlobalClass]
internal partial class BulletServer : Node {
	/// <summary>
	/// 场景根。⚠️期望外部对之赋值
	/// </summary>
	[Obsolete]
	public static Node3D? SceneRoot { get; set; }

	/// <summary>
	/// 场景根节点是否已准备好
	/// </summary>
	[Obsolete]
	public static bool ServerReady => GodotObject.IsInstanceValid(SceneRoot);

	[Obsolete("Push instead")]
	public static void Add(IBattleEntity from, BulletBase bullet, Transform3D initGlobalTransform, Vector3 initVelocity) {
		if (ServerReady) {
			bullet.GlobalTransform = initGlobalTransform;
			bullet.LinearVelocity = initVelocity;
			SceneRoot!.AddChild(bullet);
		}
	}

	public static bool ContainsScene(string sceneName) => activedScene.Keys.Contains(sceneName);

	public static void Register(string sceneName, CombatStage scene) {
		if (!activedScene.Keys.Contains(sceneName)) {
			activedScene.Add(sceneName, scene);
			return;
		}
		GD.PushWarning($"<BulletServer> Try to regitser scene for one more time: {sceneName}");
	}

	public static void Push(GUID from, BulletBase bullet, Transform3D initGlobalTransform, Vector3 initVelocity) {
		bullet.GlobalTransform = initGlobalTransform;
		bullet.LinearVelocity = initVelocity;
		if (mapCache.ContainsKey(from)) {
			activedScene[mapCache[from]].AddChild(bullet);
			return;
		}
		else {
			var success = false;
			foreach (var scene in activedScene.Values) {
				var fromTarget = scene.GetChildren().FirstOrDefault(child => child is Spaceship ship && ship.ID == from);
				if (fromTarget is null) {
					continue;
				}
				fromTarget.AddChild(bullet);
				success = true;
			}
			if (!success) {
				GD.PushError("<BulletServer>Unknown GUID, Don't know which combat stage to push in");
			}
		}
	}

	public static void Free(string sceneName) {
		activedScene.Remove(sceneName);
	}

	private static Dictionary<string, CombatStage> activedScene = [];
	private static Dictionary<GUID, string> mapCache = [];
}
