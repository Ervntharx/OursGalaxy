using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace OursGalaxy.ViewModel;

/// <summary>
/// Item资产VM
/// </summary>
[GlobalClass]
internal partial class StorehouseVM : Node {
	/// <summary>
	/// 新增元素
	/// </summary>
	public event Action<GameItem>? ElementAppendEvent;

	/// <summary>
	/// 移除事件
	/// </summary>
	public event Action<GameItem>? ELementRemovedEvent;

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

	/// <summary>
	/// 取得所有GameItem
	/// </summary>
	/// <returns></returns>
	public IReadOnlyList<GameItem> GetGameItems() => items;

	/// <summary>
	/// 过滤的取得GameItem
	/// </summary>
	/// <param name="predict"></param>
	/// <returns></returns>
	public IReadOnlyList<GameItem> GetGameItems(Func<GameItem, bool> predict) => [.. items.Where(predict)];

	/// <summary>
	/// 增加元素
	/// </summary>
	/// <param name="item"></param>
	public void Add(GameItem item) {
		var it = GameSavesDataMgr.Data.GameItems.FirstOrDefault(it => it.Equals(item));
		if (it is not null) {
			it.Num += item.Num;
		}
		else {
			item.NumChangedEvent += OnElementNumChanged;
			items.Add(item);
			Save();
		}
		ElementAppendEvent?.Invoke(item);
	}

	/// <summary>
	/// 去除元素
	/// </summary>
	/// <param name="item"></param>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public void Remove(GameItem item) {
		item.NumChangedEvent -= OnElementNumChanged;
		if (!items.Remove(item)) {
			throw new ArgumentOutOfRangeException("<ItemRepositoryVM> item not in gameitems");
		}
		Save();
		ELementRemovedEvent?.Invoke(item);
	}

	public override void _EnterTree() {
		base._EnterTree();
		// 从存档注入
		foreach (var item in GameSavesDataMgr.Data.GameItems) {
			item.NumChangedEvent += OnElementNumChanged;
			items.Add(item);
		}
	}

	public override void _ExitTree() {
		foreach (var item in items) {
			item.NumChangedEvent -= OnElementNumChanged;
		}
	}

	// 零数量检查
	private void OnElementNumChanged(GameItem item, int num) {
		if (num == 0) {
			Remove(item);
			if (item.Equals(SelectedItem)) {
				SelectedItem = null;
			}
			GD.Print(string.Join(",", GetGameItems()));
		}
	}

	private void Save() {
		GameSavesDataMgr.Data.GameItems = [.. items];
		GameSavesDataMgr.Save();
	}

	private List<GameItem> items = [];

	private GameItem? selectItem;
}
