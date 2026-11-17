using System;
using Godot;
namespace OursGalaxy.ViewModel;

[GlobalClass]
internal partial class SpaceshipEditorVM : Node {
	public event Action<SubRepository?>? LocalRepositoryChangedEvent;
	public event Action<SpaceshipData?>? EditedShipChangedEvent;
	public event Action<EquipData?>? SelectedEquipChangedEvent;
	public event Action<string?>? SelectedOutletChangedEvent;

	public SubRepository? LocalRepository {
		get => localRepository;
		set {
			localRepository = value;
			LocalRepositoryChangedEvent?.Invoke(localRepository);
		}
	}
	public SpaceshipData? EditedShip {
		get => editedShip; set {
			editedShip = value;
			EditedShipChangedEvent?.Invoke(EditedShip);
		}
	}

	public EquipData? SelectedEquip {
		get => selectedEquip;
		set {
			selectedEquip = value;
			SelectedEquipChangedEvent?.Invoke(SelectedEquip);
		}
	}

	/// <summary>
	/// 用于选中没有安装装备的空槽
	/// </summary>
	public string? SelectedOutlet {
		get => selectedOutlet;
		set {
			selectedOutlet = value;
			SelectedOutletChangedEvent?.Invoke(SelectedOutlet);
		}
	}

	public void InstallEquip(EquipData equip) {
		// TODO: 安装装备
	}

	public void UninstallEquip(EquipData equip) {
		// TODO: 卸载装备
	}

	private SubRepository? localRepository;
	private SpaceshipData? editedShip;
	private EquipData? selectedEquip;
	private string? selectedOutlet;
}
