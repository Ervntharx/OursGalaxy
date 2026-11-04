using System.Collections.Generic;
using System.Linq;
using Godot;
namespace OursGalaxy.UI;

/// <summary>
/// 战斗台
/// </summary>
internal partial class CombatStage : Node3D {
	public CombatStage Load() => (CombatStage)ResourceLoader.Load<PackedScene>("res://Scene/CombatStage.tscn").Instantiate();

	public enum TeamEnum { None, Left, Right }

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
	protected Godot.Collections.Dictionary<TeamEnum, Marker3D> FleetPosMarkers { get; set; } = [];

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

	public StaticsSpawner Statics { get; set; } = new();

	[Export]
	public Godot.Collections.Dictionary<TeamEnum, Vector3> InitialFleetDir { get; set; } = [];

	public override void _EnterTree() {
		base._EnterTree();
		CombatStageServer.Register(Name, this);
		BulletServer.Register(Name, this);
	}

	/// <summary>
	/// 配置舰队
	/// </summary>
	/// <param name="camp"></param>
	/// <param name="fleet"></param>
	public void Make(TeamEnum camp, FleetData fleet) {
		foreach (var pair in fleet.Ships) {
			var ship = CreateShip(FleetPosMarkers[camp].Position, pair.Key, pair.Value);
			ship.Basis = Basis.LookingAt(InitialFleetDir[camp], Vector3.Up);
			CombatStageServer.Connect(ship.ID, Name);
			ActiveBoard.AddChild(ship);
		}
	}

	public void Start() {
		foreach (var ship in ActiveBoard.GetChildren().OfType<Spaceship>()) {
			ship.BaseState = Spaceship.BaseStateEnum.Active;
		}
		EndTimer.Start();
	}

	public void End() {
		Result = new() {
			Time = DateTime.Now(),
			TotalSeconds = (long)(SecondsTimer.WaitTime - SecondsTimer.TimeLeft),
			Win = Statics.FleetHP[TeamEnum.Left] > Statics.FleetHP[TeamEnum.Right]
			// TODO:	记录舰队剩余状态
		};
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
		CombatStageServer.Free(this.Name);
		BulletServer.Free(this.Name);
	}

	private Spaceship CreateShip(Vector3 originPos, SpaceshipData data, TacticsSet tactics) {
		var ship = SpaceshipFactory.Make(data.GetShipTypeName());
		GD.Print(data.Code);
		ship.ID = data.ID;
		ship.TypeName = data.GetShipTypeName();
		ship.CustomName = data.CustomName;
		ship.CurrentHP = data.CurrentHP;
		ship.Tactics = tactics;
		ship.Position = originPos + tactics.OffsetPosition;
		BulletServer.Connect(ship.ID, this.Name);
		return ship;
	}

	private Dictionary<string, CombatStage> servers = [];

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
