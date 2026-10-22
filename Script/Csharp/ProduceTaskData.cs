using System;
using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class ProduceTaskData : Resource {
	public static readonly ProduceTaskData Empty = new();
	[Export]
	public DateTime StartDate { get; set; }

	[Export]
	protected long ProgressSeconds { get; set; }
	public TimeSpan ProgressTimeSpan => TimeSpan.FromSeconds(ProgressSeconds);

	[Export]
	public ItemBase Target { get; set; }

	protected ProduceTaskData() {
		Target = ItemBase.Empty;
		StartDate = DateTime.Now();
		ProgressSeconds = 5;
	}

	public ProduceTaskData(DateTime startDate, TimeSpan progressTimeSpan, ItemBase target) {
		StartDate = startDate;
		ProgressSeconds = (long)progressTimeSpan.TotalSeconds;
		Target = target;
	}

	public ProduceTaskData(ProduceTaskData other) {
		StartDate = other.StartDate;
		ProgressSeconds = other.ProgressSeconds;
		Target = other.Target;
	}

	public DateTime EndDate() {
		return new(StartDate.ToUnix() + ProgressSeconds);
	}

	public override string ToString() {
		return $"ProduceTask:{{\n\t\"Start\":\t{StartDate},\n\t\"ProgressSeconds\":\t{ProgressSeconds},\n\t\"EndDate\":\t{EndDate()}\n}}";
	}
}
