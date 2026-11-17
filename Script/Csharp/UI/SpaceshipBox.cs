using Godot;
using OursGalaxy.ResourceConfig;
using OursGalaxy.ViewModel;
using System;
namespace OursGalaxy.UI;

internal partial class SpaceshipBox : Button {
	public static SpaceshipBox Load() => (SpaceshipBox)ResourceLoader.Load<PackedScene>("res://Scene/SpaceshipBox.tscn").Instantiate();

	[Export] protected RichTextLabel NameLabel { get; set; } = null!;
	[Export] protected TextureRect Avater { get; set; } = null!;

	public SpaceshipData? Ship;

	/// <summary>
	/// 无点击行为的渲染
	/// </summary>
	/// <param name="data"></param>
	public void Render(SpaceshipData data) {
		Ship = data;
		var config = SpaceshipLib.GetConfig(data);
		NameLabel.Text = config.DisplayName;
	}

	/// <summary>
	/// 渲染,并向vm中注册选中
	/// </summary>
	/// <param name="vm"></param>
	/// <param name="ship"></param>
	public void Render(SpaceshipOverviewVM vm, SpaceshipData ship) {
		Render(ship);
		this.vm = vm;
		Pressed += OnPressed;
	}

	public override void _ExitTree() {
		Pressed -= OnPressed;
		base._ExitTree();
	}

	private void OnPressed() {
		if (vm is not null) {
			vm.SelectShip = Ship;
		}
	}

	private SpaceshipOverviewVM? vm;
}
