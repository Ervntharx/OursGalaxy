using Godot;
using OursGalaxy.ResourceConfig;
namespace OursGalaxy;

/// <summary>
/// 存档管理器,是AutoLoad的
/// </summary>
internal partial class GameSavesDataMgr : Node {
	public static GameSavesData Data { get => data; }

	public static void Read() {
		if (FileAccess.FileExists(SAVE_PATH)) {
			var data = ResourceLoader.Load<GameSavesData>(SAVE_PATH);
			GlobalViewModel.Read(data);
		}
		else {
			GlobalViewModel.Read(InitSavesData());
		}
	}

	public static void Save() {
		var data = Data;
		var err = ResourceSaver.Save(data, SAVE_PATH);
		if (err != Error.Ok) {
			GD.PushError(err);
		}
		GD.Print(FileAccess.FileExists(SAVE_PATH) == true ? "save success." : "why?");
	}

	public static void Clear() {
		if (FileAccess.FileExists(SAVE_PATH)) {
			DirAccess.RemoveAbsolute(SAVE_PATH);
			GD.PushWarning("<GameSavesDataMgr> Has removed saves data");
			return;
		}
		GD.PushWarning("<GameSavesDataMgr> no file need to clear");
	}

	public override void _Ready() {
		base._Ready();
		GD.Print("Read");
		Read();
	}

	private static GameSavesData InitSavesData() {
		var data = new GameSavesData {
			UserID = GUID.Generate(),
			UserName = "user name0123",
			RegisterDate = DateTime.Now().ToUnix(),
			LastSaveDate = DateTime.Now().ToUnix(),
			RankPoint = 100,

			ActionPoints = 200,
			BaseResource = [1000, 2000, 3000, 4000],

		};

		data.Messages.Add(new() {
			MessageTabType = "Notice",
			Head = "Welcome",
			Content = "Welcome to OursGalaxy.\n*This Message's designed to test your message panel*"
		});

		data.Repository = new();

		// idcard
		data.GameItems.Add(new("IDCard", ResourceConfig.ItemBaseConfig.QulityEnum.Gold));

		// 三条鱼
		data.Repository.Add(new SpaceshipData("Fish", ResourceConfig.ItemBaseConfig.QulityEnum.White));
		data.Repository.Add(new SpaceshipData("Fish", ResourceConfig.ItemBaseConfig.QulityEnum.White));
		data.Repository.Add(new SpaceshipData("Fish", ResourceConfig.ItemBaseConfig.QulityEnum.White));

		// 10*100炮
		for (var i = 0; i < 10; i++) {
			data.Repository.Add(new TurretData("Equip/Turret/Photon", "100Photon", ItemBaseConfig.QulityEnum.White));
		}

		return data;
	}

	private static GameSavesData data = new();

	private const string SAVE_PATH = "user://Saves.tres";
}
