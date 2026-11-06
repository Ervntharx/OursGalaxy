using System;
using System.Runtime.InteropServices;
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
	[Export]
	public GUID ID { get; set; }
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
		ID = GUID.Generate();

		size = new(() => {
			var result = SpaceshipLib.GetConfig(flagship).Size;
			foreach (var ship in Ships) {
				result += SpaceshipLib.GetConfig(ship.Key).Size;
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
			var result = SpaceshipLib.GetConfig(flagship).HP;
			foreach (var ship in Ships) {
				result += SpaceshipLib.GetConfig(ship.Key).HP;
			}
			return result;
		});

		raid = new(() => {
			var result = SpaceshipLib.GetConfig(flagship).Raid;
			foreach (var ship in Ships) {
				result += SpaceshipLib.GetConfig(ship.Key).Raid;
			}
			return result;
		});
	}

	/// <summary>
	/// ⚠️不要使用无参构造函数构造无意义量
	/// </summary>
	protected FleetData() {
		Flagship = new SpaceshipData("Template", ItemBaseConfig.QulityEnum.White);
		ID = GUID.Empty;

		size = new(0);
		hp = new(0);
		fullHP = new(0);
		raid = new(0);
	}

	private Lazy<int> size;
	private Lazy<int> hp;
	private Lazy<int> fullHP;
	private Lazy<int> raid;
}
