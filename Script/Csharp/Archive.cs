using Godot.Collections;
using Godot;
using System.ComponentModel;
namespace OursGalaxy;

/// <summary>
/// 存档数据。计划同时作为游戏ViewModel使用
/// </summary>
[GlobalClass]
internal partial class Archive : Resource, INotifyPropertyChanged {
	public static Archive Data { get; protected set; } = new();

	public event PropertyChangedEventHandler? PropertyChanged;

	protected Archive() {
		// 修改时都会触发最新修改日期
		LastSaveTime = DateTime.Now();
		PropertyChanged += (_, _) => LastSaveTime = DateTime.Now();
	}

	public static void Load() {
		if (FileAccess.FileExists("user://Archive.tscn")) {
			Data = ResourceLoader.Load<Archive>("user://Archive.tscn");
		}
	}

	public static void Save() {
		var err = ResourceSaver.Save(Archive.Data, "user://Archive.tscn");
		if (err is not Error.Ok) {
			GD.PrintErr(err);
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

	[Export]
	public Texture2D Avatar { get; set; } = new();

	/// <summary>
	/// 注册时间
	/// </summary>
	[Export]
	public Dictionary RegisterTime { get; set; } = [];

	#endregion User Profiles

	#region Base Resource
	[ExportGroup("Base Resource")]

	/// <summary>
	/// 固体资源
	/// </summary>
	[Export]
	public int Solids {
		get => _solids;
		set {
			_solids = value;
			PropertyChanged?.Invoke(this, new("Solids"));
		}
	}

	/// <summary>
	/// 液体资源
	/// </summary>
	[Export]
	public int Liquid {
		get => _liquid;
		set {
			_liquid = value;
			PropertyChanged?.Invoke(this, new("Liquid"));
		}
	}

	/// <summary>
	/// 工业品资源
	/// </summary>
	[Export]
	public int IndustrialProducts {
		get => _industrialProducts;
		set {
			_industrialProducts = value;
			PropertyChanged?.Invoke(this, new("IndustralProducts"));
		}
	}

	/// <summary>
	/// 战勋资源
	/// </summary>
	[Export]
	public int Honors {
		get => _honors;
		set {
			_honors = value;
			PropertyChanged?.Invoke(this, new("Honor"));
		}
	}

	private int _solids;
	private int _liquid;
	private int _industrialProducts;
	private int _honors;

	#endregion Base Resource

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
		new AssignedTaskData("any", AssignedTaskData.RatingLevelEnum.S, 2),
		new AssignedTaskData("others",AssignedTaskData.RatingLevelEnum.B, 3),
		new AssignedTaskData("and more?", AssignedTaskData.RatingLevelEnum.AA, 5)
	];
	#endregion Produce Tasks

	#region Battle and Combat
	[Export]
	public Array<PuntiveCombat> PunitiveCombatList { get; set; } = [];

	[Export]
	public Array<Hunt> TrackedHuntList { get; set; } = [];
	#endregion Battle and Combat
}
