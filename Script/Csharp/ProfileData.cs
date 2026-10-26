using Godot;

/// <summary>
/// 个人小卡数据
/// </summary>
[GlobalClass]
internal partial class ProfileData : Resource {
	public readonly static ProfileData Empty = new();
	/// <summary>
	/// 所属组织归属
	/// </summary>
	[Export]
	public string? Alliance { get; set; }

	/// <summary>
	/// 头像
	/// </summary>
	[Export]
	public Texture2D? Avatar { get; set; }

	/// <summary>
	/// 名字
	/// </summary>
	[Export]
	public string Name { get; set; }

	/// <summary>
	/// 点数
	/// </summary>
	[Export]
	public int Points { get; set; }

	/// <summary>
	/// 等级
	/// </summary>
	[Export]
	public int Level { get; set; }

	/// <summary>
	/// ⚠️避免主动调用无参构造函数
	/// </summary>
	protected ProfileData() { Name = string.Empty; }

	public ProfileData(string name) {
		Name = name;
	}
}
