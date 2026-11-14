using OursGalaxy.UI;
namespace OursGalaxy;

internal sealed class FleetEditorViewModel {
	/// <summary>
	/// 正在编辑的舰队ID
	/// </summary>
	public GUID? FleetID { get; private set; }

	/// <summary>
	/// 正在编辑的舰队名字
	/// </summary>
	public string? FleetName { get; private set; }

	/// <summary>
	/// 舰队资产库
	/// </summary>
	public SubRepository? FleetRepository { get; private set; }

	/// <summary>
	/// 旗舰ID
	/// </summary>
	public GUID? FlagshipID { get; private set; }

	/// <summary>
	/// 船和战术表
	/// </summary>
	public Godot.Collections.Dictionary<SpaceshipReference, TacticsSet> Ships { get; private set; } = [];

	/// <summary>
	/// UI
	/// </summary>
	public FleetEditorPanel? Panel { get; private set; }

	/// <summary>
	/// 船数
	/// </summary>
	public int? ShipsCount { get; private set; }

	/// <summary>
	/// 舰队规模
	/// </summary>
	public int? FleetSize { get; private set; }

	/// <summary>
	/// 舰队奇袭
	/// </summary>
	public int? FleetRaid { get; private set; }

	/// <summary>
	/// 关联到UI
	/// </summary>
	/// <param name="panel"></param>
	public void Load(FleetEditorPanel panel) {
		Panel = panel;
	}

	/// <summary>
	/// 读取FLeetData数据
	/// </summary>
	/// <param name="data"></param>
	public void Read(FleetData data) {
		FleetID = data.ID;
		FleetName = data.Name;

		FleetRepository = data.Repository;

		Ships = data.Ships;
	}

	public void Unload() {
		Panel = null;
	}

	private FleetData? data;
}
