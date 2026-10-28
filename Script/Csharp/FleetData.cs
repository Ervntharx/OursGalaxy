using System;
using System.Runtime.InteropServices;
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
	public string UniqueID { get; set; }
	[Export]
	public string Name { get; set; } = DEFAULT_NAME;

	[Export]
	public SpaceshipData Flagship { get; set; }

	[Export]
	public Dictionary<SpaceshipData, TacticsSet> Ships { get; set; } = [];

	public int Size => size.Value;

	public int ShipsCount => Ships.Count;

	public int HP => hp.Value;

	public int FullHP => fullHP.Value;

	public int Raid => raid.Value;

	public FleetData(SpaceshipData flagship) {
		Flagship = flagship;
		UniqueID = GenerateUniqueID();

		size = new(() => {
			var result = Flagship.Size;
			foreach (var ship in Ships) {
				result += ship.Key.Size;
			}
			return result;
		});

		hp = new(() => {
			var result = Flagship.CurrentHP;
			foreach (var ship in Ships) {
				result += ship.Key.CurrentHP;
			}
			return result;
		});

		fullHP = new(() => {
			var result = Flagship.FullHP;
			foreach (var ship in Ships) {
				result += ship.Key.FullHP;
			}
			return result;
		});

		raid = new(() => {
			var result = Flagship.Raid;
			foreach (var ship in Ships) {
				result += ship.Key.Raid;
			}
			return result;
		});
	}

	/// <summary>
	/// ⚠️不要使用无参构造函数构造无意义量
	/// </summary>
	protected FleetData() {
		Flagship = SpaceshipData.Empty;
		UniqueID = GenerateUniqueID();

		size = new(0);
		hp = new(0);
		fullHP = new(0);
		raid = new(0);
	}

	/// <summary>
	/// 生成舰队ID
	/// 通过当前时间戳生成一个唯一的ID
	/// </summary>
	/// <returns></returns>
	private static string GenerateUniqueID() {
		return Time.GetDateStringFromSystem(true);
	}

	private Lazy<int> size;
	private Lazy<int> hp;
	private Lazy<int> fullHP;
	private Lazy<int> raid;
}
