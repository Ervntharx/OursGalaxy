using Godot;
using System;
/// <summary>
/// Represents a time span in the game. instead of TimeSpan, GameSpan is target to store in the resource file, and can be exported to the editor.
/// </summary>
[GlobalClass]
internal partial class GameTimeSpan : Resource {
	public GameTimeSpan() { }// Default constructor}

	public GameTimeSpan(TimeSpan timeSpan) {
		// Constructor that takes a TimeSpan
		TotalSeconds = (ulong)timeSpan.TotalSeconds;
	}

	[Export]
	public ulong TotalSeconds { get; set; } = 1;

	[Export]
	public ulong Hours {
		get => TotalSeconds / 3600;
		set => TotalSeconds = value * 3600 + Minutes * 60 + Seconds;
	}

	[Export]
	public ulong Minutes {
		get => (TotalSeconds % 3600) / 60;
		set => TotalSeconds = Hours * 3600 + value * 60 + Seconds;
	}

	[Export]
	public ulong Seconds {
		get => TotalSeconds % 60;
		set => TotalSeconds = Hours * 3600 + Minutes * 60 + value;
	}

	public TimeSpan ToTimeSpan() {
		return TimeSpan.FromSeconds(TotalSeconds);
	}

	public override string ToString() {
		return ToTimeSpan().ToString();
	}
}
