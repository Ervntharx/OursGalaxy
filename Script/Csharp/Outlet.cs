using System.ComponentModel.DataAnnotations;
using Godot;
namespace OursGalaxy;

/// <summary>
/// 实体插槽类型
/// </summary>
[GlobalClass]
internal partial class Outlet : Marker3D {
	[Export] public string UniqueName { get; set; } = string.Empty;

	[Export] public OutletSizeType SizeType { get; set; } = OutletSizeType.Air;

	/// <summary>
	/// 左转限制角度
	/// </summary>
	[Export] public float LeftYawClampAngle { get; set; } = 180;

	/// <summary>
	/// 右转限制角度
	/// </summary>
	[Export] public float RightYawClampAngle { get; set; } = 180;

	/// <summary>
	/// 抬头限制角度
	/// </summary>
	[Export] public float UpPitchClampAngle { get; set; } = 45;

	/// <summary>
	/// 低头限制角度
	/// </summary>
	[Export] public float DownPitchClampAngle { get; set; } = 45;

	public void Install(EquipBase equip) {
		AddChild(equip, true);
	}

	public EquipBase? GetEquipOrNull() => GetChildOrNull<EquipBase?>(0);
}
