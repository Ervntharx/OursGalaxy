using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class DamageControlConfig : EquipConfig {
	/// <summary>
	/// 作用范围
	/// </summary>
	[Export] public float Range { get; set; }

	/// <summary>
	/// 毫秒CD
	/// </summary>
	[Export] public long CDMisc { get; set; }

	/// <summary>
	/// 回复HP
	/// </summary>
	[Export] public int TreatHP { get; set; }

	/// <summary>
	/// 消除效果数量
	/// </summary>
	[Export] public int ReducePromoteCount { get; set; }
}
