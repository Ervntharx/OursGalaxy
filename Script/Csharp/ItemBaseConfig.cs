using Godot;
namespace OursGalaxy.ResourceConfig;

/// <summary>
/// ItemBase的配置项项目
/// </summary>
[GlobalClass]
internal partial class ItemBaseConfig : Resource {
	public enum QulityEnum { White, Green, Purple, Gold }

	/// <summary>
	/// ItemBase的物品类型Code
	/// </summary>
	[Export] public string Code { get; set; } = TEMPLATE_CODE;

	[Export]
	public QulityEnum Qulity {
		get {
			if (int.TryParse(Code.Split("_", false)[2], out var result)) {
				return (QulityEnum)result;
			}
			GD.PushError("Invalid Code");
			return QulityEnum.White;
		}
		set {
			var codeLs = Code.Split("_", false);
			codeLs[2] = ((int)value).ToString();
			Code = string.Join("_", codeLs);
		}
	}

	[Export] public string DisplayName { get; set; } = string.Empty;

	/// <summary>
	/// 简介
	/// </summary>
	[Export] public string Introductuon { get; set; } = string.Empty;

	private const string TEMPLATE_CODE = "Template";
}
