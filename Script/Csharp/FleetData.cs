using Godot;
using Godot.Collections;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 舰队数据类, 用于存储舰队的基本信息和状态
/// </summary>
[GlobalClass]
internal partial class FleetData : Resource {
	public const string DEFAULT_NAME = "Untitled Fleet";

	/// <summary>
	/// 舰队ID
	/// </summary>
	[Export] public GUID ID { get; set; }

	/// <summary>
	/// 舰队名
	/// </summary>
	[Export] public string Name { get; set; } = DEFAULT_NAME;

	/// <summary>
	/// 舰队资产
	/// </summary>
	[Export] public SubRepository Repository { get; set; } = new();

	/// <summary>
	/// 旗舰ID,该值也在Ships中
	/// </summary>
	[Export] public SpaceshipReference FlagshipReference { get; set; }

	/// <summary>
	/// 舰船Reference和战术设置
	/// </summary>
	[Export] public Dictionary<SpaceshipReference, TacticsSet> Ships { get; set; } = [];


	public FleetData(SpaceshipData flagship) {
		ID = GUID.Generate();
		Repository.Add(flagship);
		FlagshipReference = flagship.MakeReference();
		Ships.Add(FlagshipReference, new());
	}

	/// <summary>
	/// ⚠️不要使用无参构造函数构造无意义量
	/// </summary>
	protected FleetData() {
		ID = GUID.Empty;
		var tmpShip = new SpaceshipData(SpaceshipConfig.TEMPLATE_NAMECODE, ItemBaseConfig.QulityEnum.White);
		Repository.Add(tmpShip);
		FlagshipReference = tmpShip.MakeReference();
		Ships.Add(FlagshipReference, new());
	}
}
