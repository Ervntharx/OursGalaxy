using System;
using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class TurretData : EquipData {
	public string GetTurretType() => ITypeCode;
	public string GetTurretName() => NameCode;
}
