using Godot.Collections;
using Godot;
using System;
using OursGalaxy.ResourceConfig;
using OursGalaxy.UI;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
namespace OursGalaxy;

/// <summary>
///游戏ViewModel
/// </summary>
internal partial class GlobalViewModel {
	private static GameSavesData Data = new();

	public static void Read(GameSavesData saves) => Data = saves;
	public static GameSavesData Save() => Data;

	/// <summary>
	/// 存档最后编辑时间
	/// </summary>
	[Export]
	public static DateTime LastSaveTime { get => new(Data.LastSaveDate); set => Data.LastSaveDate = value.ToUnix(); }

	#region User Profiles
	[ExportGroup("User Profiles")]

	[Export]
	public static GUID UserID { get => Data.UserID; set => Data.UserID = value; }

	/// <summary>
	/// 用户名
	/// </summary>
	[Export]
	public static string UserName { get => Data.UserName; set => Data.UserName = value; }

	/// <summary>
	/// 头像
	/// </summary>
	[Export]
	public static ImageTexture Avatar {
		get {
			Image image = new();
			var err = image.LoadWebpFromBuffer(Convert.FromBase64String(Data.AvaterBase64));
			if (err != Error.Ok) {
				GD.PushError($"<GlobalViewModel> Fail to load avater: {err}");
				return new();
			}
			return ImageTexture.CreateFromImage(image);
		}
		set => Data.AvaterBase64 = Convert.ToBase64String(value.GetImage().SaveWebpToBuffer(false));
	}

	/// <summary>
	/// 注册时间
	/// </summary>
	[Export]
	public static DateTime RegisterTime { get => new(Data.RegisterDate); set => Data.RegisterDate = value.ToUnix(); }

	#endregion User Profiles

	#region Base Resource
	[ExportGroup("Base Resource")]

	/// <summary>
	/// 固体资源
	/// </summary>
	[Export]
	public static int Solids {
		get => Data.BaseResource[(int)BaseResourceEnum.Solids];
		set {
			Data.BaseResource[(int)BaseResourceEnum.Solids] = Math.Clamp(value, 0, SolidsLimit);
			BaseResourceUpdatedEvent?.Invoke();
		}
	}

	[Export]
	public static int SolidsLimit {
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
	public static int Liquid {
		get => Data.BaseResource[(int)BaseResourceEnum.Liquid];
		set {
			Data.BaseResource[(int)BaseResourceEnum.Liquid] = Math.Clamp(value, 0, LiquidLimit);
			BaseResourceUpdatedEvent?.Invoke();
		}
	}

	[Export]
	public static int LiquidLimit {
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
	public static int IndustrialProducts {
		get => Data.BaseResource[(int)BaseResourceEnum.IndustralProducts];
		set {
			Data.BaseResource[(int)BaseResourceEnum.IndustralProducts] = Math.Clamp(value, 0, IndustralProductsLimit);
			BaseResourceUpdatedEvent?.Invoke();
		}
	}

	[Export]
	public static int IndustralProductsLimit {
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
	public static int Honors {
		get => Data.BaseResource[(int)BaseResourceEnum.Honors];
		set {
			Data.BaseResource[(int)BaseResourceEnum.Honors] = Math.Clamp(value, 0, HonorsLimit);
			BaseResourceUpdatedEvent?.Invoke();
		}
	}

	[Export]
	public static int HonorsLimit {
		get => baseResourceLimit[(int)BaseResourceEnum.Honors];
		set {
			baseResourceLimit[(int)BaseResourceEnum.Honors] = value;
			BaseResourceUpdatedEvent?.Invoke();
		}
	}

	public enum BaseResourceEnum { Solids = 0, Liquid = 1, IndustralProducts = 2, Honors = 3 }

	public static event Action? BaseResourceUpdatedEvent;

	private static int[] baseResourceLimit = [50000, 50000, 50000, 5000];

	#endregion Base Resource

	#region Action Points
	/// <summary>
	/// 体力点
	/// </summary>
	[Export]
	public static int ActionPoints {
		get => Data.ActionPoints; set {
			Data.ActionPoints = Math.Clamp(value, 0, actionPointsLimit);
			ActionPointsChangedEvent?.Invoke();
		}
	}

	public static int ActionPointsLimit {
		get => actionPointsLimit;
		set {
			actionPointsLimit = value;
			ActionPointsChangedEvent?.Invoke();
		}
	}

	public static event Action? ActionPointsChangedEvent;
	private static int actionPointsLimit = 400;

	public static Godot.Collections.Dictionary<ActionPointsPanel.ActionPointsCansEnum, int> ActionPointsCans { get; protected set; } = new() {
		{ActionPointsPanel.ActionPointsCansEnum.Big,3},
		{ActionPointsPanel.ActionPointsCansEnum.Middle,2},
		{ActionPointsPanel.ActionPointsCansEnum.Little,1}
	};
	#endregion Action Points

	#region Produce Tasks
	[ExportGroup("Produce Tasks")]
	[Export]
	public static Array<ProduceTaskData> ShipsProduceTasks { get; set; } = [
		//	for test only
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,

	];

	[Export]
	public static Array<ProduceTaskData> EquipProduceTasks { get; set; } = [
		// for text only
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,
	];

	[Export]
	public static Array<AssignedTaskData> TasksList { get; set; } = [
		// for test only
		new AssignedTaskData("any", AssignedTaskConfig.RatingLevelEnum.S, 2),
		new AssignedTaskData("others",AssignedTaskConfig.RatingLevelEnum.B, 3),
		new AssignedTaskData("and more?", AssignedTaskConfig.RatingLevelEnum.AA, 5)
	];
	#endregion Produce Tasks

	#region Battle and Combat
	[Export]
	public static Array<CombatData> PunitiveCombatList { get; protected set; } = [];

	[Export]
	public static Array<HuntData> TrackedHuntList { get; protected set; } = [];
	#endregion Battle and Combat

	public static ObservableCollection<SpaceshipData> Ships { get; } = [];
	public static ObservableCollection<EquipData> Equips { get; } = [];
	public static ObservableCollection<GameItem> GameItems { get; } = [];
}
