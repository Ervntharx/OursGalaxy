using System;
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

	[Export(PropertyHint.EnumSuggestion, "S,M,L,XL,Air")]
	public string Type { get; set; } = "Air";

	public OutletSizeType() { }
	public OutletSizeType(string sizeType) { Type = sizeType; }

	public static bool operator ==(OutletSizeType left, OutletSizeType right) => !(left != right);
	public static bool operator !=(OutletSizeType left, OutletSizeType right) => left.Type != right.Type;

	public override bool Equals(object? obj) {
		if (ReferenceEquals(this, obj)) {
			return true;
		}
		return obj is not null && obj is OutletSizeType outletObj && outletObj == this;
	}

	public override int GetHashCode() => Type.GetHashCode();
	public override string ToString() => Type;
}
