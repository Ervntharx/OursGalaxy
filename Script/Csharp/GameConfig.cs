using Godot;

namespace OursGalaxy;

/// <summary>
/// 游戏配置数据单例
/// </summary>
[GlobalClass]
internal partial class GameConfig : Resource {
	public static GameConfig Data { get; private set; } = new();
}
