using System;
using Godot;
namespace OursGalaxy.ResourceConfig;

[GlobalClass]
internal partial class TurretData : EquipData {
	/// <summary>
	/// 构造.⚠️turretTypeCode是完整TypeCode.例如Equip/Turret/Photon
	/// </summary>
	/// <param name="turretTypeCode">⚠️使用完整TypeCode</param>
	/// <param name="turretNameCode"></param>
	/// <param name="qulity"></param>
	public TurretData(string turretTypeCode, string turretNameCode, ItemBaseConfig.QulityEnum qulity) : base(turretTypeCode, turretNameCode, qulity) { }
	protected TurretData() : base() { }
	public string GetTurretType() => TypeCode;
	public string GetTurretName() => NameCode;
}
