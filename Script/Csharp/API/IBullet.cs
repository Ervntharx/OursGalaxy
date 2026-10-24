using Godot;
/// <summary>
/// Represents a bullet in the game.
/// </summary>
public interface IBullet {
	public T ToNode<T>() where T : Node;
	public Area3D AsArea3D();

	public Vector3 Velocity { get; set; }
}
