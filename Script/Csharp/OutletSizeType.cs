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

	/// <summary>
	/// 该尺寸的装备能否装入指定尺寸的插槽. 尺寸顺序: Air &lt; S &lt; M &lt; L &lt; XL.
	/// Air是防空装备尺寸/防空槽, 是最狭窄的尺寸.
	/// </summary>
	public bool CanFitTo(OutletSizeType outlet) {
		// 任何特殊槽都按自定义名称严格匹配
		if (IsSpecialLevel(Type) || IsSpecialLevel(outlet.Type)) {
			return Type == outlet.Type;
		}
		return GetLevel(Type) <= GetLevel(outlet.Type);
	}

	// 非默认槽的特殊尺寸槽,是自定义名称
	private bool IsSpecialLevel(string type) => GetLevel(type) == 0;

	private static int GetLevel(string type) => type switch {
		"Air" => 1,
		"S" => 2,
		"M" => 3,
		"L" => 4,
		"XL" => 5,
		_ => 0,
	};

	public override bool Equals(object? obj) {
		if (ReferenceEquals(this, obj)) {
			return true;
		}
		return obj is not null && obj is OutletSizeType outletObj && outletObj == this;
	}

	public override int GetHashCode() => Type.GetHashCode();
	public override string ToString() => Type;
}
