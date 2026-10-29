using System;
using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class FightBackTaskEvent : TaskEvent {
	public override string? ReactButtonName => "击退";

	public override string? ReactUrl => $"FightBack";
	// TODO:	击退类型？无需创建击退类型，是否意味着PunitiveCombat同样无需特例化
	[Export]
	public PuntiveCombat Combat { get; set; }

	[Export]
	public bool HasFightBack { get; set; }


	public FightBackTaskEvent(string code) {
		Time = DateTime.Now();
		Code = code;
	}

	protected FightBackTaskEvent() {
		Time = DateTime.Now();
		Code = string.Empty;
		desc = new(string.Empty);
	}

	public void OnMatchUrl(Variant variant) {
		// TODO:	直接调用CombatStage
	}

	private Lazy<string> desc;
}
