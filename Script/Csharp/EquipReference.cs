using System.Linq;
using Godot;
namespace OursGalaxy;

/// <summary>
/// 装备的引用
/// </summary>
[GlobalClass]
internal partial class EquipReference : UniqueItemReference {
	public EquipReference(EquipData data) : base(data) { }
}
