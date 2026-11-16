using Godot.Collections;
using Godot;
using System;
using OursGalaxy.ResourceConfig;
using OursGalaxy.UI;
using System.Collections.Generic;
using System.Linq;
namespace OursGalaxy;

/// <summary>
///游戏ViewModel, 是AutoLoad的
/// </summary>
internal partial class GlobalDataSource : Node {
	/// <summary>
	/// 全局单例
	/// </summary>
	public static GlobalDataSource Source { get; private set; } = new();

	public static void Read(GameSavesData saves) => Source.savesData = saves;
	public static GameSavesData Save() => Source.savesData;

	/// <summary>
	/// 存档最后编辑时间
	/// </summary>
	public static DateTime LastSaveTime { get => new(Source.savesData.LastSaveDate); set => Source.savesData.LastSaveDate = value.ToUnix(); }

	#region User Profiles
	[ExportGroup("User Profiles")]

	public static GUID UserID { get => Source.savesData.UserID; set => Source.savesData.UserID = value; }

	/// <summary>
	/// 用户名
	/// </summary>
	public static string UserName { get => Source.savesData.UserName; set => Source.savesData.UserName = value; }

	/// <summary>
	/// 头像
	/// </summary>

	public static ImageTexture Avatar {
		get {
			Image image = new();
			var err = image.LoadWebpFromBuffer(Convert.FromBase64String(Source.savesData.AvaterBase64));
			if (err != Error.Ok) {
				GD.PushError($"<GlobalViewModel> Fail to load avater: {err}");
				return new();
			}
			return ImageTexture.CreateFromImage(image);
		}
		set => Source.savesData.AvaterBase64 = Convert.ToBase64String(value.GetImage().SaveWebpToBuffer(false));
	}

	/// <summary>
	/// 注册时间
	/// </summary>
	public static DateTime RegisterTime { get => new(Source.savesData.RegisterDate); set => Source.savesData.RegisterDate = value.ToUnix(); }

	#endregion User Profiles

	public Array<MessageData> Messages { get => Source.savesData.Messages; set => Source.savesData.Messages = value; }

	#region Base Resource
	[ExportGroup("Base Resource")]

	/// <summary>
	/// 固体资源
	/// </summary>

	public static int Solids {
		get => Source.savesData.BaseResource[(int)BaseResourceEnum.Solids];
		set {
			Source.savesData.BaseResource[(int)BaseResourceEnum.Solids] = Math.Clamp(value, 0, SolidsLimit);
			BaseResourceUpdatedEvent?.Invoke();
		}
	}


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

	public static int Liquid {
		get => Source.savesData.BaseResource[(int)BaseResourceEnum.Liquid];
		set {
			Source.savesData.BaseResource[(int)BaseResourceEnum.Liquid] = Math.Clamp(value, 0, LiquidLimit);
			BaseResourceUpdatedEvent?.Invoke();
		}
	}


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

	public static int IndustrialProducts {
		get => Source.savesData.BaseResource[(int)BaseResourceEnum.IndustralProducts];
		set {
			Source.savesData.BaseResource[(int)BaseResourceEnum.IndustralProducts] = Math.Clamp(value, 0, IndustralProductsLimit);
			BaseResourceUpdatedEvent?.Invoke();
		}
	}


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

	public static int Honors {
		get => Source.savesData.BaseResource[(int)BaseResourceEnum.Honors];
		set {
			Source.savesData.BaseResource[(int)BaseResourceEnum.Honors] = Math.Clamp(value, 0, HonorsLimit);
			BaseResourceUpdatedEvent?.Invoke();
		}
	}


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

	public static int ActionPoints {
		get => Source.savesData.ActionPoints; set {
			Source.savesData.ActionPoints = Math.Clamp(value, 0, actionPointsLimit);
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
	public static IReadOnlyList<ShipProduceTaskData> GetShipProduceLine() => Source.savesData.ShipsProduceLines;
	public static IReadOnlyList<EquipProduceTaskData> GetEquipProduceLine() => Source.savesData.EquipsProduceLines;
	public static ShipProduceTaskData GetShipProduceLine(int idx) => GetShipProduceLine()[idx];
	public static EquipProduceTaskData GetEquipProduceLine(int idx) => GetEquipProduceLine()[idx];
	/// <summary>
	/// 提交生产任务
	/// </summary>
	/// <param name="idx"></param>
	/// <param name="data"></param>
	/// <exception cref="InvalidOperationException"></exception>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static void SubmitProduceTask(int idx, ShipProduceTaskData data) {
		if (idx >= 0 && idx < Source.savesData.ShipsProduceLines.Count) {
			if (Source.savesData.ShipsProduceLines[idx].IsEmpty) {
				Source.savesData.ShipsProduceLines[idx] = data;
				return;
			}
			throw new InvalidOperationException("<GlobalViewModel-ProduceTask> target produceline isnt empty");
		}
		throw new ArgumentOutOfRangeException("<GlobalViewModel-ProduceTask> Submit task index out of range or line is not empty");
	}

	/// <summary>
	/// 完成生产任务
	/// </summary>
	/// <param name="idx"></param>
	/// <param name="data"></param>
	/// <exception cref="Exception"></exception>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static void ClaimProduceTask(int idx, out SpaceshipData data) {
		if (idx >= 0 && idx < Source.savesData.ShipsProduceLines.Count) {
			if (!Source.savesData.ShipsProduceLines[idx].IsEmpty) {
				data = Source.savesData.ShipsProduceLines[idx].Target ?? throw new Exception("<GlobalViewModel-ProduceTask> target produceline is empty");
				MainRepository.Add(data);
				Source.savesData.ShipsProduceLines[idx] = new();
			}
		}
		throw new ArgumentOutOfRangeException("<GlobalViewModel-ProduceTask> Claim task index out of range");
	}

