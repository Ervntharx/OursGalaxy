using Godot;
using OursGalaxy;
using System;
namespace OursGalaxy.UI;

/// <summary>
/// 战斗台
/// </summary>
internal partial class CombatStage : Node3D {
	public CombatStage Load() => (CombatStage)ResourceLoader.Load<PackedScene>("res://Scene/CombatStage.tscn").Instantiate();

	/// <summary>
	/// 物理层预设
	/// </summary>
	public enum LayerPresetEnum {
		Default = 0,
		LeftShips = 1,
		RightShips = 2,
		LeftBullets = 3,
		RightBullets = 4,
		LeftMissiles = 5,
		RightMissiles = 6
	}

	/// <summary>
	/// 终止到计时器
	/// </summary>
	[Export]
	protected Timer? EndTimer { get; set; }

	/// <summary>
	/// 秒刷新器
	/// </summary>
	[Export]
	protected Timer? SecondsTimer { get; set; }

	/// <summary>
	/// 初始化
	/// </summary>
	/// <param name="leftFleet"></param>
	/// <param name="rightFleet"></param>
	public void Make(FleetData leftFleet, FleetData rightFleet) {
		left = leftFleet;
		Right = rightFleet;
	}

	public void Start() {

	}

	public void End() {

	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private FleetData? left;
	private FleetData? Right;
}
