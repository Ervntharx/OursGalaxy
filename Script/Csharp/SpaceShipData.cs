using System;
using System.IO;
using System.Linq;
using Godot;
using OursGalaxy.DataBase;
namespace OursGalaxy;
/// <summary>
/// 飞船数据类, 用于存储飞船的基本信息和状态
/// </summary>
[GlobalClass]
internal partial class SpaceshipData : UniqueItem {
	public new static readonly SpaceshipData Empty = new();
	protected readonly static string ItemCodePrefix = "Ship_";

	[Export]
	public string? CustomName { get; set; }

	[Export]
	public int CurrentHP { get; set; }

	[Export]
	public Godot.Collections.Dictionary<Outlet, EquipData> EquipsConfig { get; set; } = [];

	public int FullHP => fullHP.Value;

	public int Size => size.Value;

	public int Raid => raid.Value;

	public float Range => range.Value;

	public SpaceshipData(string typeName, GUID? ID = null) : base(ItemCodePrefix + typeName, ID) {
		// GameDataBase.Connect();
		size = new(() => {
			if (int.TryParse(Query("Size"), out var result)) {
				return result;
			}
			throw new IOException("Can't query valid data");
		});

		fullHP = new(() => {
			if (int.TryParse(Query("HP"), out var result)) {
				return result;
			}
			throw new IOException("Can't query valid data");
		});

		raid = new(() => {
			if (int.TryParse(Query("Raid"), out var result)) {
				return result;
			}
			throw new IOException("Can't query valid data");
		});

		range = new(() => {
			var leastRange = 0f;
			foreach (var turret in EquipsConfig.Select(out2Equip => out2Equip.Value).OfType<TurretBase>()) {
				if (leastRange == 0) {
					leastRange = Math.Max(leastRange, turret.Range);
				}
				else {
					leastRange = Math.Min(leastRange, turret.Range);
				}
			}
			return leastRange;
		});
		// GameDataBase.Disconnet();
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
