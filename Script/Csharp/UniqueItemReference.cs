using System;
using System.Runtime.CompilerServices;
using Godot;
namespace OursGalaxy;

/// <summary>
/// 强引用。
/// 因为UniqueItem基本都仍然存储于固定的List列表，对于其他对象逻辑持有的情况，使用Reference标记引用状态。并在UniqueItem方面Location不为null，为被占用状态。
/// </summary>
[GlobalClass]
internal partial class UniqueItemReference : Resource {
	[Export] public GUID IDReference { get; set; }

	public UniqueItemReference(UniqueItem uItem) {
		IDReference = uItem.ID;
	}

	public UniqueItemReference() {
		IDReference = GUID.Empty;
	}

	public static bool operator !=(UniqueItemReference left, UniqueItemReference right) => left.IDReference != right.IDReference;
	public static bool operator ==(UniqueItemReference left, UniqueItemReference right) => !(left != right);

	public override bool Equals(object? obj) {
		if (ReferenceEquals(obj, this)) {
			return true;
		}
		return obj is UniqueItemReference refer && refer == this;
	}

	public override int GetHashCode() {
		return HashCode.Combine(IDReference.GetHashCode(), "Reference");
	}
}
