using Godot;
namespace OursGalaxy;

/// <summary>
/// 空间实体接口
/// </summary>
internal interface ISpaceEntity {
	public T ToNode<T>() where T : Node;
	public Node3D AsNode3D();
}
