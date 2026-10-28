using System.Linq;
using Godot;
namespace OursGalaxy.UI;

/// <summary>
/// 战斗台
/// </summary>
internal partial class CombatStage : Node3D {
	public CombatStage Load() => (CombatStage)ResourceLoader.Load<PackedScene>("res://Scene/CombatStage.tscn").Instantiate();

	/// <summary>
	/// 终止到计时器
	/// </summary>
	[Export]
	protected Timer EndTimer { get; set; } = null!;

	/// <summary>
	/// 秒刷新器
	/// </summary>
	[Export]
	protected Timer SecondsTimer { get; set; } = null!;

	[Export]
	protected StaticsServer Statics { get; set; } = null!;

	[Export]
	protected Marker3D LeftFleetPos { get; set; } = null!;

	[Export]
	protected Marker3D RightFleetPos { get; set; } = null!;

	[Export]
	protected Node3D ActiveBoard { get; set; } = null!;

	/// <summary>
	/// 初始化
	/// </summary>
	/// <param name="leftFleet"></param>
	/// <param name="rightFleet"></param>
	public void Make(FleetData leftFleet, FleetData rightFleet) {
		left = leftFleet;
		leftSurvivalCount = left.ShipsCount;
		foreach (var leftShipAndTactics in leftFleet.Ships) {
			ActiveBoard.AddChild(CreateShip(LeftFleetPos.Position, leftShipAndTactics.Key, leftShipAndTactics.Value));
		}

		Right = rightFleet;
		rightSurvivalCount = Right.ShipsCount;
		foreach (var rightShipAndTactics in rightFleet.Ships) {
			ActiveBoard.AddChild(CreateShip(RightFleetPos.Position, rightShipAndTactics.Key, rightShipAndTactics.Value));
		}
	}

	public void Start() {
		foreach (var ship in ActiveBoard.GetChildren().OfType<Spaceship>()) {
			ship.Active = true;
		}
	}

	public void End() {

	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		EndTimer.Timeout += End;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private Spaceship CreateShip(Vector3 originPos, SpaceshipData shipData, TacticsSet tactics) {
		var ship = new Spaceship(shipData) {
			Position = originPos + tactics.OffsetPosition
		};
		return ship;
	}

	private FleetData? left;
	private FleetData? Right;

	// 存活舰船计数
	private int leftSurvivalCount;
	private int rightSurvivalCount;
}
