using System;
using Godot;

namespace OursGalaxy.ViewModel;

/// <summary>
/// Item资产VM
/// </summary>
[GlobalClass]
internal partial class StorehouseVM : Node {
	/// <summary>
	/// 选中物品事件
	/// </summary>
	public event Action<GameItem?>? SelectElementEvent;

	/// <summary>
	/// 选中了物品
	/// </summary>
	public GameItem? SelectedItem {
		get => selectItem; set {
			selectItem = value;
			SelectElementEvent?.Invoke(selectItem);
		}
	}

	public override void _EnterTree() {
		base._EnterTree();
		GlobalDataSource.Source.GameItemRemovedEvent += OnGameItemRemovedEvent;
	}

	public override void _ExitTree() {
		GlobalDataSource.Source.GameItemRemovedEvent -= OnGameItemRemovedEvent;
		base._ExitTree();
	}

	private void OnGameItemRemovedEvent(GameItem item) {
		if (item.Equals(SelectedItem)) {
			GD.Print("<StorehouseVM> auto canel selected");
			SelectedItem = null;
		}
	}

	private GameItem? selectItem;
}
