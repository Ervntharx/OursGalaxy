using System;
using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class FightBackTaskEvent : TaskEvent {
	public override string? ReactButtonName => "击退";

	public override string? ReactUrl => $"FightBack";
	// TODO:	击退类型？无需创建击退类型，是否意味着PunitiveCombat同样无需特例化
	[Export]
	public CombatData Combat { get; set; }

	[Export]
	public bool HasFightBack { get; set; }


	public FightBackTaskEvent(string code) {
		Time = DateTime.Now();
		Code = code;
		Combat = CombatData.Template;
	}

	protected FightBackTaskEvent() {
		Time = DateTime.Now();
		Code = string.Empty;
		Combat = CombatData.Empty;
	}

	public override void OnMatchUrl(Variant variant) {
		// TODO:	直接调用CombatStage
	}
}
