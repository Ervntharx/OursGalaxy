using Godot;
namespace OursGalaxy;

[GlobalClass]
public partial class OutletSizeType : Resource {
	public enum SizeTypeEnum { S, M, L, XL, DefenseAir, Special }
	[Export]
	public SizeTypeEnum SizeType { get; set; }
	[Export]
	public string? SpecialSizeTypeName { get; set; }


	public OutletSizeType(SizeTypeEnum sizeType, string? specialSTName = null) {
		SizeType = sizeType;
		SpecialSizeTypeName = specialSTName;
	}
	public static bool operator !=(OutletSizeType left, OutletSizeType right) {
		if (left.SizeType == SizeTypeEnum.Special && right.SizeType == SizeTypeEnum.Special) {
			return left.SpecialSizeTypeName != right.SpecialSizeTypeName;
		}
		return left.SizeType != right.SizeType;
	}

	public static bool operator ==(OutletSizeType left, OutletSizeType right) {
		return !(left != right);
	}

	public override bool Equals(object? obj) {
		if (ReferenceEquals(this, obj)) {
			return true;
		}
		return obj is not null && obj is OutletSizeType outletObj && outletObj == this;
	}

	public override int GetHashCode() {
		return SizeType.GetHashCode() + SpecialSizeTypeName?.GetHashCode() ?? 0;
	}

	public override string ToString() {
		return SizeType == SizeTypeEnum.Special ? SpecialSizeTypeName! : SizeType.ToString();
	}
}
