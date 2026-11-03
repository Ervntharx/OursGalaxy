using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy.UI;

/// <summary>
/// 战斗台
/// </summary>
internal partial class CombatStage : Node3D {
	public CombatStage Load() => (CombatStage)ResourceLoader.Load<PackedScene>("res://Scene/CombatStage.tscn").Instantiate();

	public static CombatStage? Value { get; private set; }

	public enum CampEnum { None, Left, Right }

	public Dictionary<CampEnum, GUID> FleetID { get; } = [];

	/// <summary>
	/// 战斗计时器
	/// </summary>
	[Export]
	protected Timer EndTimer { get; set; } = null!;

	/// <summary>
	/// 秒刷新器
	/// </summary>
	[Export]
	protected Timer SecondsTimer { get; set; } = null!;

	[Export]
	protected Godot.Collections.Dictionary<CampEnum, Marker3D> FleetPosMarkers { get; set; } = [];

	[Export]
	protected Node3D ActiveBoard { get; set; } = null!;

	[Export]
	protected Node3D CameraArm { get; set; } = null!;
	[Export]
	protected Camera3D Camera { get; set; } = null!;

	[Export]
	protected CombatPanel Panel { get; set; } = null!;

	// 战斗钟
	public int CombatTotalTime => Mathf.RoundToInt(EndTimer.WaitTime);
	public int CombatLeftTime => Mathf.RoundToInt(EndTimer.TimeLeft);

	public CombatResult? Result { get; set; }

	// statics data
	#region Statics Data
	public int GetFleetLeftHP(CampEnum camp) {
		var result = 0;

		foreach (var shipsLs in FleetShips[camp].Values) {
			foreach (var ship in shipsLs) {
				result += ship.CurrentHP;
			}
		}
		return result;
	}

	public int GetShipCountInScaleSize(CampEnum camp, SpaceshipConfig.ScaleSizeEnum scaleSize) => FleetShips[camp][scaleSize].Count;

	public Dictionary<CampEnum, Dictionary<SpaceshipConfig.ScaleSizeEnum, List<Spaceship>>> FleetShips { get; } = [];

	public bool IsLeftFlagshipAlive { get; set; }
	public bool IsRightFlagshipAlive { get; set; }
	#endregion Statics Data

	[Export]
	public Godot.Collections.Dictionary<CampEnum, Vector3> InitialFleetDir { get; set; } = [];

	public CombatStage() {
		if (Value is null) {
			Value = this;
			BulletServer.SceneRoot = ActiveBoard;
			return;
		}
		throw new InvalidOperationException("mutiple Combat Stage is not allowed");
	}

	/// <summary>
	/// 配置舰队
	/// </summary>
	/// <param name="camp"></param>
	/// <param name="fleet"></param>
	public void Make(CampEnum camp, FleetData fleet) {
		FleetID.Add(camp, fleet.ID);
		foreach (var pair in fleet.Ships) {
			var ship = CreateShip(FleetPosMarkers[camp].Position, pair.Key, pair.Value);
			ship.Basis = Basis.LookingAt(InitialFleetDir[camp], Vector3.Up);
			ship.FromFleetID = fleet.ID;
			ActiveBoard.AddChild(ship);
		}
	}

	public void Start() {
		foreach (var ship in ActiveBoard.GetChildren().OfType<Spaceship>()) {
			ship.Active = true;
		}
		EndTimer.Start();
	}

	public void End() {
		Result = new() {
			Time = DateTime.Now(),
			TotalSeconds = (long)(SecondsTimer.WaitTime - SecondsTimer.TimeLeft),
			Win = GetFleetLeftHP(CampEnum.Left) > GetFleetLeftHP(CampEnum.Right)
			// TODO:	记录舰队剩余状态
		};
	}

	[Obsolete("用回Camp")]
	public static List<SpaceshipSnapshot> ProbeShips(GUID filterFleetID) {
		return [..Value?.ActiveBoard.GetChildren().OfType<Spaceship>()
			.Where(ship=>ship.FromFleetID != filterFleetID)
			.Select<Spaceship,SpaceshipSnapshot>(ship=>new(ship))??[]];
	}

	public static List<SpaceshipSnapshot> ProbeShips(CampEnum campFilter) {
		return [.. Value?.ActiveBoard.GetChildren().OfType<Spaceship>()
			.Where(ship => {
				if(campFilter == CampEnum.Left && ship.FromFleetID == Value.FleetID[campFilter]){
					return false;
				}
				else if(campFilter == CampEnum.Right && ship.FromFleetID == Value.FleetID[campFilter] ){
					return false;
				}
				return true;
			})
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
				pos = new Vector3(pos.X - mmotion.Relative.X * CAMERA_MOVE_STEP, 0, pos.Z - mmotion.Relative.Y * CAMERA_MOVE_STEP);
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

	private Spaceship CreateShip(Vector3 originPos, SpaceshipData data, TacticsSet tactics) {
		var ship = SpaceshipFactory.Make(data.GetShipTypeName());
		GD.Print(data.Code);
		ship.ID = data.ID;
		ship.TypeName = data.GetShipTypeName();
		ship.CustomName = data.CustomName;
		ship.CurrentHP = data.CurrentHP;
		// ship.Range = data.Range;
		ship.Tactics = tactics;
		ship.Position = originPos + tactics.OffsetPosition;
		return ship;
	}

	// 摄像机
	private const float OVERVIEW_MODE_CAMERA_CLAMP_PITCH = 70;
	private const float CAMERA_CLAMP_PITCH_ANGLE = 85;
	private const float DEFAULT_CAMERA_DISTANCE = 20;
	private const float CAMERA_DISTANCE_MOVE_STEP = 2;
	private const float CAMERA_MOVE_STEP = 0.2f;
	private const float CAMERA_DISTANCE_MIN = 2;
	private const float CAMERA_DISTANCE_MAX = 50;
	private float cameraYawAngle;
	private float cameraPitchAngle;
	private float cameraDistance = DEFAULT_CAMERA_DISTANCE;
	private bool touchingnScreen;

	private bool OverviewMode = true;


	private double deltaCache;
}
