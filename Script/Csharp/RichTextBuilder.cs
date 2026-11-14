using System.ComponentModel;
using System.Text;
using Godot;
namespace OursGalaxy;

/// <summary>
/// RichText构造工具
/// </summary>
internal class RichTextBuilder {
	public RichTextBuilder(string? itemSpliter = null) {
		raw = new();
		this.itemSpliter = itemSpliter ?? "\t";
	}

	public RichTextBuilder Add(string key, string value, string? valueSpliter = null) {
		raw.Append($"[color=gold]{key}[/color] {valueSpliter} [color=white]{value}[/color]");
		raw.Append(itemSpliter);
		return this;
	}

	public override string ToString() => raw.ToString();

	private StringBuilder raw;
	private string itemSpliter;
}
