using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class RepairTaskEvent : TaskEventData {
	public RepairTaskEvent(string code) : base(code) { }

	protected RepairTaskEvent() : base() { }
}
