using System;
using System.IO;
using Godot;
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
	public Godot.Collections.Dictionary<Outlet, UniqueItem> EquipsConfig { get; set; } = [];

	public int FullHP => fullHP.Value;

	public int Size => size.Value;

	public int Raid => raid.Value;

	public int Range => range.Value;

	public SpaceshipData(string typeName, GUID? ID = null) : base(ItemCodePrefix + typeName, ID) {
		size = new(() => {
			if (TryQuery<int>("Size", out var result)) {
				return result;
			}
			throw new IOException("Can't query valid data");
		});

		fullHP = new(() => {
			if (TryQuery<int>("HP", out var result)) {
				return result;
			}
			throw new IOException("Can't query valid data");
		});

		raid = new(() => {
			if (TryQuery<int>("Raid", out var result)) {
				return result;
			}
			throw new IOException("Can't query valid data");
		});

		range = new(() => {
			var leastRange = 0;
			foreach (var equip in EquipsConfig) {
				// TODO:	转为非硬编码的射程统计
				var tableName = equip.Value.GetTableName();
				if (tableName == "Laser" || tableName == "Missile" || tableName == "Photon") {
					if (TryQuery<int>("Range", out int singleRange)) {
						if (leastRange == 0) {
							leastRange = Math.Max(leastRange, singleRange);
						}
						else {
							leastRange = Math.Min(leastRange, singleRange);
						}
						continue;
					}
					throw new IOException("Can't query valid data");
				}
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
		range = new(0);
	}

	private Lazy<int> fullHP;
	private Lazy<int> size;
	private Lazy<int> raid;
	private Lazy<int> range;
}
