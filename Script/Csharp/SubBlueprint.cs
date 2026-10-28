using System;
using System.IO;
using System.Xml.XPath;
using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class SubBlueprint : GameItem {
	protected SubBlueprint() : base() {
		requireNum = new(() => {
			if (TryQuery<int>("RequireNum", out var result)) {
				return result;
			}
			throw new IOException("Can't query valid data");
		});
	}

	public SubBlueprint(string name, ItemBase.QulityEnum qulity, int num = 0) : base("Blueprint", name, qulity, num) {
		requireNum = new(() => {
			if (TryQuery<int>("RequireNum", out var result)) {
				return result;
			}
			throw new IOException("Can't query valid data");
		});
	}

	public int RequireNum => requireNum.Value;


	private Lazy<int> requireNum;
}
