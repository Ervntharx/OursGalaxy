using System;
using System.ComponentModel.Design.Serialization;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 唯一性游戏物品，具备唯一id识别能力
/// </summary>
[GlobalClass]
internal partial class UniqueItem : ItemBase, IEquatable<UniqueItem> {
	/// <summary>
	/// 唯一id
	/// </summary>
	[Export]
	public GUID ID { get; protected set; }

	/// <summary>
	/// 物品位置占位.如果被reference则此不为null
	/// </summary>
	[Export]
	public ItemLocation? ItemLocation { get; set; }

	/// <summary>
	/// ⚠️予Godot设施提供的构造函数。继承类型中应包含，但不应主动使用以产生空值
	/// </summary>
	protected UniqueItem() : base() { ID = GUID.Empty; }

	public UniqueItem(string typeCode, string nameCode, ItemBaseConfig.QulityEnum qulity, GUID? id = null, ItemLocation? loc = null) : base(typeCode, nameCode, qulity) {
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
	/// 回收引用关系,refer会因此操作改变
	/// </summary>
	/// <param name="refer"></param>
	public virtual void RecycleReference(UniqueItemReference refer) {
		refer.IDReference = GUID.Empty;
		ItemLocation = null;
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

	public bool Equals(UniqueItem? other) => base.Equals(other) && other.ID == ID;
	public override bool Equals(object? obj) => Equals(obj as UniqueItem);

	public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), ID.GetHashCode());
}
