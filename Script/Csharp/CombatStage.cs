using System.Linq;
using Godot;
namespace OursGalaxy.UI;

/// <summary>
/// 战斗台
/// </summary>
internal partial class CombatStage : Node3D {
	public CombatStage Load() => (CombatStage)ResourceLoader.Load<PackedScene>("res://Scene/CombatStage.tscn").Instantiate();

	public enum CampEnum { None, Left, Right }

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

	public int LeftFleetTotalHP { get; set; } = 0;
	public int RightFleetTotalHP { get; set; } = 0;

	public CombatResult? Result { get; set; }

	/// <summary>
	/// 初始化
	/// </summary>
	/// <param name="leftFleet"></param>
	/// <param name="rightFleet"></param>
	public void Make(FleetData leftFleet, FleetData rightFleet) {
		left = leftFleet;
		leftSurvivalCount = left.ShipsCount;
		foreach (var leftShipAndTactics in leftFleet.Ships) {
			var ship = CreateShip(LeftFleetPos.Position, leftShipAndTactics.Key, leftShipAndTactics.Value);
			ship.OnDeath += OnShipDeath;
			ActiveBoard.AddChild(ship);
		}

		Right = rightFleet;
		rightSurvivalCount = Right.ShipsCount;
		foreach (var rightShipAndTactics in rightFleet.Ships) {
			var ship = CreateShip(RightFleetPos.Position, rightShipAndTactics.Key, rightShipAndTactics.Value);
			ship.OnDeath += OnShipDeath;
			ActiveBoard.AddChild(ship);
		}
	}

	public void Start() {
		foreach (var ship in ActiveBoard.GetChildren().OfType<Spaceship>()) {
			ship.Active = true;
		}
	}

	public void End() {
		Result = new() {
			Time = DateTime.Now(),
			TotalSeconds = (long)(SecondsTimer.WaitTime - SecondsTimer.TimeLeft),
			Win = LeftFleetTotalHP > RightFleetTotalHP
			// TODO:	记录舰队剩余状态
		};
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

	private void OnShipDeath(SpaceshipSnapshot shipshot) {
		if (shipshot.Camp == CampEnum.Left) {
			leftSurvivalCount -= 1;
		}
		if (shipshot.Camp == CampEnum.Right) {
			rightSurvivalCount -= 1;
		}

		if (leftSurvivalCount == 0 || rightSurvivalCount == 0) {
			End();
		}
	}

	private FleetData? left;
	private FleetData? Right;

	// 存活舰船计数
	private int leftSurvivalCount;
	private int rightSurvivalCount;
}
