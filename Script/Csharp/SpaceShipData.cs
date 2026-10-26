using System;
using Godot;
/// <summary>
/// 飞船数据类, 用于存储飞船的基本信息和状态
/// </summary>
[GlobalClass]
internal partial class SpaceshipData : UniqueItem {
	public new static readonly SpaceshipData Empty = new();
	protected readonly static string ItemCodePrefix = "Ship_";

	[Export]
	public string CustomName { get; set; } = string.Empty;

	[Export]
	public int CurrentHP { get; set; }

	public int FullHP => fullHP.Value;

	public int Size => size.Value;

	public int Raid => raid.Value;

	public SpaceshipData(string typeName, long? uniqueID = null) : base(ItemCodePrefix + typeName, uniqueID) {
		size = new(() => {
			if (TryQuery<int>("Size", out var result)) {
				return result;
			}
			return 0;
		});

		fullHP = new(() => {
			if (TryQuery<int>("HP", out var result)) {
				return result;
			}
			return 0;
		});

		raid = new(() => {
			if (TryQuery<int>("Raid", out var result)) {
				return result;
			}
			return 0;
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
	}

	private Lazy<int> fullHP;
	private Lazy<int> size;
	private Lazy<int> raid;
}
