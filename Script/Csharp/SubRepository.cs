using System;
using System.Collections.Generic;
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
	/// 只读
	/// </summary>
	public IReadOnlyCollection<SpaceshipData> Ships => shipSource.AsReadOnly();

	/// <summary>
	/// 只读
	/// </summary>
	public IReadOnlyCollection<EquipData> Equips => equipsSource.AsReadOnly();

	public event Action<SubRepository, UniqueItem?>? ElementsChangedEvent;

	public SubRepository() { ID = GUID.Generate(); }

	public SpaceshipData this[SpaceshipReference reference] => Ships?.FirstOrDefault(ship => ship.ID == reference.IDReference)
		?? throw new NullReferenceException($"<SubRepository> Can't find spaceship refer: {reference.IDReference}");

	public EquipData this[EquipReference reference] => Equips?.FirstOrDefault(ship => ship.ID == reference.IDReference)
		?? throw new NullReferenceException($"<SubRepository> Can't find equip refer: {reference.IDReference}");

	public void Add(SpaceshipData data) {
		ThrowIfOccupied(data);
		shipSource.Add(data);
		ElementsChangedEvent?.Invoke(this, data);
	}

	public void Add(EquipData data) {
		ThrowIfOccupied(data);
		equipsSource.Add(data);
		ElementsChangedEvent?.Invoke(this, data);
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
		if (!shipSource.Remove(data)) {
			throw new InvalidOperationException("<SubRepository> unqiueitem isnt in repository");
		}
		ElementsChangedEvent?.Invoke(this, data);
	}

	public void Remove(EquipData data) {
		ThrowIfOccupied(data);
		if (!equipsSource.Remove(data)) {
			throw new InvalidOperationException("<SubRepository> unqiueitem isnt in repository");
		}
		ElementsChangedEvent?.Invoke(this, data);
	}

	private void ThrowIfOccupied(UniqueItem item) {
		if (item.IsOccupied()) throw new ArgumentException("<SubRepository> UniqueItem is occupied");
	}

	[Export] private Godot.Collections.Array<SpaceshipData> shipSource { get; set; } = [];

	[Export] private Godot.Collections.Array<EquipData> equipsSource { get; set; } = [];
}
