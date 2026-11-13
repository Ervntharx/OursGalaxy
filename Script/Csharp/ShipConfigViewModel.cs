using System;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy.UI;

internal sealed class ShipConfigViewModel {
	public ShipConfigPanel Panel { get; set; }
	public SubRepository? Repository { get; private set; }
	public SpaceshipReference? Ship { get; private set; }

	public string? PendingEditOutletName { get; private set; }

	public event Action<OutletSizeType>? PushPendingEditOutletEvent;
	public event Action? PopPendingEditOutletEvent;

	public event Action<string, EquipReference?>? OutletUPdatedEvent;

	public ShipConfigViewModel(ShipConfigPanel panel) => Panel = panel;

	public void Load(SubRepository repository, SpaceshipReference shipRefer) {
		Repository = repository;
		Ship = shipRefer;
	}

	public void PushOutletIntoPending(string outletName) {
		if (Repository is not null && Ship is not null) {
			PendingEditOutletName = outletName;
			var shipData = Repository.GetSpaceshipData(Ship);
			var shipConfig = SpaceshipLib.GetConfig(shipData);
			PushPendingEditOutletEvent?.Invoke(shipConfig.OutletSet[outletName]);
		}
		else {
			GD.PushError("<ShipConfig> null Repository and ship reference");
		}
	}

	public void CLearOutlet(string outletName) {
		if (Repository is not null && Ship is not null) {
			PendingEditOutletName = outletName;
			var data = Repository.GetSpaceshipData(Ship);
			if (data.EquipsLs.ContainsKey(outletName)) {
				// 先清理引用,后移出资产到主资产库
				var equipReference = data.EquipsLs[outletName];
				var equipData = Repository.GetEquipData(equipReference);
				equipData.RecycleReference(equipReference);
				data.EquipsLs.Remove(outletName);
				RepositoryMap.Move(equipData, GlobalViewModel.MainRepository.ID);

				OutletUPdatedEvent?.Invoke(PendingEditOutletName, null);
			}
			PendingEditOutletName = null;
		}
		else {
			GD.PushError("<ShipConfig> null Repository and ship reference");
		}
	}

	public void Install(EquipData equipData) {
		if (Repository is not null && Ship is not null && PendingEditOutletName is not null) {
			if (equipData.ItemLocation is null) {
				var data = Repository.GetSpaceshipData(Ship);
				RepositoryMap.Move(equipData, Repository.ID);
				var reference = equipData.MakeReference();
				data.EquipsLs.Add(PendingEditOutletName, reference);

				OutletUPdatedEvent?.Invoke(PendingEditOutletName, reference);
			}
			else {
				GD.PushError($"<ShipConfig> equip is occupied: {equipData.ID}");
			}
		}
		else {
			GD.PushError("<ShipConfig> null Repository and ship reference");
		}
	}

	public void DisplayEquipInf(EquipData data) {
		GD.Print($"<Ship Config> Display Equip information:{data.ID}");
	}
}
