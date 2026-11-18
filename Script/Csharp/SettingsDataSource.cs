using Godot;
using System;
namespace OursGalaxy;

/// <summary>
/// 设置配置数据源. 需要AutoLoad
/// </summary>
internal sealed partial class SettingsDataSource : Node {
	public static SettingsDataSource Source { get; private set; } = new();

	#region Camera
	public static event Action<float>? CameraDistanceMaxChanged;
	public static event Action<float>? CameraDistanceMinChanged;
	public static event Action<float>? CameraDefaultDistanceChanged;
	public static event Action<float>? CameraMoveVelocityChanged;
	public static event Action<float>? CameraDistanceChangeStepChanged;

	public static float Camera_Distance_Max {
		get {
			return (float)Source.config.GetValue("Camera", "Distance_Max", 50).AsDouble();
		}

		set {
			Source.config.SetValue("Camera", "Distance_Max", value);
			CameraDistanceMaxChanged?.Invoke(Camera_Distance_Max);
			Save();
		}
	}

	public static float Camera_Distance_Min {
		get => (float)Source.config.GetValue("Camera", "Distance_Min", 2).AsDouble();
		set {
			Source.config.SetValue("Camera", "Distance_Min", value);
			CameraDistanceMinChanged?.Invoke(Camera_Distance_Min);
			Save();
		}
	}

	public static float Camera_Default_Distance {
		get => (float)Source.config.GetValue("Camera", "Distance_Default", 4).AsDouble();
		set {
			Source.config.SetValue("Camera", "Distance_Default", value);
			CameraDefaultDistanceChanged?.Invoke(Camera_Default_Distance);
			Save();
		}
	}

	public static float Camera_Move_Velocity {
		get => (float)Source.config.GetValue("Camera", "Move_Velocity", 1.2).AsDouble();
		set {
			Source.config.SetValue("Camera", "Move_Velocity", value);
			CameraMoveVelocityChanged?.Invoke(Camera_Move_Velocity);
			Save();
		}
	}

	public static float Camera_Distance_ChangsStep {
		get => (float)Source.config.GetValue("Camera", "Distance_Move_Velocity", 1).AsDouble();
		set {
			Source.config.SetValue("Camera", "Distance_Move_Velocity", value);
			CameraDistanceChangeStepChanged?.Invoke(Camera_Distance_ChangsStep);
			Save();
		}
	}
	#endregion Camera

	#region Apperance UI

	public enum PanelsMarginEnum { Left, Right, Top, Bottom }
	/// <summary>
	/// 界面边距变化时
	/// </summary>
	public static event Action<PanelsMarginEnum, int>? PanelsMarginChanged;

	/// <summary>
	/// 左边距
	/// </summary>
	public static int Apperance_UI_Panels_Margin_Left {
		get => Source.config.GetValue("UI", "Panels_Margin_Left", 5).AsInt32();
		set {
			Source.config.SetValue("UI", "Panels_Margin_Left", value);
			PanelsMarginChanged?.Invoke(PanelsMarginEnum.Left, value);
			Save();
		}
	}

	/// <summary>
	/// 右边距
	/// </summary>
	public static int Apperance_UI_Panels_Margin_Right {
		get => Source.config.GetValue("UI", "Panels_Margin_Right", 5).AsInt32();
		set {
			Source.config.SetValue("UI", "Panels_Margin_Right", value);
			PanelsMarginChanged?.Invoke(PanelsMarginEnum.Right, value);
			Save();
		}
	}

	/// <summary>
	/// 上边距
	/// </summary>
	public static int Apperance_UI_Panels_Margin_Top {
		get => Source.config.GetValue("UI", "Panels_Margin_Top", 5).AsInt32();
		set {
			Source.config.SetValue("UI", "Panels_Margin_Top", value);
			PanelsMarginChanged?.Invoke(PanelsMarginEnum.Top, value);
			Save();
		}
	}

	/// <summary>
	/// 下边距
	/// </summary>
	public static int Apperance_UI_Panels_Margin_Bottom {
		get => Source.config.GetValue("UI", "Panels_Margin_Bottom", 5).AsInt32();
		set {
			Source.config.SetValue("UI", "Panels_Margin_Bottom", value);
			PanelsMarginChanged?.Invoke(PanelsMarginEnum.Bottom, value);
			Save();
		}
	}
	#endregion Apperance UI

	#region Sound
	public static bool SoundMainSwitch {
		get => Source.config.GetValue("Sound", "MainSwitch", true).AsBool();
		set {
			Source.config.SetValue("Sound", "MainSwitch", value);
			SoundMainToggledEvent?.Invoke(value);
			Save();
		}
	}
	public static event Action<bool>? SoundMainToggledEvent;
	#endregion Sound

	#region IO
	public event Action? SavedEvent;

	public static void Save() {
		var err = ResourceSaver.Save(Source.config, SAVE_PATH);
		if (err != Error.Ok) {
			GD.PushError($"<SettingsDataSource> Save settings data failed with error: {err}");
			return;
		}
		GD.Print($"<SettingsDataSource> Save settings data at {Godot.ProjectSettings.GlobalizePath(SAVE_PATH)}");
		Source.SavedEvent?.Invoke();
	}

	public override void _EnterTree() {
		base._EnterTree();
		if (FileAccess.FileExists(SAVE_PATH)) {
			GD.Print($"<SettingsDataSource> Load settings data at {Godot.ProjectSettings.GlobalizePath(SAVE_PATH)}");
			config = ResourceLoader.Load<SettingsData>(SAVE_PATH);
		}
		else {
			GD.Print($"<SettingsDataSource> No settings data found at {Godot.ProjectSettings.GlobalizePath(SAVE_PATH)}, creating new one.");
			Save();
		}
	}

	public override void _ExitTree() {
		Save();
		base._ExitTree();
	}

	private const string SAVE_PATH = "user://settings.res";
	#endregion IO

	private SettingsData config = new();
}
