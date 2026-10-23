using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
namespace OursGalaxy;
// 实现收益低，遂放弃

// [GlobalClass]
// internal partial class ObserveItemCollection : Resource, IList<ItemBase>, ICollection<ItemBase>, IEnumerable<ItemBase>, IReadOnlyList<ItemBase>, IReadOnlyCollection<ItemBase> {
// 	public event Action<ObserveItemCollection, ChangedArugments>? ElementsChanged;

// 	public int Count => value.Count;

// 	public bool IsReadOnly => value.IsReadOnly;

// 	ItemBase IList<ItemBase>.this[int index] {
// 		get => value[index]; set {
// 			var oldElement = this.value[index];
// 			this.value[index] = value;

// 			ElementsChanged?.Invoke(this, new() {
// 				OldCount = Count,
// 				NewCount = Count,
// 				ChangedIndex = [index],
// 				NewElements = [value],
// 				OldElements = [oldElement]
// 			});
// 		}
// 	}

// 	ItemBase IReadOnlyList<ItemBase>.this[int index] => this.value[index];

// 	int IReadOnlyCollection<ItemBase>.Count => this.Count;

// 	public void Clear() {
// 		var list_bak = value.ToList();
// 		value.Clear();

// 		ElementsChanged?.Invoke(this, new() {
// 			OldCount = list_bak.Count,
// 			NewCount = Count,
// 			ChangedIndex = [.. Enumerable.Range(0, list_bak.Count - 1)],
// 			NewElements = [],
// 			OldElements = list_bak
// 		});
// 	}

// 	public void CopyTo(ItemBase[] array, int arrayIndex) {
// 		value.CopyTo(array, arrayIndex);
// 	}

// 	public System.Collections.IEnumerator GetEnumerator() {
// 		return value.GetEnumerator();
// 	}

// 	public void RemoveAt(int index) {
// 		var oldCount = Count;
// 		var oldElement = value[index];
// 		value.RemoveAt(index);

// 		ElementsChanged?.Invoke(this, new() {
// 			OldCount = oldCount,
// 			NewCount = Count,
// 			ChangedIndex = [index],
// 			OldElements = [oldElement],
// 		});
// 	}

// 	public void Add(ItemBase item) {
// 		var oldCount = Count;
// 		var oldElement = new ItemBase(value.FirstOrDefault(item));
// 		if (old is not null) {
// 			var newElement = value.FirstOrDefault(item)
// 		}
// 	}

// 	public void Add(params ItemBase[] items) {
// 		var oldCount = Count;
// 		var oldElements = new List<ItemBase>();
// 		var newElements = new List<ItemBase>();
// 		foreach (var item in items) {
// 			var old = value.FirstOrDefault<ItemBase>(item);
// 			if (old is not null) {
// 				oldElements = oldElements
// 			}
// 		}

// 		ElementsChanged?.Invoke(this, new() {
// 			OldCount = oldCount,
// 			NewCount = Count,
// 			ChangedIndex = [Count - 1],
// 			NewElements = [item]
// 		});
// 	}

// 	public bool Contains(ItemBase item) {
// 		return value.Contains(item);
// 	}

// 	int IList<ItemBase>.IndexOf(ItemBase item) {
// 		return value.IndexOf(item);
// 	}

// 	void IList<ItemBase>.Insert(int index, ItemBase item) {
// 		var oldCount = Count;
// 		value.Insert(index, item);

// 		ElementsChanged?.Invoke(this, new() {
// 			OldCount = oldCount,
// 			NewCount = Count,
// 			ChangedIndex = [index],
// 			NewElements = [item]
// 		});
// 	}

// 	public bool Remove(ItemBase item) {
// 		var oldCount = Count;
// 		var changedIndex = value.IndexOf(item);
// 		var result = value.Remove(item);

// 		ElementsChanged?.Invoke(this, new() {
// 			OldCount = oldCount,
// 			NewCount = Count,
// 			ChangedIndex = [changedIndex],
// 			OldElements = [item]
// 		});

// 		return result;
// 	}

// 	IEnumerator<ItemBase> IEnumerable<ItemBase>.GetEnumerator() {
// 		return value.GetEnumerator();
// 	}

// 	[Export]
// 	protected Godot.Collections.Array<ItemBase> value { get; set; } = [];

