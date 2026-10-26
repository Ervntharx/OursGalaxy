using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace OursGalaxy;

/// <summary>
/// 设置配置。保留tscn和json两种序列化方式
/// </summary>
[GlobalClass]
internal partial class SettingsArchive : Resource {
	/// <summary>
	/// 保存路径，tscn版本
	/// </summary>
	public const string SAVE_PATH_TSCN = "user://settings.tscn";
	/// <summary>
	/// 保存路径，json版本
	/// </summary>
	public const string SAVE_PATH_JSON = "user://settings.json";

	/// <summary>
	/// 单例
	/// </summary>
	[JsonIgnore]
	public static SettingsArchive Data { get; protected set; } = new();

	#region Apperance UI

	public enum PanelsMarginEnum { Left, Right, Top, Bottom }
	/// <summary>
	/// 界面边距变化时
	/// </summary>
	public event Action<PanelsMarginEnum, int>? PanelsMarginChanged;

	/// <summary>
	/// 左边距
	/// </summary>
	[Export]
	[JsonPropertyName("Apperance_UI_Panels_Margin_Left")]
	public int Apperance_UI_Panels_Margin_Left {
		get => panelsMargin[PanelsMarginEnum.Left];
		set {
			panelsMargin[PanelsMarginEnum.Left] = value;
			PanelsMarginChanged?.Invoke(PanelsMarginEnum.Left, value);
		}
	}

	/// <summary>
	/// 右边距
	/// </summary>
	[Export]
	[JsonPropertyName("Apperance_UI_Panels_Margin_Right")]
	public int Apperance_UI_Panels_Margin_Right {
		get => panelsMargin[PanelsMarginEnum.Right];
		set {
			panelsMargin[PanelsMarginEnum.Right] = value;
			PanelsMarginChanged?.Invoke(PanelsMarginEnum.Right, value);
		}
	}

	/// <summary>
	/// 上边距
	/// </summary>
	[Export]
	[JsonPropertyName("Apperance_UI_Panels_Margin_Top")]
	public int Apperance_UI_Panels_Margin_Top {
		get => panelsMargin[PanelsMarginEnum.Top];
		set {
			panelsMargin[PanelsMarginEnum.Top] = value;
			PanelsMarginChanged?.Invoke(PanelsMarginEnum.Top, value);
		}
	}

	/// <summary>
	/// 下边距
	/// </summary>
	[Export]
	[JsonPropertyName("Apperance_UI_Panels_Margin_Bottom")]
	public int Apperance_UI_Panels_Margin_Bottom {
		get => panelsMargin[PanelsMarginEnum.Bottom];
		set {
			panelsMargin[PanelsMarginEnum.Bottom] = value;
			PanelsMarginChanged?.Invoke(PanelsMarginEnum.Bottom, value);
		}
	}

	[Export]
	protected Godot.Collections.Dictionary<PanelsMarginEnum, int> panelsMargin { get; set; } = new() {
		{PanelsMarginEnum.Left,0},
		{PanelsMarginEnum.Right, 0},
		{PanelsMarginEnum.Top, 0},
		{PanelsMarginEnum.Bottom, 0}
	};
	#endregion Apperance UI

}
