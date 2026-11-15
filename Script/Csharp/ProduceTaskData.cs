using System;
using Godot;
namespace OursGalaxy;

[GlobalClass]
internal partial class ProduceTaskData : Resource {
	[Export] public DateTime? StartDate { get; set; }
	[Export] public int ProgressSeconds { get; set; }

	public bool IsEmpty => StartDate is null;

	public DateTime? EndDate() {
		return StartDate is not null ? new(StartDate.ToUnix() + ProgressSeconds) : null;
	}

	public override string ToString() {
		return $"ProduceTask:{{\n\t\"Start\":\t{StartDate},\n\t\"ProgressSeconds\":\t{ProgressSeconds},\n\t\"EndDate\":\t{EndDate()}\n}}";
	}
}
