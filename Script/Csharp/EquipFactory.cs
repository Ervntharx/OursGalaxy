using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

internal static class EquipFactory {
	public static EquipBase Make(EquipData data) {
		var config = ResourceLib.GetConfig<EquipConfig>(data);
		var equip = (EquipBase)ResourceLoader.Load<PackedScene>(config.InstanceResourcePath).Instantiate();
		equip.TypeCode = config.TypeCode;
		equip.NameCode = config.NameCode;
		equip.Qulity = config.Qulity;
		equip.SizeType = config.SizeType;

		equip.ID = data.ID;
		return equip;
	}
}
