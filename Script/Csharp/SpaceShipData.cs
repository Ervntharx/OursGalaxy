using System;
using System.Linq;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 飞船数据类, 用于存储飞船的基本信息和状态
/// </summary>
[GlobalClass]
internal partial class SpaceshipData : UniqueItem {
	[Export] public string? CustomName { get; set; }

	[Export(PropertyHint.Range, "1,0,99999")]
	public int CurrentHP { get; set; }

	[Export]
	public Godot.Collections.Array<OutletData> EquipsLs { get; set; } = [];

	public int FullHP => fullHP.Value;
	public int Size => size.Value;
	public int Raid => raid.Value;
	public float Range => range.Value;

	/// <summary>
	/// 从Config构造Data
	/// </summary>
	/// <param name="config"></param>
	/// <returns></returns>
	public static SpaceshipData From(SpaceshipConfig config) {
		return new(config.ShipTypeName);
	}

	public SpaceshipData(string typeName, GUID? ID = null) : base($"{SpaceshipConfig.ItemCodePrefix}_{typeName}", ID) {
		// GameDataBase.Connect();
		size = new(() => ResourceLib.QueryConfig<SpaceshipConfig>(Code).Size);

		fullHP = new(() => ResourceLib.QueryConfig<SpaceshipConfig>(Code).HP);

		raid = new(() => ResourceLib.QueryConfig<SpaceshipConfig>(Code).Raid);

		range = new(() => {
			var leastRange = 0f;
			foreach (var turret in EquipsLs.Select(out2Equip => out2Equip.InstalledEquip).OfType<TurretData>()) {
				leastRange = leastRange == 0 ? Math.Max(leastRange, turret.Range) : Math.Min(leastRange, turret.Range);
			}
			return leastRange;
		});
	}

	public string GetShipTypeName() => GetItemName();

	/// <summary>
	/// ⚠️避免使用无参构造函数构造无意义量
	/// </summary>
	protected SpaceshipData() : base() {
		size = new(0);
		fullHP = new(0);
		raid = new(0);
		range = new(0f);
	}

	private Lazy<int> fullHP;
	private Lazy<int> size;
	private Lazy<int> raid;
	private Lazy<float> range;
}
