using Godot;
using OursGalaxy.ResourceConfig;
using System;
using System.Diagnostics.Tracing;
namespace OursGalaxy.UI;

/// <summary>
/// 选择装备用的EquipBox
/// </summary>
internal partial class PickEquipBox : Button {
	public static PickEquipBox Load() => (PickEquipBox)ResourceLoader.Load<PackedScene>("res://Scene/PickEquipBox.tscn").Instantiate();

	[Export] private TextureRect avaterRect { get; set; } = null!;
	[Export] private RichTextLabel nameLabel { get; set; } = null!;
	[Export] private Button pickButton { get; set; } = null!;

	public EquipData? Equip { get; private set; }

	public void Render(EquipData data, ShipConfigViewModel VM) {
		var config = ResourceLib.GetConfig<EquipConfig>(data);
		avaterRect.Texture = config.Texture;
		nameLabel.Text = config.DisplayName;

		Equip = data;
		vm = VM;
	}

	public override void _EnterTree() {
		base._EnterTree();
		pickButton.Pressed += OnPickPressed;
	}

	public override void _ExitTree() {
		pickButton.Pressed -= OnPickPressed;
		base._ExitTree();
	}

	private void OnPickPressed() {
		if (vm is not null && Equip is not null) {
			vm.Install(Equip);
		}
		else {
			GD.PushError("<PickEquipBox> null reference");
		}
	}

	private ShipConfigViewModel? vm;
}
