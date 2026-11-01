using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
namespace OursGalaxy.UI;

/// <summary>
/// 战斗台
/// </summary>
internal partial class CombatStage : Node3D {
	public CombatStage Load() => (CombatStage)ResourceLoader.Load<PackedScene>("res://Scene/CombatStage.tscn").Instantiate();

	public static CombatStage? Value { get; private set; }

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

	[Export]
	protected Node3D CameraArm { get; set; } = null!;
	[Export]
	protected Camera3D Camera { get; set; } = null!;

	public int LeftFleetTotalHP { get; set; } = 0;
	public int RightFleetTotalHP { get; set; } = 0;

	public CombatResult? Result { get; set; }

	public CombatStage() {
		if (Value is null) {
			Value = this;
			BulletServer.SceneRoot = ActiveBoard;
			return;
		}
		throw new InvalidOperationException("mutiple Combat Stage is not allowed");
	}

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
			ship.Basis = Basis.LookingAt(Vector3.Left, Vector3.Up);
			ship.Camp = CampEnum.Left;
			ship.OnDeath += OnShipDeath;
			ActiveBoard.AddChild(ship);
		}

		Right = rightFleet;
		rightSurvivalCount = Right.ShipsCount;
		foreach (var rightShipAndTactics in rightFleet.Ships) {
			var ship = CreateShip(RightFleetPos.Position, rightShipAndTactics.Key, rightShipAndTactics.Value);
			ship.Basis = Basis.LookingAt(Vector3.Right, Vector3.Up);
			ship.Camp = CampEnum.Right;
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

	public static List<SpaceshipSnapshot> ScanShips(CampEnum campFilter) {
		return [.. Value?.ActiveBoard.GetChildren().OfType<Spaceship>()
			.Where(ship => ship.Camp != campFilter)
			.Select<Spaceship, SpaceshipSnapshot>(ship => new(ship))??[]];
	}

	public static Spaceship? GetShip(GUID id) {
		return Value?.ActiveBoard.GetChildren().OfType<Spaceship>().Where(ship => ship.ID == id).ToList()[0];
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		EndTimer.Timeout += End;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		// GD.Print(touchingnScreen);
		deltaCache = delta;
	}

	public override void _Input(InputEvent @event) {
		if (@event is InputEventScreenTouch touchEvent) {
			touchingnScreen = touchEvent.Pressed;
		}
		if (@event is InputEventMouseButton mbEvent) {
			if (mbEvent.ButtonIndex == MouseButton.Left) {
				touchingnScreen = mbEvent.Pressed;
			}
			else if (mbEvent.ButtonIndex == MouseButton.WheelUp) {
				cameraDistance = Mathf.Clamp(cameraDistance + CAMERA_DISTANCE_MOVE_STEP, CAMERA_DISTANCE_MIN, CAMERA_DISTANCE_MAX);
			}
			else if (mbEvent.ButtonIndex == MouseButton.WheelDown) {
				cameraDistance = Mathf.Clamp(cameraDistance - CAMERA_DISTANCE_MOVE_STEP, CAMERA_DISTANCE_MIN, CAMERA_DISTANCE_MAX);
			}
		}
		if (touchingnScreen && @event is InputEventMouseMotion mmotion) {
			if (OverviewMode) {
				var pos = CameraArm.Position;
				pos = new Vector3(pos.X - mmotion.Relative.X, 0, pos.Y + mmotion.Relative.Y);
				CameraArm.Position = pos;
			}
		}
		base._Input(@event);
	}

	public override void _UnhandledInput(InputEvent @event) {

	}

	public override void _ExitTree() {
		if (Value == this) {
			Value = null;
		}
	}

	private Spaceship CreateShip(Vector3 originPos, SpaceshipData shipData, TacticsSet tactics) {
		var ship = Spaceship.Load(shipData, tactics);
		ship.Position = originPos + tactics.OffsetPosition;
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

	// 摄像机
	private const float OVERVIEW_MODE_CAMERA_CLAMP_PITCH = 70;
	private const float CAMERA_CLAMP_PITCH_ANGLE = 85;
	private const float DEFAULT_CAMERA_DISTANCE = 20;
	private const float CAMERA_DISTANCE_MOVE_STEP = 2;
	private const float CAMERA_MOVE_STEP = 2;
	private const float CAMERA_DISTANCE_MIN = 2;
	private const float CAMERA_DISTANCE_MAX = 50;
	private float cameraYawAngle;
	private float cameraPitchAngle;
	private float cameraDistance = DEFAULT_CAMERA_DISTANCE;
	private bool touchingnScreen;

	private bool OverviewMode = true;


	private double deltaCache;
}
