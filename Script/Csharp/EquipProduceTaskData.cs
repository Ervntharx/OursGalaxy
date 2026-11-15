using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class EquipProduceTaskData : ProduceTaskData {
	[Export] public EquipData? Target { get; set; }
}
