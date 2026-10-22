using Godot.Collections;
using Godot;
namespace OursGalaxy;

/// <summary>
/// 存档数据
/// </summary>
[GlobalClass]
internal partial class Archive : Resource {
	public static Archive Data { get; protected set; } = new();

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

	/// <summary>
	/// 存档最后编辑时间
	/// </summary>
	[Export]
	public Dictionary LastSaveTime { get; set; } = [];

	/// <summary>
	/// 固体资源
	/// </summary>
	[Export]
	public int Solids { get; set; }

	/// <summary>
	/// 液体资源
	/// </summary>
	[Export]
	public int Liquid { get; set; }

	/// <summary>
	/// 工业品资源
	/// </summary>
	[Export]
	public int IndustrialProducts { get; set; }

	/// <summary>
	/// 战勋资源
	/// </summary>
	[Export]
	public int Honors { get; set; }

	[Export]
	public Array<ProduceTaskData> ShipsProduceTasks { get; set; } = [
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,

	];

	[Export]
	public Array<ProduceTaskData> EquipProduceTasks { get; set; } = [
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,
		ProduceTaskData.Empty,

	];
}
