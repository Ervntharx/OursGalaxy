using Godot;
namespace OursGalaxy;

/// <summary>
/// unique id方案。不包速度效率。是不可变类型
/// </summary>
[GlobalClass]
internal partial class GUID : Resource {
	public readonly static GUID Empty = new();

	/// <summary>
	/// ⚠️使用Generate
	/// </summary>
	protected GUID() { }

	public static GUID Generate() => new() { raw = Time.GetUnixTimeFromSystem(), randCode = GD.Randi() };

	public static bool operator !=(GUID left, GUID right) => left.raw != right.raw || left.randCode != right.randCode;
	public static bool operator ==(GUID left, GUID right) => !(left != right);

	public override bool Equals(object? obj) {
		if (ReferenceEquals(this, obj)) {
			return true;
		}
		return obj is not null && obj is GUID gUID && gUID == this;
	}

	public override int GetHashCode() => raw.GetHashCode() + randCode.GetHashCode() + 3;

	public override string ToString() => $"{raw.ToString()}_{randCode.ToString()}";

	private double raw;
	private long randCode;
}
