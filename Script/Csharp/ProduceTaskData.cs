using Godot;
using Godot.Collections;

[GlobalClass]
internal partial class ProduceTaskData : Resource {
	[Export]
	public Dictionary StartDate;

	[Export]
	public ItemBase Target;

	protected ProduceTaskData() {
		Target = ItemBase.Empty;
		StartDate = [];
	}
}
