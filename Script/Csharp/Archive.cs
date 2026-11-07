using Godot.Collections;
using Godot;
using System;
using OursGalaxy.ResourceConfig;
using OursGalaxy.UI;
namespace OursGalaxy;

/// <summary>
/// 存档数据。同时作为游戏ViewModel使用
/// </summary>
[GlobalClass]
internal partial class Archive : Resource {
	public const string SAVE_PATH = "user://Archive.tres";
	public static Archive Data { get; protected set; } = new();

	protected Archive() {
		// 修改时都会触发最新修改日期
		LastSaveTime = DateTime.Now();
	}

	public static void Load() {
		if (FileAccess.FileExists(SAVE_PATH)) {
			Data = ResourceLoader.Load<Archive>(SAVE_PATH);
		}
	}

	public static void Save() {
		var err = ResourceSaver.Save(Archive.Data, SAVE_PATH);
		if (err is not Error.Ok) {
			GD.PushError(err);
		}
	}

	/// <summary>
	/// 存档最后编辑时间
	/// </summary>
	[Export]
	public DateTime LastSaveTime { get; set; }

	#region User Profiles
	[ExportGroup("User Profiles")]

	/// <summary>
	/// 用户ID
	/// </summary>
	[Export]
	public int UserID { get; set; }

	/// <summary>
	/// 用户名
	/// </summary>
	[Export]
	public string UserName { get; set; } = string.Empty;

	/// <summary>
	/// 头像
	/// </summary>
	[Export]
	public Texture2D Avatar { get; set; } = new();

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
		get => solids;
		set {
			solids = Math.Clamp(value, 0, SolidsLimit);
			BaseResourceChangedEvent?.Invoke(BaseResourceEnum.Solids);
		}
	}

	[Export]
	public int SolidsLimit {
		get => solidsLimit;
		set {
			solidsLimit = value;
			BaseResourceLimitChangedEvent?.Invoke(BaseResourceEnum.Solids);
		}
	}

	/// <summary>
	/// 液体资源
	/// </summary>
	[Export]
	public int Liquid {
		get => liquid;
		set {
			liquid = Math.Clamp(value, 0, LiquidLimit);
			BaseResourceChangedEvent?.Invoke(BaseResourceEnum.Liquid);
		}
	}

	[Export]
	public int LiquidLimit {
		get => liquidLimit;
		set {
			liquidLimit = value;
			BaseResourceLimitChangedEvent?.Invoke(BaseResourceEnum.Liquid);
		}
	}

	/// <summary>
	/// 工业品资源
	/// </summary>
	[Export]
	public int IndustrialProducts {
		get => industrialProducts;
		set {
			industrialProducts = Math.Clamp(value, 0, IndustralProductsLimit);
			BaseResourceChangedEvent?.Invoke(BaseResourceEnum.IndustralProducts);
		}
	}

	[Export]
	public int IndustralProductsLimit {
		get => industralProductsLimit;
		set {
			industralProductsLimit = value;
			BaseResourceLimitChangedEvent?.Invoke(BaseResourceEnum.IndustralProducts);
		}
	}

	/// <summary>
	/// 战勋资源
	/// </summary>
	[Export]
	public int Honors {
		get => honors;
		set {
			honors = Math.Clamp(value, 0, HonorsLimit);
			BaseResourceLimitChangedEvent?.Invoke(BaseResourceEnum.Honors);
		}
	}

	[Export]
	public int HonorsLimit {
		get => honorsLimit;
		set {
			honorsLimit = value;
			BaseResourceLimitChangedEvent?.Invoke(BaseResourceEnum.Honors);
		}
	}

	public enum BaseResourceEnum { Solids, Liquid, IndustralProducts, Honors }
	public static event Action<BaseResourceEnum>? BaseResourceChangedEvent;
	public static event Action<BaseResourceEnum>? BaseResourceLimitChangedEvent;

	private int solids = 45000;
	private int liquid = 45000;
	private int industrialProducts = 45000;
	private int honors = 45000;
	private int solidsLimit = 50000;
	private int liquidLimit = 50000;
	private int industralProductsLimit = 50000;
	private int honorsLimit = 50000;

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
	public Array<CombatData> PunitiveCombatList { get; set; } = [];

	[Export]
	public Array<HuntData> TrackedHuntList { get; set; } = [];
	#endregion Battle and Combat
}
