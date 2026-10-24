using System;
using System.Collections.Generic;
using Godot;

namespace OursGalaxy;

[GlobalClass]
internal partial class StaticsServer : Node {
	public void Regiter(Spaceship ship) {
		value.TryAdd(new(ship), new());
	}

	public StaticsSet this[Spaceship ship] {
		get {
			foreach (var pair in value) {
				if (pair.Key.IsFrom(ship)) {
					return pair.Value;
				}
			}
			throw new ArgumentOutOfRangeException();
		}
		set {
			this.value.TryAdd(new(ship), value);
		}
	}

	private Dictionary<SpaceshipSnapshot, StaticsSet> value = [];

	public class StaticsSet {
		public Dictionary<Spaceship.ScaleSizeEnum, int> KillCount { get; private set; } = [];
		public int TotalTakeDamage { get; set; }
		public int TotalDamage { get; set; }
		public int? DeadSeconds { get; set; }
	}
}
