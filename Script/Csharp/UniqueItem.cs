using System;
using System.Security.AccessControl;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 唯一性游戏物品，具备唯一id识别能力
/// </summary>
[GlobalClass]
internal partial class UniqueItem : ItemBase {
	[Export]
	public GUID ID { get; protected set; }

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
	public UniqueItemReference MakeReference(ItemLocationInf? location = null) {
		ItemLocation = location ?? new ItemLocationInf();
		return new(this);
	}

	public static bool operator ==(UniqueItem left, UniqueItem right) => !(left != right);
	public static bool operator !=(UniqueItem left, UniqueItem right)
		=> left.ITypeCode != right.ITypeCode || left.NameCode != right.NameCode || left.Qulity != right.Qulity || left.ID != right.ID;

	public override bool Equals(object? obj) {
		if (ReferenceEquals(this, obj)) {
			return true;
		}
		return obj is not null && obj is UniqueItem uniqueObj && uniqueObj == this;
	}

	public override int GetHashCode() => base.GetHashCode() + ID.GetHashCode();
}
