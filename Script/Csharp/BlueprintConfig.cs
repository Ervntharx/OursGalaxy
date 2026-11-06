using Godot;
namespace OursGalaxy.ResourceConfig;

/// <summary>
/// 蓝图配置
/// </summary>
[GlobalClass]
internal partial class BlueprintConfig : ItemBaseConfig {
	public const string ITEM_CODE_PREFIX = "Blueprint";
	/// <summary>
	/// subprint合成blueprint所需的数量
	/// </summary>
	[Export]
	public int RequireSubNum { get; set; } = DEFAULT_REQUIRE_SUB_NUM;

	/// <summary>
	/// 材料列表
	/// </summary>
	[Export]
	public Godot.Collections.Array<ItemBaseConfig> Source { get; set; } = [];

	/// <summary>
	/// 产出列表
	/// </summary>
	[Export]
	public Godot.Collections.Array<ItemBaseConfig> Result { get; set; } = [];

	/// <summary>
	/// 所需时间（秒）
	/// </summary>
	[Export]
	public long TimeSec { get; set; } = DEFAULT_TIME_SEC;

	private const int DEFAULT_REQUIRE_SUB_NUM = 5;
	private const int DEFAULT_TIME_SEC = 3;
}

