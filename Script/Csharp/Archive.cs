using Godot.Collections;
using Godot;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.ComponentModel;
namespace OursGalaxy;

/// <summary>
/// 存档数据。计划同时作为游戏ViewModel使用
/// </summary>
[GlobalClass]
internal partial class Archive : Resource, INotifyPropertyChanged {
	[JsonIgnore]
	public static Archive Data { get; protected set; } = new();

	public event PropertyChangedEventHandler? PropertyChanged;

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

	public static void JsonSnapshot(string? path = null) {
		var json_data = JsonSerializer.Serialize<Archive>(Data, new JsonSerializerOptions() { });
		System.IO.File.WriteAllText(path ?? $"user://snapshot_{Godot.Time.GetDateStringFromSystem(true)}.json", json_data);
	}

	/// <summary>
	/// 用户ID
	/// </summary>
	[Export]
	[JsonPropertyName("UserID")]
	public int UserID { get; set; }
	/// <summary>
	/// 用户名
	/// </summary>
	[Export]
	[JsonPropertyName("UserName")]
	public string UserName { get; set; } = string.Empty;

	[Export]
	[JsonIgnore]
	public Texture2D Avatar { get; set; } = new();

	/// <summary>
	/// 注册时间
	/// </summary>
	[Export]
	[JsonIgnore]
	public Dictionary RegisterTime { get; set; } = [];

	[JsonPropertyName("RegisterTime")]
	protected string RegisterTimeStr => Godot.Time.GetDatetimeStringFromDatetimeDict(RegisterTime, true).ToString();

	/// <summary>
	/// 存档最后编辑时间
	/// </summary>
	[Export]
	[JsonIgnore]
	public Dictionary LastSaveTime { get; set; } = [];

	[JsonPropertyName("LastSaveTime")]
	protected string LastSaveTimeStr => Godot.Time.GetDatetimeStringFromDatetimeDict(LastSaveTime, true).ToString();

	/// <summary>
	/// 固体资源
	/// </summary>
	[Export]
	[JsonPropertyName("Solids")]
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
	[JsonPropertyName("Liquid")]
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
	[JsonPropertyName("IndustralProducts")]
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
	[JsonPropertyName("Honors")]
	public int Honors {
		get => _honors;
		set {
			_honors = value;
			PropertyChanged?.Invoke(this, new("Honor"));
		}
	}

	[Export]
	[JsonPropertyName("ShipsProduceTasks")]
	public Array<ProduceTaskData> ShipsProduceTasks { get; set; } = [
		//	for test only
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,

	];

	[Export]
	[JsonPropertyName("EquipProduceTasks")]
	public Array<ProduceTaskData> EquipProduceTasks { get; set; } = [
		// for text only
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,
	];

	[Export]
	[JsonPropertyName("Missions")]
	public Array<AssignedTaskData> Missions { get; set; } = [
		// for test only
		new AssignedTaskData("any", AssignedTaskData.RatingLevelEnum.S, 2),
		new AssignedTaskData("others",AssignedTaskData.RatingLevelEnum.B, 3),
		new AssignedTaskData("and more?", AssignedTaskData.RatingLevelEnum.AA, 5)
	];

	private int _solids;
	private int _liquid;
	private int _industrialProducts;
	private int _honors;
}
