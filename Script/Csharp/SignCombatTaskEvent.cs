using System;
using System.Linq;
using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class SignCombatTaskEvent : TaskEvent {
	public override string? ReactButtonName => $"{(HasSign ? "已" : "")}标记";
	public override string? ReactUrl => "Sign";

	[Export]
	public CombatData Combat { get; set; }

	[Export]
	public bool HasSign { get; set; }

	public SignCombatTaskEvent(string code) : base(code) {
		//	TODO:	生成Combat
		Combat = CombatData.Template;
	}

	protected SignCombatTaskEvent() : base() { Combat = CombatData.Empty; }

	public override void OnMatchUrl(Variant variant) {
		if (!HasSign) {
			Archive.Data.PunitiveCombatList.Add(Combat);
			HasSign = true;
			TaskEventStateChanged?.Invoke(this);
		}
	}
}
