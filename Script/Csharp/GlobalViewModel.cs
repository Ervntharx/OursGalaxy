using Godot.Collections;
using Godot;
using System;
using OursGalaxy.ResourceConfig;
using OursGalaxy.UI;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Data;
namespace OursGalaxy;

/// <summary>
///游戏ViewModel
/// </summary>
internal partial class GlobalViewModel {
	public static void Read(GameSavesData saves) => Data = saves;
	public static GameSavesData Save() => Data;

	/// <summary>
	/// 存档最后编辑时间
	/// </summary>
	[Export] public static DateTime LastSaveTime { get => new(Data.LastSaveDate); set => Data.LastSaveDate = value.ToUnix(); }

	#region User Profiles
	[ExportGroup("User Profiles")]

	[Export] public static GUID UserID { get => Data.UserID; set => Data.UserID = value; }

	/// <summary>
	/// 用户名
	/// </summary>
	[Export] public static string UserName { get => Data.UserName; set => Data.UserName = value; }

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
	[Export] public static DateTime RegisterTime { get => new(Data.RegisterDate); set => Data.RegisterDate = value.ToUnix(); }

	#endregion User Profiles

	[Export] public Array<MessageData> Messages { get => Data.Messages; set => Data.Messages = value; }

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
	public static IReadOnlyList<ShipProduceTaskData> GetShipProduceLine() => Data.ShipsProduceLines;
	public static IReadOnlyList<EquipProduceTaskData> GetEquipProduceLine() => Data.EquipsProduceLines;
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
		if (idx >= 0 && idx < Data.ShipsProduceLines.Count) {
			if (Data.ShipsProduceLines[idx].IsEmpty) {
				Data.ShipsProduceLines[idx] = data;
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
		if (idx >= 0 && idx < Data.ShipsProduceLines.Count) {
			if (!Data.ShipsProduceLines[idx].IsEmpty) {
				data = Data.ShipsProduceLines[idx].Target ?? throw new Exception("<GlobalViewModel-ProduceTask> target produceline is empty");
				MainRepository.Add(data);
				Data.ShipsProduceLines[idx] = new();
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
		if (idx >= 0 && idx < Data.EquipsProduceLines.Count) {
			if (Data.EquipsProduceLines[idx].IsEmpty) {
				Data.EquipsProduceLines[idx] = data;
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
		if (idx >= 0 && idx < Data.EquipsProduceLines.Count) {
			if (!Data.EquipsProduceLines[idx].IsEmpty) {
				data = Data.EquipsProduceLines[idx].Target ?? throw new Exception("<GlobalViewModel-ProduceTask> target produceline is empty");
				MainRepository.Add(data);
				Data.EquipsProduceLines[idx] = new();
			}
		}
		throw new Exception("<GlobalViewModel-ProduceTask> Claim task index out of range");
	}

	#endregion Produce Tasks

	#region AssignedTask
	public static IReadOnlyList<AssignedTaskData> GetAcceptedTasks() => Data.AcceptedTasks;

	public static void UpdateAcceptableTask() {
		// 检查30分钟刷新
		if (Data.LastUpdateAcceptableTasks?.To(DateTime.Now()).TotalMinutes >= 30) {
			// TODO: 刷新任务算法
			Data.AcceptableTasks.Clear();
			// 刷新6个任务
			var tasksLs = ResourceLoader.ListDirectory("res://ResourceLib/Task/");
			var randRating = (AssignedTaskConfig.RatingLevelEnum)GD.RandRange((int)AssignedTaskConfig.RatingLevelEnum.D, (int)AssignedTaskConfig.RatingLevelEnum.SS);
			var randLevel = GD.RandRange(AssignedTaskConfig.MinLevel, AssignedTaskConfig.MaxLevel);
			for (var i = 0; i < 6; i++) {
				var randTaskName = tasksLs[GD.RandRange(0, tasksLs.Count() - 1)];
				Data.AcceptableTasks.Add(new(randTaskName, randRating, randLevel));
			}
		}
	}

	public static void AcceptTask(AssignedTaskData data) {
		if (Data.AcceptableTasks.Contains(data)) {
			// 接受任务并启动时间记录
			data.AcceptedTime = DateTime.Now();
			data.LastEventUpdatedTime = DateTime.Now();
			Data.AcceptedTasks.Add(data);
			Data.AcceptableTasks.Remove(data);
			return;
		}
		throw new ArgumentOutOfRangeException("<GlobalViewModel-AssignedTask> Accepted task must be acceptable");
	}

	public static void ClaimTask(AssignedTaskData data) {
		if (Data.AcceptedTasks.Contains(data) && data.AcceptedTime is not null) {
			if (new DateTime(data.AcceptedTime.ToUnix() + data.Time).Over(data.AcceptedTime)) {
				Solids += data.RewardSolids;
				Liquid += data.RewardLiquids;
				IndustrialProducts += data.RewardIndustrialProduct;
				Data.AcceptedTasks.Remove(data);
				return;
			}
			throw new Exception("<GlobalViewModel-AssignedTask> Claim task failed, task isnt finished");
		}
		throw new Exception("<GlobalViewModel-AssignedTask> Claim task failed, task isnt accepted");
	}
	#endregion AssignedTask

	#region Battle and Combat
	public static IReadOnlyList<CombatData> GetCombats() => Data.Combats;
	public static IReadOnlyList<HuntData> GetHunts() => Data.Hunts;

	public static void CompleteComnbat(CombatData data) => Data.Combats.Remove(data);
	public static void CompleteHunt(HuntData data) => Data.Hunts.Remove(data);
	#endregion Battle and Combat

	#region Repository
	public static Array<FleetData> Fleets { get => Data.Fleets; set => Data.Fleets = value; }
	public static SubRepository MainRepository { get => Data.Repository; set => Data.Repository = value; }

	public static Array<GameItem> GameItems { get => Data.GameItems; set => Data.GameItems = value; }
	#endregion Repository

	private static GameSavesData Data = new();
}
