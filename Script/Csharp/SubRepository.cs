using System;
using System.Linq;
using Godot;
using Godot.Collections;
namespace OursGalaxy;

[GlobalClass]
internal partial class SubRepository : Resource {
	[Export] public GUID ID { get; set; } = GUID.Generate();
	[Export] public Array<SpaceshipData> Ships { get; protected set; } = [];
	[Export] public Array<EquipData> Equips { get; protected set; } = [];

	public UniqueItem GetUniqueItem(GUID uID) {
		UniqueItem? result = Ships.FirstOrDefault(ship => ship.ID == uID);
		result ??= Equips.FirstOrDefault(equip => equip.ID == uID);
		return result!;
	}

	public SpaceshipData GetSpaceshipData(SpaceshipReference refer) {
		var result = Ships.FirstOrDefault(ship => ship.ID == refer.IDReference);
		if (result is null) {
			throw new NullReferenceException($"<Repository> Can't find spaceship refer: {refer.IDReference}");
		}
		return result;
	}

	public EquipData GetEquipData(EquipReference refer) {
		var result = Equips.FirstOrDefault(ship => ship.ID == refer.IDReference);
		if (result is null) {
			throw new NullReferenceException($"<Repository> Can't find equip refer: {refer.IDReference}");
		}
		return result;
	}
}
