using Godot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace OursGalaxy.UI;

internal partial class CombatPanel : Panel {
	[Export]
	protected ProgressBar? LeftProgressBar { get; set; }
	[Export]
	protected ProgressBar? RightProgressBar { get; set; }

	[Export]
	protected Label? TimeLabel { get; set; }

	[Export]
	protected Button? HideUIButton { get; set; }

	[Export]
	protected Button? Retreat { get; set; }

	public FleetStatics Left { get; private set; } = new();

	public FleetStatics Right { get; private set; } = new();

	public void Make(FleetData left, FleetData right) {

	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	public class FleetStatics : INotifyPropertyChanged {
		public string DisplayName { get => _displayName; set => _displayName = value; }
		public bool FlagshipAlive { get => _flagshipAlive; set => _flagshipAlive = value; }
		public int this[Spaceship.ScaleSizeEnum scaleSizeIndex] {
			get {
				return shipsStatics[scaleSizeIndex];
			}
			set {
				shipsStatics[scaleSizeIndex] = value;
				PropertyChanged?.Invoke(this, new(scaleSizeIndex.ToString()));
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private string _displayName = string.Empty;
		private bool _flagshipAlive;
		private Dictionary<Spaceship.ScaleSizeEnum, int> shipsStatics = [];
	}

	private bool _leftFleetFlagshipAlive;
	private bool _rightFleetshipAlive;
}
