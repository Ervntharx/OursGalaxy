using Godot;
namespace OursGalaxy.ResourceConfig;

internal static class TaskEventLib {
	public static bool Exists(TaskEventConfig.EventTypeEnum eventType, string nameCode) => ResourceLoader.Exists(GetResourcePath(eventType, nameCode));

	public static TaskEventConfig GetConfig(TaskEventData data) => GetConfig(data.EventType, data.NameCode);

	public static TaskEventConfig GetConfig(TaskEventConfig.EventTypeEnum eventType, string nameCode) => GetConfig<TaskEventConfig>(eventType, nameCode);

	public static T GetConfig<T>(TaskEventData data) where T : TaskEventConfig => GetConfig<T>(data.EventType, data.NameCode);

	public static T GetConfig<T>(TaskEventConfig.EventTypeEnum eventType, string nameCode) where T : TaskEventConfig {
		if (Exists(eventType, nameCode)) {
			return ResourceLoader.Load<T>(GetResourcePath(eventType, nameCode));
		}
		GD.PushError($"<TaskEventLib> Cant get config of {eventType}\t{nameCode}");
		return null!;
	}

	private static string GetResourcePath(TaskEventConfig.EventTypeEnum eventTypeEnum, string nameCode) => $"res://ResourceLib/TaskEvent/{eventTypeEnum.ToString()}/{nameCode}.tres";
}
