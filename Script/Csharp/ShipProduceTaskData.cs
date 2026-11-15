using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class ShipProduceTaskData : ProduceTaskData {
	[Export] public SpaceshipData? Target { get; set; }
}
