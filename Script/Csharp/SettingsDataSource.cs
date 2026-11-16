using Godot;
using System;
namespace OursGalaxy;

/// <summary>
/// 设置配置数据源. 需要AutoLoad
/// </summary>
internal sealed partial class SettingsDataSource : Node {
	#region Camera
	public static event Action<float>? CameraDistanceMaxChanged;
	public static event Action<float>? CameraDistanceMinChanged;
	public static event Action<float>? CameraDefaultDistanceChanged;
	public static event Action<float>? CameraMoveVelocityChanged;
	public static event Action<float>? CameraDistanceChangeStepChanged;

	public static float Camera_Distance_Max {
		get => (float)config.GetValue("Camera", "Distance_Max").AsDouble();
		set {
			config.SetValue("Camera", "Distance_Max", value);
			CameraDistanceMaxChanged?.Invoke(Camera_Distance_Max);
		}
	}

	public static float Camera_Distance_Min {
		get => (float)config.GetValue("Camera", "Distance_Min").AsDouble();
		set {
			config.SetValue("Camera", "Distance_Min", value);
			CameraDistanceMinChanged?.Invoke(Camera_Distance_Min);
		}
	}

	public static float Camera_Default_Distance {
		get => (float)config.GetValue("Camera", "Distance_Default").AsDouble();
		set {
			config.SetValue("Camera", "Distance_Default", value);
			CameraDefaultDistanceChanged?.Invoke(Camera_Default_Distance);
		}
	}

	public static float Camera_Move_Velocity {
		get => (float)config.GetValue("Camera", "Move_Velocity").AsDouble();
		set {
			config.SetValue("Camera", "Move_Velocity", value);
			CameraMoveVelocityChanged?.Invoke(Camera_Move_Velocity);
		}
	}

	public static float Camera_Distance_ChangsStep {
		get => (float)config.GetValue("Camera", "Distance_Move_Velocity").AsDouble();
		set {
			config.SetValue("Camera", "Distance_Move_Velocity", value);
			CameraDistanceChangeStepChanged?.Invoke(Camera_Distance_ChangsStep);
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
		get => config.GetValue("UI", "Panels_Margin_Left").AsInt32();
		set {
			config.SetValue("UI", "Panels_Margin_Left", value);
			PanelsMarginChanged?.Invoke(PanelsMarginEnum.Left, value);
		}
	}

	/// <summary>
	/// 右边距
	/// </summary>
	public static int Apperance_UI_Panels_Margin_Right {
		get => config.GetValue("UI", "Panels_Margin_Right").AsInt32();
		set {
			config.SetValue("UI", "Panels_Margin_Right", value);
			PanelsMarginChanged?.Invoke(PanelsMarginEnum.Right, value);
		}
	}

	/// <summary>
	/// 上边距
	/// </summary>
	public static int Apperance_UI_Panels_Margin_Top {
		get => config.GetValue("UI", "Panels_Margin_Top").AsInt32();
		set {
			config.SetValue("UI", "Panels_Margin_Top", value);
			PanelsMarginChanged?.Invoke(PanelsMarginEnum.Top, value);
		}
	}

	/// <summary>
	/// 下边距
	/// </summary>
	public static int Apperance_UI_Panels_Margin_Bottom {
		get => config.GetValue("UI", "Panels_Margin_Bottom").AsInt32();
		set {
			config.SetValue("UI", "Panels_Margin_Bottom", value);
			PanelsMarginChanged?.Invoke(PanelsMarginEnum.Bottom, value);
		}
	}
	#endregion Apperance UI

	#region Sound
	public static bool SoundMainSwitch {
		get => config.GetValue("Sound", "MainSwitch").AsBool();
		set {
			config.SetValue("Sound", "MainSwitch", value);
			SoundMainToggledEvent?.Invoke(value);
		}
	}
	public static event Action<bool>? SoundMainToggledEvent;
	#endregion Sound

	public static void Save() {
		config.Save(SAVE_PATH);
	}

	public override void _EnterTree() {
		base._EnterTree();
		if (FileAccess.FileExists(SAVE_PATH)) {
			var err = config.Load(SAVE_PATH);
			if (err != Error.Ok) {
				GD.PushError(err);
			}
		}
		else {
			Save();
		}
	}

	public override void _ExitTree() {
		Save();
		base._ExitTree();
	}

	private static ConfigFile config = new();

	private const string SAVE_PATH = "user://settings.cfg";
}
