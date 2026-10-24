using System;
using Godot;
using Godot.Collections;
namespace OursGalaxy;

[GlobalClass]
internal partial class DateTime : Resource {
	[Export]
	protected Dictionary? Date { get; set; }

	public DateTime(Dictionary godotDateTime) {
		Date = godotDateTime;
		// 辅助检查字典
		_ = Time.GetUnixTimeFromDatetimeDict(Date);
	}

	public DateTime(long unixTime) {
		Date = Time.GetDatetimeDictFromUnixTime(unixTime);
	}

	public static DateTime Now() => new() { Date = Time.GetDatetimeDictFromSystem(true) };

	public long ToUnix() => Time.GetUnixTimeFromDatetimeDict(Date);

	public bool Over(DateTime dateTime) {
		return ToUnix() > dateTime.ToUnix();
	}

	public TimeSpan To(DateTime target) {
		return TimeSpan.FromSeconds(target.ToUnix() - ToUnix());
	}

	public override string ToString() {
		return Time.GetDatetimeStringFromDatetimeDict(Date, true);
	}

	// 不允许无参构造
	private DateTime() { }
}
