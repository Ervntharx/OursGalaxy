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
		var success = false;
		foreach (var scene in activedScene.Values) {
			var fromTarget = scene.GetShips().FirstOrDefault(child => child is Spaceship ship && ship.ID == from);
			GD.Print($"<BulletServer> try found from ship {fromTarget?.CustomName ?? "???"}");
			if (fromTarget is null) {
				continue;
			}
			GD.Print("<BulletServer> Blu!");
			fromTarget.AddChild(bullet);
			success = true;
		}
		if (!success) {
			GD.PushError("<BulletServer>Unknown GUID, Don't know which combat stage to push in");
		}
	}

	public static void Free(string sceneName) {
		activedScene.Remove(sceneName);
	}

	private static Dictionary<string, CombatStage> activedScene = [];
}
