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
	/// 当前选中的插槽
	/// </summary>
	public string? SelectedOutlet {
		get => selectedOutlet;
		private set {
			selectedOutlet = value;
			SelectedOutletChangedEvent?.Invoke(SelectedOutlet);
		}
	}

	/// <summary>
	/// Change状态下等待被替换的装备. 非null表示安装新装备前需要先卸载它.
	/// </summary>
	public EquipData? PendingUninstallEquip => pendingUninstallEquip;

	public bool IsChangingEquip => pendingUninstallEquip is not null;

	/// <summary>
	/// 开始编辑一艘飞船
	/// </summary>
	public void Load(SubRepository repository, SpaceshipData ship) {
		pendingUninstallEquip = null;
		SelectedEquip = null;
		SelectedOutlet = null;
		LocalRepository = repository;
		EditedShip = ship;
	}

	/// <summary>
	/// 选中一个空槽准备安装
	/// </summary>
	public void BeginInstall(string outletName) {
		pendingUninstallEquip = null;
		SelectedEquip = null;
		SelectedOutlet = outletName;
	}

	/// <summary>
	/// 选中一个已安装装备的槽准备更换. 只记录状态, 不修改数据.
	/// </summary>
	public void BeginChange(string outletName) {
		if (localRepository is null || editedShip is null) {
			GD.PushError("<SpaceshipEditorVM> BeginChange requires repository and ship");
			return;
		}
		if (!editedShip.EquipsLs.ContainsKey(outletName)) {
			BeginInstall(outletName);
			return;
		}

		pendingUninstallEquip = localRepository[editedShip.EquipsLs[outletName]];
		SelectedEquip = pendingUninstallEquip;
		SelectedOutlet = outletName;
	}

	/// <summary>
	/// 取消当前选择. 不修改任何装配数据.
	/// </summary>
	public void CancelSelection() {
		pendingUninstallEquip = null;
		SelectedEquip = null;
		SelectedOutlet = null;
	}

	/// <summary>
	/// 将装备安装到当前选中的槽位. Change状态下会先卸载旧装备.
	/// </summary>
	public void InstallEquip(EquipData equip) {
		if (localRepository is null || editedShip is null || selectedOutlet is null) {
			GD.PushError("<SpaceshipEditorVM> Install requires repository, ship and selected outlet");
			return;
		}

		if (pendingUninstallEquip is not null) {
			GlobalDataSource.Source.ShipUninstallEquip(localRepository, editedShip, selectedOutlet);
		}
		GlobalDataSource.Source.ShipInstallEquip(localRepository, editedShip, selectedOutlet, equip);

		SelectedEquip = equip;
		pendingUninstallEquip = null;
		SelectedOutlet = null;
		EditedShipChangedEvent?.Invoke(editedShip);
	}

	/// <summary>
	/// 从飞船卸载指定装备. 只负责卸载.
	/// </summary>
	public void UninstallEquip(EquipData equip) {
		if (localRepository is null || editedShip is null) {
			GD.PushError("<SpaceshipEditorVM> Uninstall requires repository and ship");
			return;
		}

		foreach (var pair in editedShip.EquipsLs) {
			if (!localRepository[pair.Value].Equals(equip)) {
				continue;
			}
			GlobalDataSource.Source.ShipUninstallEquip(localRepository, editedShip, pair.Key);
			SelectedEquip = null;
			if (selectedOutlet == pair.Key) {
				pendingUninstallEquip = null;
				SelectedOutlet = null;
			}
			EditedShipChangedEvent?.Invoke(editedShip);
			return;
		}
		GD.PushError($"<SpaceshipEditorVM> Can't find outlet of equip: {equip.ID}");
	}

	private SubRepository? localRepository;
	private SpaceshipData? editedShip;
	private EquipData? selectedEquip;
	private string? selectedOutlet;
	private EquipData? pendingUninstallEquip;
}
