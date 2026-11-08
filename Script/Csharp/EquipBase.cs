using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 装备类基类
/// </summary>
internal abstract partial class EquipBase : Node3D {
	// ItemBase属性
	[Export] public string TypeCode { get; set; } = string.Empty;
	[Export] public string NameCode { get; set; } = string.Empty;
	[Export] public ItemBaseConfig.QulityEnum Qulity { get; set; }

	/// <summary>
	/// 自身的ID
	/// </summary>
	[Export] public GUID ID { get; set; } = GUID.Empty;

	/// <summary>
	/// 安装的飞船的ID
	/// </summary>
	[Export] public GUID From { get; set; } = GUID.Empty;

	/// <summary>
	/// 插座尺寸数据
	/// </summary>
	[Export] public OutletSizeType SizeType { get; set; } = OutletSizeType.Air;

	protected EquipBase() { }
}
