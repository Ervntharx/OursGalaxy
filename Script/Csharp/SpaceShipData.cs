using Godot;
/// <summary>
/// 飞船数据类, 用于存储飞船的基本信息和状态
/// </summary>
[GlobalClass]
internal partial class SpaceshipData : Resource {
	[Export]
	public string TypeName { get; set; } = string.Empty;

	[Export]
	public int UniqueID { get; set; }

	[Export]
	public string CustomName { get; set; } = string.Empty;

	[Export]
	public int CurrentHealth { get; set; }
}
