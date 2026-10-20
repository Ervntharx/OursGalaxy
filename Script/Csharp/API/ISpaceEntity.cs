using Godot;
/// <summary>
/// 空间实体接口
/// </summary>
public interface ISpaceEntity {
	public T ToNode<T>() where T : Node;
	public Node3D ToNode3D();
}
