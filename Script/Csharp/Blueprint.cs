using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class Blueprint : GameItem {
	protected Blueprint() : base() {
		source = new(QueryItems("Source"));
		result = new(QueryItems("Result"));
	}

	public Blueprint(string name, ItemBase.QulityEnum qulity, int num = 0) : base("Blueprint", name, qulity, num) {
		source = new(QueryItems("Source"));
		result = new(QueryItems("Result"));
	}

	public IReadOnlyCollection<ItemBase> Source => source.Value;
	public IReadOnlyCollection<ItemBase> Result => result.Value;

	private Lazy<List<ItemBase>> source;
	private Lazy<List<ItemBase>> result;

	private List<ItemBase> QueryItems(string tableItemName) {
		if (TryQuery<string>(tableItemName, out var result)) {
			return [.. result.Split(";", false).Select<string, ItemBase>(code => new(code))];
		}
		throw new IOException("Can't query valid data");
	}
}
