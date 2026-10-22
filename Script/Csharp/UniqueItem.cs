using Godot;
using OursGalaxy;

/// <summary>
/// 唯一性游戏物品，具备唯一id识别能力
/// </summary>
[GlobalClass]
internal partial class UniqueItem : ItemBase {
	public new static readonly UniqueItem Empty = new();
	[Export]
	public long UniqueID { get; protected set; }

	/// <summary>
	/// ⚠️予Godot设施提供的构造函数。继承类型中应包含，但不应主动使用以产生空值
	/// </summary>
	protected UniqueItem() : base() { }

	public UniqueItem(string code, long? uniqueID = null) : base(code) {
		UniqueID = uniqueID ?? GenerateID();
	}

	private static long GenerateID() {
		return Time.GetUnixTimeFromDatetimeDict(Time.GetDateDictFromSystem(true));
	}

	public static bool operator ==(UniqueItem left, UniqueItem right) {
		return !(left != right);
	}

	public static bool operator !=(UniqueItem left, UniqueItem right) {
		return left.UniqueID != right.UniqueID;
	}

	public override bool Equals(object? obj) {
		if (ReferenceEquals(this, obj)) {
			return true;
		}
		return obj is not null && obj is UniqueItem uniqueObj && uniqueObj == this;
	}

	public override int GetHashCode() {
		return UniqueID.GetHashCode() + base.GetHashCode() + 17;
	}
}
