public interface IPromote {
	public string ItemName { get; }
	public enum PromoteFormEnum { Rate, Value }
	public PromoteFormEnum PromoteForm { get; }
	public double Value { get; }
}
