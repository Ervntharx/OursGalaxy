using System.Net.NetworkInformation;
using Godot;
namespace OursGalaxy;

/// <summary>
/// 设置数据的纯DTO类.行为模仿一点ConfigFile
/// </summary>
[GlobalClass]
internal partial class SettingsData : Resource {
	/// <summary>
	/// 取得值
	/// </summary>
	/// <param name="selection"></param>
	/// <param name="key"></param>
	/// <param name="defaultValue"></param>
	/// <returns></returns>
	public Variant GetValue(string selection, string key, Variant defaultValue) {
		if (!value.ContainsKey(selection)) {
			value.Add(selection, []);
		}
		if (!value[selection].ContainsKey(key)) {
			value[selection].Add(key, defaultValue);
		}
		return value[selection][key];
	}

	/// <summary>
	/// 设置值
	/// </summary>
	/// <param name="selection"></param>
	/// <param name="key"></param>
	/// <param name="value"></param>
	public void SetValue(string selection, string key, Variant value) => this.value[selection][key] = value;

	[Export]
	private Godot.Collections.Dictionary<string, Godot.Collections.Dictionary<string, Variant>> value { get; set; } = [];
}
