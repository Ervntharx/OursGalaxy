using System;
using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class TurretData : EquipData {
	[Obsolete]
	[Export] public float Range { get; set; }

	public string GetTurretType() => ITypeCode;
	public string GetTurretName() => NameCode;
}