	/// <summary>
	///  提交生产任务
	/// </summary>
	/// <param name="idx"></param>
	/// <param name="data"></param>
	/// <exception cref="InvalidOperationException"></exception>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static void SubmitProduceTask(int idx, EquipProduceTaskData data) {
		if (idx >= 0 && idx < Source.savesData.EquipsProduceLines.Count) {
			if (Source.savesData.EquipsProduceLines[idx].IsEmpty) {
				Source.savesData.EquipsProduceLines[idx] = data;
				return;
			}
			throw new InvalidOperationException("<GlobalViewModel-ProduceTask> target produceline isnt empty");
		}
		throw new ArgumentOutOfRangeException("<GlobalViewModel-ProduceTask> Submit task index out of range or line is not empty");
	}

	/// <summary>
	/// 完成生产任务
	/// </summary>
	/// <param name="idx"></param>
	/// <param name="data"></param>
	/// <exception cref="Exception"></exception>
	public static void ClaimProduceTask(int idx, out EquipData data) {
		if (idx >= 0 && idx < Source.savesData.EquipsProduceLines.Count) {
			if (!Source.savesData.EquipsProduceLines[idx].IsEmpty) {
				data = Source.savesData.EquipsProduceLines[idx].Target ?? throw new Exception("<GlobalViewModel-ProduceTask> target produceline is empty");
				MainRepository.Add(data);
				Source.savesData.EquipsProduceLines[idx] = new();
			}
		}
		throw new Exception("<GlobalViewModel-ProduceTask> Claim task index out of range");
	}

	#endregion Produce Tasks

	#region AssignedTask
	public static IReadOnlyList<AssignedTaskData> GetAcceptedTasks() => Source.savesData.AcceptedTasks;

	public static void UpdateAcceptableTask() {
		// 检查30分钟刷新
		if (Source.savesData.LastUpdateAcceptableTasks?.To(DateTime.Now()).TotalMinutes >= 30) {
			// TODO: 刷新任务算法
			Source.savesData.AcceptableTasks.Clear();
			// 刷新6个任务
			var tasksLs = ResourceLoader.ListDirectory("res://ResourceLib/Task/");
			var randRating = (AssignedTaskConfig.RatingLevelEnum)GD.RandRange((int)AssignedTaskConfig.RatingLevelEnum.D, (int)AssignedTaskConfig.RatingLevelEnum.SS);
			var randLevel = GD.RandRange(AssignedTaskConfig.MinLevel, AssignedTaskConfig.MaxLevel);
			for (var i = 0; i < 6; i++) {
				var randTaskName = tasksLs[GD.RandRange(0, tasksLs.Count() - 1)];
				Source.savesData.AcceptableTasks.Add(new(randTaskName, randRating, randLevel));
			}
		}
	}

	public static void AcceptTask(AssignedTaskData data) {
		if (Source.savesData.AcceptableTasks.Contains(data)) {
			// 接受任务并启动时间记录
			data.AcceptedTime = DateTime.Now();
			data.LastEventUpdatedTime = DateTime.Now();
			Source.savesData.AcceptedTasks.Add(data);
			Source.savesData.AcceptableTasks.Remove(data);
			return;
		}
		throw new ArgumentOutOfRangeException("<GlobalViewModel-AssignedTask> Accepted task must be acceptable");
	}

	public static void ClaimTask(AssignedTaskData data) {
		if (Source.savesData.AcceptedTasks.Contains(data) && data.AcceptedTime is not null) {
			if (new DateTime(data.AcceptedTime.ToUnix() + data.Time).Over(data.AcceptedTime)) {
				Solids += data.RewardSolids;
				Liquid += data.RewardLiquids;
				IndustrialProducts += data.RewardIndustrialProduct;
				Source.savesData.AcceptedTasks.Remove(data);
				return;
			}
			throw new Exception("<GlobalViewModel-AssignedTask> Claim task failed, task isnt finished");
		}
		throw new Exception("<GlobalViewModel-AssignedTask> Claim task failed, task isnt accepted");
	}
	#endregion AssignedTask

	#region Battle and Combat
	public static IReadOnlyList<CombatData> GetCombats() => Source.savesData.Combats;
	public static IReadOnlyList<HuntData> GetHunts() => Source.savesData.Hunts;

	public static void CompleteComnbat(CombatData data) => Source.savesData.Combats.Remove(data);
	public static void CompleteHunt(HuntData data) => Source.savesData.Hunts.Remove(data);

	// 缓存的在FleetViewer选中的舰队
	public static FleetData? CachedSelectFleet { get; set; }

	/// <summary>
	/// 准备战斗的舰队配置更新
	/// </summary>
	public static event Action? UpdatedReady2CombatFleetEvent;

	public static FleetData GetReady2CombatFleet(CombatStage.TeamEnum team) => Ready2CombatFleets[team];
	public static void PushReady2CombatFleet(CombatStage.TeamEnum team, FleetData fleet) {
		Ready2CombatFleets[team] = fleet;
		UpdatedReady2CombatFleetEvent?.Invoke();
	}

	private static System.Collections.Generic.Dictionary<CombatStage.TeamEnum, FleetData> Ready2CombatFleets = [];
	#endregion Battle and Combat

	#region Repository
	public static Array<FleetData> Fleets { get => Source.savesData.Fleets; set => Source.savesData.Fleets = value; }
	public static SubRepository MainRepository { get => Source.savesData.Repository; set => Source.savesData.Repository = value; }

	public static Array<GameItem> GameItems { get => Source.savesData.GameItems; set => Source.savesData.GameItems = value; }
	#endregion Repository

	private GameSavesData savesData = new();
}
