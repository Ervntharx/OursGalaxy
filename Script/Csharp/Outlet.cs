using Godot;

namespace OursGalaxy;

/// <summary>
/// 插槽数据类型
/// </summary>
[GlobalClass]
public partial class Outlet : Resource {
	[Export]
	public OutletSizeType SizeType { get; set; } = new(OutletSizeType.SizeTypeEnum.S);

	[Export]
	public NodePath? OutletMarker { get; set; }

	[Export]
	public float LimitYawLeft { get; set; }

	[Export]
	public float LimitYawRight { get; set; }
}
