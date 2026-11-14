using System;
using System.Linq;
using Godot;
using OursGalaxy.ResourceConfig;

namespace OursGalaxy;

internal static class SpaceshipFactory {
	/// <summary>
	/// 构造Spaceship.但缺乏Repository无法构造Equips
	/// </summary>
	/// <param name="data"></param>
	/// <returns></returns>
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

		result.ID = data.ID;
		result.CurrentHP = data.CurrentHP <= 0 ? result.HP : data.CurrentHP;
		result.CustomName = data.CustomName;
		return result;
	}

	/// <summary>
	/// 完整构造
	/// </summary>
	/// <param name="data"></param>
	/// <param name="repository"></param>
	/// <returns></returns>
	public static Spaceship Make(SpaceshipData data, SubRepository? repository = null) {
		var result = Make(data);
		// 安装装配配置
		foreach (var pair in data.EquipsLs) {
			var equipData = pair.Value;
			if (equipData is not null) {
				var equip = EquipFactory.Make(repository[pair.Value]);
				equip.From = result.ID;
				result.Outlets.FirstOrDefault(outlet => outlet.Name == pair.Key)?.Install(equip);
			}
		}
		return result;
	}

	public static Spaceship Make(SpaceshipReference refer, SubRepository repository) {
		return Make(repository[refer], repository);
	}
}
