using Godot.Collections;
using Godot;
using System;
using OursGalaxy.ResourceConfig;
using OursGalaxy.UI;
using System.Collections.ObjectModel;
namespace OursGalaxy;

/// <summary>
///游戏ViewModel
/// </summary>
internal partial class GlobalViewModel {
	public static GlobalViewModel Data { get; protected set; } = new();

	public static void Read(GameSavesData saves) {
		Data = new();
		#region User Profile
		Data.UserID = saves.UserID;
		Data.UserName = saves.UserName;

		var avaterImage = new Image();
		var err = avaterImage.LoadWebpFromBuffer(Convert.FromBase64String(saves.AvaterBase64));
		if (err != Error.Ok) {
			GD.PushError($"<GlobalViewModel> Load avater failed: {err}");
			avaterImage = new();
		}
		else {
			Data.Avatar = avaterImage;
		}

		Data.RegisterTime = new DateTime(saves.RegisterDate);
		Data.LastSaveTime = new DateTime(saves.LastSaveDate);
		#endregion User Profile

		#region Base Resource
		Data.baseResource = saves.BaseResource;
		#endregion Base Resource
	}

	public static GameSavesData Save() {
		var saves = new GameSavesData();
		return saves;
	}

	/// <summary>
	/// 存档最后编辑时间
	/// </summary>
	[Export]
	public DateTime LastSaveTime { get; set; } = DateTime.Now();

	#region User Profiles
	[ExportGroup("User Profiles")]

	public Guid UserID { get; set; }

	/// <summary>
	/// 用户名
	/// </summary>
	[Export]
	public string UserName { get; set; } = string.Empty;

	/// <summary>
	/// 头像
	/// </summary>
	[Export]
	public Image Avatar { get; set; } = new();

	/// <summary>
	/// 注册时间
	/// </summary>
	[Export]
	public DateTime RegisterTime { get; set; } = DateTime.Now();

	#endregion User Profiles

	#region Base Resource
	[ExportGroup("Base Resource")]

	/// <summary>
	/// 固体资源
	/// </summary>
	[Export]
	public int Solids {
		get => baseResource[(int)BaseResourceEnum.Solids];
		set {
			baseResource[(int)BaseResourceEnum.Solids] = Math.Clamp(value, 0, SolidsLimit);
			BaseResourceUpdatedEvent?.Invoke();
		}
	}

	[Export]
	public int SolidsLimit {
		get => baseResourceLimit[(int)BaseResourceEnum.Solids];
		set {
			baseResourceLimit[(int)BaseResourceEnum.Solids] = value;
			BaseResourceUpdatedEvent?.Invoke();
		}
	}

	/// <summary>
	/// 液体资源
	/// </summary>
	[Export]
	public int Liquid {
		get => baseResource[(int)BaseResourceEnum.Liquid];
		set {
			baseResource[(int)BaseResourceEnum.Liquid] = Math.Clamp(value, 0, LiquidLimit);
			BaseResourceUpdatedEvent?.Invoke();
		}
	}

	[Export]
	public int LiquidLimit {
		get => baseResourceLimit[(int)BaseResourceEnum.Liquid];
		set {
			baseResourceLimit[(int)BaseResourceEnum.Liquid] = value;
			BaseResourceUpdatedEvent?.Invoke();
		}
	}

	/// <summary>
	/// 工业品资源
	/// </summary>
	[Export]
	public int IndustrialProducts {
		get => baseResource[(int)BaseResourceEnum.IndustralProducts];
		set {
			baseResource[(int)BaseResourceEnum.IndustralProducts] = Math.Clamp(value, 0, IndustralProductsLimit);
			BaseResourceUpdatedEvent?.Invoke();
		}
	}

	[Export]
	public int IndustralProductsLimit {
		get => baseResourceLimit[(int)BaseResourceEnum.IndustralProducts];
		set {
			baseResourceLimit[(int)BaseResourceEnum.IndustralProducts] = value;
			BaseResourceUpdatedEvent?.Invoke();
		}
	}

	/// <summary>
	/// 战勋资源
	/// </summary>
	[Export]
	public int Honors {
		get => baseResource[(int)BaseResourceEnum.Honors];
		set {
			baseResource[(int)BaseResourceEnum.Honors] = Math.Clamp(value, 0, HonorsLimit);
			BaseResourceUpdatedEvent?.Invoke();
		}
	}

	[Export]
	public int HonorsLimit {
		get => baseResourceLimit[(int)BaseResourceEnum.Honors];
		set {
			baseResourceLimit[(int)BaseResourceEnum.Honors] = value;
			BaseResourceUpdatedEvent?.Invoke();
		}
	}

	public enum BaseResourceEnum { Solids = 0, Liquid = 1, IndustralProducts = 2, Honors = 3 }

	public static event Action? BaseResourceUpdatedEvent;

	private int[] baseResource = [45000, 45000, 45000, 45000];
	private int[] baseResourceLimit = [50000, 50000, 50000, 5000];

	#endregion Base Resource

	#region Action Points
	/// <summary>
	/// 体力点
	/// </summary>
	[Export]
	public int ActionPoints {
		get => actionPoints; set {
			actionPoints = Math.Clamp(value, 0, actionPointsLimit);
			ActionPointsChangedEvent?.Invoke();
		}
	}

	public int ActionPointsLimit {
		get => actionPointsLimit;
		set {
			actionPointsLimit = value;
			ActionPointsChangedEvent?.Invoke();
		}
	}

	public event Action? ActionPointsChangedEvent;
	private int actionPoints = 200;
	private int actionPointsLimit = 400;

	[Export]
	public Godot.Collections.Dictionary<ActionPointsPanel.ActionPointsCansEnum, int> ActionPointsCans { get; protected set; } = new() {
		{ActionPointsPanel.ActionPointsCansEnum.Big,3},
		{ActionPointsPanel.ActionPointsCansEnum.Middle,2},
		{ActionPointsPanel.ActionPointsCansEnum.Little,1}
	};
	#endregion Action Points

	#region Produce Tasks
	[ExportGroup("Produce Tasks")]
	[Export]
	public Array<ProduceTaskData> ShipsProduceTasks { get; set; } = [
		//	for test only
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,

	];

	[Export]
	public Array<ProduceTaskData> EquipProduceTasks { get; set; } = [
		// for text only
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,
	];

	[Export]
	public Array<AssignedTaskData> TasksList { get; set; } = [
		// for test only
		new AssignedTaskData("any", AssignedTaskConfig.RatingLevelEnum.S, 2),
		new AssignedTaskData("others",AssignedTaskConfig.RatingLevelEnum.B, 3),
		new AssignedTaskData("and more?", AssignedTaskConfig.RatingLevelEnum.AA, 5)
	];
	#endregion Produce Tasks

	#region Battle and Combat
	[Export]
	public Array<CombatData> PunitiveCombatList { get; protected set; } = [];

	[Export]
	public Array<HuntData> TrackedHuntList { get; protected set; } = [];
	#endregion Battle and Combat

	public ObservableCollection<SpaceshipData> Ships { get; } = [];
	public ObservableCollection<GameItem> GameItems { get; } = [];
}