// 	internal class ChangedArugments : EventArgs {
// 		public int OldCount { get; init; }
// 		public int NewCount { get; init; }
// 		public List<int> ChangedIndex { get; init; } = [];
// 		public IReadOnlyCollection<ItemBase> NewElements { get; init; } = [];
// 		public IReadOnlyCollection<ItemBase> OldElements { get; init; } = [];
// 	}

// 	~ObserveItemCollection() {
// 		ElementsChanged = null;
// 	}
// }

//=====================================================================================================================================================

//=====================================================================================================================================================

// [GlobalClass]
// public class ObserveItemCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T> where T : ItemBase {
// 	public T this[int index] {
// 		get => value[index]; set {
// 			var oldCount = Count;
// 			var oldElement = this.value[index];

// 			this.value[index] = value;
// 			Changed?.Invoke(this, new() {
// 				OldCount = oldCount,
// 				NewCount = Count,
// 				MutipleChangedElementIndex = [index],
// 				AddedElements = [value],
// 				RemovedElements = [oldElement]
// 			});
// 		}
// 	}

// 	public int Count => value.Count;

// 	public bool IsReadOnly => false;

// 	public event Action<ObserveItemCollection<T>, ChangedArugments>? Changed;

// 	public void Add(T item) {
// 		var oldCount = Count;
// 		value.Add(item);
// 		Changed?.Invoke(this, new() {
// 			OldCount = oldCount,
// 			NewCount = Count,
// 			MutipleChangedElementIndex = [Count - 1],
// 			AddedElements = [item],
// 			RemovedElements = null
// 		});
// 	}

// 	public void Clear() {
// 		var list_bak = value.ToList();
// 		value.Clear();

// 		Changed?.Invoke(this, new() {
// 			OldCount = list_bak.Count,
// 			NewCount = Count,
// 			MutipleChangedElementIndex = [.. Enumerable.Range(0, list_bak.Count - 1)],
// 			AddedElements = null,
// 			RemovedElements = list_bak,
// 		});
// 	}

// 	public bool Contains(T item) {
// 		return value.Contains(item);
// 	}

// 	public void CopyTo(T[] array, int arrayIndex) {
// 		value.CopyTo(array, arrayIndex);
// 	}

// 	public IEnumerator<T> GetEnumerator() {
// 		return value.GetEnumerator();
// 	}

// 	public int IndexOf(T item) {
// 		return value.IndexOf(item);
// 	}

// 	public void Insert(int index, T item) {
// 		var oldCount = Count;
// 		value.Insert(index, item);

// 		Changed?.Invoke(this, new() {
// 			OldCount = oldCount,
// 			NewCount = Count,
// 			MutipleChangedElementIndex = [index],
// 			AddedElements = [item],
// 			RemovedElements = null
// 		});
// 	}

// 	public bool Remove(T item) {
// 		// Todo:	自定义的删除操作
// 		var removed
// 		var oldCount = Count;
// 		var indexList = value
// 			.Select((item, index) => new { item, index })
//    			.Where(x => TComparer.Equals(x.item, item))
//    			.Select(x => x.index)
//    			.ToList();
// 		value.Remove(item);

// 		Changed?.Invoke(this, new() {
// 			OldCount = oldCount,
// 			NewCount = Count,
// 			MutipleChangedElementIndex = indexList,
// 			AddedElements = null,
// 			RemovedElements =
// 		})
// 	}

// 	public void RemoveAt(int index) {
// 		throw new NotImplementedException();
// 	}

// 	IEnumerator IEnumerable.GetEnumerator() {
// 		return value.GetEnumerator();
// 	}

// 	public class ChangedArugments : EventArgs {
// 		public required int? OldCount { get; init; }
// 		public required int? NewCount { get; init; }

// 		[Obsolete("by multi element index instead", true)]
// 		public int? ChangedElementIndex { get; init; }

// 		public required IReadOnlyCollection<int>? MutipleChangedElementIndex { get; init; }

// 		public required IReadOnlyCollection<T>? AddedElements { get; init; }
// 		public required IReadOnlyCollection<T>? RemovedElements { get; init; }
// 	}

// 	private readonly List<T> value = [];

// 	private static readonly EqualityComparer<T> TComparer = EqualityComparer<T>.Default;
// }
