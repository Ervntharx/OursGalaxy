using System.Linq;
using Godot;
namespace OursGalaxy;

/// <summary>
/// 飞船的引用
/// </summary>
[GlobalClass]
internal partial class SpaceshipReference : UniqueItemReference {
	protected SpaceshipReference() : base() { }
	public SpaceshipReference(SpaceshipData data) : base(data) { }
}
