using System;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 唯一性游戏物品，具备唯一id识别能力
/// </summary>
[GlobalClass]
internal partial class UniqueItem : ItemBase {
	/// <summary>
	/// 唯一id
	/// </summary>
	[Export]
	public GUID ID { get; protected set; }

	/// <summary>
	/// 物品位置占位.如果被reference则此不为null
	/// </summary>
	[Export]
	public ItemLocationInf? ItemLocation { get; set; }

	/// <summary>
	/// ⚠️予Godot设施提供的构造函数。继承类型中应包含，但不应主动使用以产生空值
	/// </summary>
	protected UniqueItem() : base() { ID = GUID.Empty; }

	public UniqueItem(string typeCode, string nameCode, ItemBaseConfig.QulityEnum qulity, GUID? id = null, ItemLocationInf? loc = null) : base(typeCode, nameCode, qulity) {
		ID = id ?? GUID.Generate();
		ItemLocation = loc;
	}

	/// <summary>
	/// 构造引用。即使不指定location,ItemLocation也具有默认值。
	/// </summary>
	/// <param name="location"></param>
	/// <returns></returns>
	public virtual UniqueItemReference MakeReference() {
		ItemLocation = new();
		return new(this);
	}

	/// <summary>
	/// 被占用状态
	/// </summary>
	/// <returns></returns>
	public virtual bool IsOccupied() => ItemLocation is not null;

	/// <summary>
	/// 尝试清理引用。依靠派生类按照UniqueItem的类型进行定向检查占用位置。默认没有效果且不会成功
	/// </summary>
	/// <returns>是否清理成功</returns>
	public virtual bool TryCleanOccupied() { return false; }

	public static bool operator ==(UniqueItem left, UniqueItem right) => !(left != right);
	public static bool operator !=(UniqueItem left, UniqueItem right)
		=> left.TypeCode != right.TypeCode || left.NameCode != right.NameCode || left.Qulity != right.Qulity || left.ID != right.ID;

	public override bool Equals(object? obj) {
		if (ReferenceEquals(this, obj)) {
			return true;
		}
		return obj is not null && obj is UniqueItem uniqueObj && uniqueObj == this;
	}

	public override int GetHashCode() => base.GetHashCode() + ID.GetHashCode();
}
