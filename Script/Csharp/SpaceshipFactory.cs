using System;
using System.Linq;
using Godot;
using OursGalaxy.ResourceConfig;

namespace OursGalaxy;

internal static class SpaceshipFactory {
	public static Spaceship Make(SpaceshipData data) {
		var config = SpaceshipLib.GetConfig(data);
		var result = (Spaceship)ResourceLoader.Load<PackedScene>(config.InstanceResourcePath).Instantiate();
		result.ShipTypeName = config.ShipTypeName;
		result.HP = config.HP;
		result.ScaleSize = config.ScaleSize;
		result.PreventValue = config.PreventValue;
		result.ImpactTolerance = config.ImpactTolerance;
		result.PenetrateTolerance = config.PenetrateTolerance;
		result.PhotothermalTolerance = config.PhotothermalTolerance;
		result.ElectromagneticTolerance = config.ElectromagneticTolerance;
		result.RotateCapability = config.RotateCapability;
		result.MicroPowrCapability = config.MicroPowerCapability;
		result.PowerCapability = config.PowerCapability;
		result.BackPowerCapability = config.BackPowerCapability;

		// 安装装配配置
		foreach (var pair in data.EquipsLs) {
			var equipData = pair.Value;
			if (equipData is not null) {
				var equip = EquipFactory.Make(equipData);
				equip.From = result.ID;
				result.Outlets.FirstOrDefault(outlet => outlet.Name == pair.Key)?.Install(equip);
			}
		}

		result.ID = data.ID;
		result.CurrentHP = data.CurrentHP <= 0 ? result.HP : data.CurrentHP;
		result.CustomName = data.CustomName;
		return result;
	}


}
