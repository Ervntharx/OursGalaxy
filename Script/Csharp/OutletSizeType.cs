using Godot;
namespace OursGalaxy;

/// <summary>
/// 插槽类型尺寸信息
/// </summary>
[GlobalClass]
internal partial class OutletSizeType : Resource {
	public readonly static OutletSizeType S = new("S");
	public readonly static OutletSizeType M = new("M");
	public readonly static OutletSizeType L = new("L");
	public readonly static OutletSizeType XL = new("XL");
	public readonly static OutletSizeType Air = new("Air");

	protected OutletSizeType() { type = Air.type; }
	public OutletSizeType(string specialOutletTypeName) { type = specialOutletTypeName; }

	public static bool operator ==(OutletSizeType left, OutletSizeType right) => !(left != right);
	public static bool operator !=(OutletSizeType left, OutletSizeType right) => left.type != right.type;

	public override bool Equals(object? obj) {
		if (ReferenceEquals(this, obj)) {
			return true;
		}
		return obj is not null && obj is OutletSizeType outletObj && outletObj == this;
	}

	public override int GetHashCode() => type.GetHashCode();
	public override string ToString() => type;

	private string type = "S";
}
