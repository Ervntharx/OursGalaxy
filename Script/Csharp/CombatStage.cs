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

	public CombatResult? Result { get; set; }

	public StaticsData Statics { get; set; } = new();

	[Export]
	public Godot.Collections.Dictionary<TeamEnum, Vector3> InitialFleetDir { get; set; } = [];

	public override void _EnterTree() {
		base._EnterTree();
		CombatStageServer.Register(Name, this);
		BulletServer.Register(Name, this);
		SecondsTimer.Timeout += UpdateLeftTimeLabel;
		Panel.RetreatEvent += () => End(CombatResult.EndReasonEnum.Retreat);
		EndTimer.Timeout += () => End(CombatResult.EndReasonEnum.Timeout);
		Statics.StaticsDataChanged += Panel.OnStaticsSpawnerUpdated;
	}

	/// <summary>
	/// 配置舰队
	/// </summary>
	/// <param name="team"></param>
	/// <param name="fleet"></param>
	public void PutOnFleet(TeamEnum team, FleetData fleet) {
		// 旗舰
		var flagship = CreateShip(FleetPosMarkers[team].Position, fleet.Flagship, fleet.FlagshipTactics);
		flagship.Basis = Basis.LookingAt(InitialFleetDir[team], Vector3.Up);
		flagship.Team = team;
		ActiveBoard.AddChild(flagship);
		flagship.IsFlagShip = true;
		Statics.Track(flagship);
		// 成员船
		foreach (var pair in fleet.Ships) {
			var ship = CreateShip(FleetPosMarkers[team].Position, pair.Key, pair.Value);
			ship.Basis = Basis.LookingAt(InitialFleetDir[team], Vector3.Up);
			ship.Team = team;
			ActiveBoard.AddChild(ship);
			Statics.Track(ship);
		}
	}

	public void Start() {
		foreach (var ship in ActiveBoard.GetChildren().OfType<Spaceship>()) {
			ship.BaseState = Spaceship.BaseStateEnum.Active;
		}
		EndTimer.Start();
	}

	public IEnumerable<Spaceship> GetShips() => ActiveBoard.GetChildren().OfType<Spaceship>();

	public void End(CombatResult.EndReasonEnum reason) {
		Result = new() {
			EndReason = reason,
			Time = DateTime.Now(),
			TotalSeconds = (long)(SecondsTimer.WaitTime - SecondsTimer.TimeLeft),
			Win = Statics.HP[TeamEnum.Left] / Statics.FullHP[TeamEnum.Left] > Statics.HP[TeamEnum.Right] / Statics.FullHP[TeamEnum.Right]
			// TODO:	记录舰队剩余状态
		};
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
		base._UnhandledInput(@event);
	}

	public override void _ExitTree() {
		CombatStageServer.Free(Name);
		BulletServer.Free(Name);
		SecondsTimer.Timeout -= UpdateLeftTimeLabel;
	}

	private Spaceship CreateShip(Vector3 originPos, SpaceshipData data, TacticsSet tactics) {
		var ship = SpaceshipFactory.Make(data);
		ship.Tactics = tactics;
		ship.Position = originPos + tactics.OffsetPosition;
		return ship;
	}

	private void UpdateLeftTimeLabel() {
		Panel.UpdateLeftTime(EndTimer.TimeLeft);
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
