using System;
using Godot;
namespace OursGalaxy;

/// <summary>
/// System.Guid的封装。支持Godot Resource进行序列化
/// </summary>
[GlobalClass]
internal partial class GUID : Resource {
	public readonly static GUID Empty = new(Guid.Empty);

	/// <summary>
	/// ⚠️使用Generate
	/// </summary>
	protected GUID() { }
	public GUID(Guid guid) { raw = guid; }
	public static GUID Generate() => new() { raw = Guid.NewGuid() };

	public static bool operator !=(GUID left, GUID right) => left.raw != right.raw;
	public static bool operator ==(GUID left, GUID right) => !(left != right);

	public override bool Equals(object? obj) => obj is not null && obj is GUID guid && this == guid;
	public override int GetHashCode() => raw.GetHashCode();

	public override string ToString() => raw.ToString();

	private Guid raw;
}
