using System.Linq;
namespace OursGalaxy;

/// <summary>
/// 飞船的引用
/// </summary>
internal partial class SpaceshipReference : UniqueItemReference {
	public SpaceshipReference(SpaceshipData data) : base(data) { }
}
