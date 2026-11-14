using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Godot;
namespace OursGalaxy;

/// <summary>
/// 游戏逻辑资产库.只管理Unique的SpaceshipData和EquipsData两类型.
/// </summary>
[GlobalClass]
internal sealed partial class SubRepository : Resource {
	[Export] public GUID ID { get; private set; }

	/// <summary>
	/// 只读,且需要赋值一次
	/// </summary>
	public IReadOnlyCollection<SpaceshipData> Ships {
		get => ships ?? throw new InvalidOperationException("<SubRepository> Ships isnt reaay");
		set {
			ships = new(value);
			ships.CollectionChanged += (obj, args) => ElementsChangedEvent?.Invoke(obj, args);
		}
	}

	/// <summary>
	/// 只读,且需要赋值一次
	/// </summary>
	public IReadOnlyCollection<EquipData> Equips {
		get => equips ?? throw new InvalidOperationException("<SubRepository> Equips isnt reaay");
		set {
			equips = new(value);
			equips.CollectionChanged += (obj, args) => ElementsChangedEvent?.Invoke(obj, args);
		}
	}

	public event System.Collections.Specialized.NotifyCollectionChangedEventHandler? ElementsChangedEvent;

	public SubRepository() {
		ID = GUID.Generate();
	}

	public SpaceshipData this[SpaceshipReference reference] => Ships?.FirstOrDefault(ship => ship.ID == reference.IDReference)
		?? throw new NullReferenceException($"<SubRepository> Can't find spaceship refer: {reference.IDReference}");

	public EquipData this[EquipReference reference] => Equips?.FirstOrDefault(ship => ship.ID == reference.IDReference)
		?? throw new NullReferenceException($"<SubRepository> Can't find equip refer: {reference.IDReference}");

	public void Add(SpaceshipData data) {
		ThrowIfOccupied(data);
		if (ships is null) {
			throw new InvalidOperationException("<SubRepository> Ships isnt reaay");
		}
		ships.Add(data);
	}

	public void Add(EquipData data) {
		ThrowIfOccupied(data);
		if (equips is null) {
			throw new InvalidOperationException("<SubRepository> Equips isnt reaay");
		}
		equips.Add(data);
	}

	public void Move(SpaceshipData data, SubRepository to) {
		ThrowIfOccupied(data);
		Remove(data);
		to.Add(data);
	}

	public void Move(EquipData data, SubRepository to) {
		ThrowIfOccupied(data);
		Remove(data);
		to.Add(data);
	}

	public void Remove(SpaceshipData data) {
		ThrowIfOccupied(data);
		if (ships is null) {
			throw new InvalidOperationException("<SubRepository> Ships isnt reaay");
		}
		if (!ships.Remove(data)) {
			throw new InvalidOperationException("<SubRepository> unqiueitem isnt in repository");
		}
	}

	public void Remove(EquipData data) {
		ThrowIfOccupied(data);
		if (equips is null) {
			throw new InvalidOperationException("<SubRepository> Equips isnt reaay");
		}
		if (!equips.Remove(data)) {
			throw new InvalidOperationException("<SubRepository> unqiueitem isnt in repository");
		}
	}

	private void ThrowIfOccupied(UniqueItem item) {
		if (item.IsOccupied()) throw new ArgumentException("<SubRepository> UniqueItem is occupied");
	}

	private ObservableCollection<SpaceshipData>? ships;
	private ObservableCollection<EquipData>? equips;

	/// <summary>
	/// 仅用于Godot Resource使用的序列化接口.在Godot Array和ObservaleCollection之间转换
	/// </summary>
	[Export]
	private Godot.Collections.Array<SpaceshipData> shipSource {
		get {
			var result = new Godot.Collections.Array<SpaceshipData>();
			foreach (var ship in ships ?? []) {
				result.Add(ship);
			}
			return result;
		}
		set {
			ships = [];
			foreach (var ship in value) {
				ships.Add(ship);
			}
		}
	}

	/// <summary>
	/// 仅用于Godot Resource使用的序列化接口.在Godot Array和ObservaleCollection之间转换
	/// </summary>
	[Export]
	private Godot.Collections.Array<EquipData> equipsSource {
		get {
			var result = new Godot.Collections.Array<EquipData>();
			foreach (var equip in equips ?? []) {
				result.Add(equip);
			}
			return result;
		}
		set {
			equips = [];
			foreach (var equip in value) {
				equips.Add(equip);
			}
		}
	}
}
