using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class RepairTaskEvent : TaskEventData {
	public RepairTaskEvent(string code) : base(code) { }

	protected RepairTaskEvent() : base() { }

	public override void OnMatchUrl(Variant variant) {
		// TODO:	修理舰队
	}
}
