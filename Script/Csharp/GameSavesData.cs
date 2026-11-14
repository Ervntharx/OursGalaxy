using Godot.Collections;
using System;
using Godot;
namespace OursGalaxy;

/// <summary>
/// 游戏存档数据
/// </summary>
internal partial class GameSavesData : Resource {
	#region User Profile
	/// <summary>
	/// 用户Guid
	/// </summary>
	[Export]
	public GUID UserID { get; set; } = GUID.Empty;

	/// <summary>
	/// 用户名
	/// </summary>
	[Export]
	public string UserName { get; set; } = string.Empty;

	/// <summary>
	/// 头像的base64数据
	/// </summary>
	[Export]
	public string AvaterBase64 { get; set; } = string.Empty;

	/// <summary>
	/// 注册时间
	/// </summary>
	[Export]
	public long RegisterDate { get; set; }

	/// <summary>
	/// 最后更改时间
	/// </summary>
	[Export]
	public long LastSaveDate { get; set; }
	#endregion User Profile

	#region PVP
	/// <summary>
	/// 分
	/// </summary>
	[Export]
	public int RankPoint { get; set; }

	/// <summary>
	/// 防御舰队的GUID
	/// </summary>
	[Export]
	public GUID DefenseFleetGUID { get; set; } = GUID.Empty;
	#endregion PVP

	[Export]
	public int[] BaseResource { get; set; } = [45000, 45000, 45000, 45000];

	[Export]
	public int ActionPoints { get; set; }

	[Export]
	public int[] ActionCans { get; set; } = new int[4];

	#region Items series

	[Export]
	public Array<GameItem> GameItems { get; set; } = [];

	[Export] public SubRepository Repository { get; set; } = new();

	[Export]
	public Array<FleetData> Fleets { get; set; } = [];
	#endregion Items series

	[Export]
	public Array<CombatData> Combats { get; set; } = [];

	[Export]
	public Array<HuntData> Hunts { get; set; } = [];
}
