using Godot;
using Godot.Collections;
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
	[Export]
	public long ID { get; set; }
	[Export]
	public string Name { get; set; } = DEFAULT_NAME;

	[Export]
	public required SpaceshipData Flagship { get; set; }

	[Export]
	public Array<SpaceshipData> Ships { get; set; } = new();

	[Export]
	public Dictionary<SpaceshipData, TacticsSet> Tactics { get; set; } = new();

	public FleetData() {
		ID = GenerateID();
	}

	/// <summary>
	/// 生成舰队ID
	/// 通过当前时间戳生成一个唯一的ID
	/// </summary>
	/// <returns></returns>
	private static long GenerateID() {
		return Time.GetUnixTimeFromDatetimeDict(Time.GetDateDictFromSystem(true));
	}
}
