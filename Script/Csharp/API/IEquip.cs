using System.Collections.Generic;

namespace OursGalaxy;

public interface IEquip {
	public enum OutletTypeEnum { S, M, L, XL, AirDefense, Special }
	public OutletTypeEnum SizeType { get; }
	public string? SpecialTypeName { get; }

	public long CDMisc { get; }
	public IReadOnlyCollection<IPromote>? Promotes { get; }
}
