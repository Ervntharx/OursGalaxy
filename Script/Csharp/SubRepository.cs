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

	/// <summary>
	/// 非占用物的移动
	/// </summary>
	/// <param name="data"></param>
	/// <param name="to"></param>
	public void Move(SpaceshipData data, SubRepository to) {
		ThrowIfOccupied(data);
		Remove(data);
		to.Add(data);
	}

	/// <summary>
	/// 针对非占用物的移动
	/// </summary>
	/// <param name="data"></param>
	/// <param name="to"></param>
	public void Move(EquipData data, SubRepository to) {
		ThrowIfOccupied(data);
		Remove(data);
		to.Add(data);
	}

	/// <summary>
	/// 对占用物的移动,shipRefer会失效. 因为Spaceship包含EquipReference,这里唯一包含少量Occuyied的EquipData的危险移动
	/// 如果from中不包含任何Equip会抛出异常,不会发生移动.
	/// </summary>
	/// <param name="shipRefer"></param>
	/// <param name="from"></param>
	/// <param name="to"></param>
	public void Move(SpaceshipReference shipRefer, SubRepository from, SubRepository to) {
		// 统计所有需要移动的equip. 如果任何reference不在from的Repository内则在该阶段报错并退出,则不会发生移动行为
		var moveEquipLs = new List<EquipData>();
		foreach (var equipRefer in from[shipRefer].EquipsLs.Values) {
			moveEquipLs.Add(from[equipRefer]);
		}

		// 直接带Reference的EquipData移动
		foreach (var moveEquip in moveEquipLs) {
			from.Remove(moveEquip);
			to.Add(moveEquip);
		}
		// 安全移动ship
		var shipData = from[shipRefer];
		shipData.RecycleReference(shipRefer);
		from.Remove(shipData);
		to.Add(shipData);
	}

	/// <summary>
	/// 对占用物的移动,equipRefer会失效
	/// </summary>
	/// <param name="equipRefer"></param>
	/// <param name="from"></param>
	/// <param name="to"></param>
	public void Move(EquipReference equipRefer, SubRepository from, SubRepository to) {
		var equipData = from[equipRefer];
		equipData.RecycleReference(equipRefer);
		from.Remove(equipData);
		to.Add(equipData);
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
