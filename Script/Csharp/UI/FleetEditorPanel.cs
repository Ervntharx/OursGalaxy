using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class FleetEditorPanel : MarginContainer {
	public static FleetEditorPanel Load() => (FleetEditorPanel)ResourceLoader.Load<PackedScene>("res://Scene/FleetEditorPanel.tscn").Instantiate();

	[Export] protected Button QuitButton { get; set; } = null!;

	[Export] protected ConfirmationDialog SaveDialog { get; set; } = null!;

	[Export] protected LineEdit FleetNameEdit { get; set; } = null!;

	[Export] protected Button ChangeFlagShip { get; set; } = null!;

	[Export] protected Button AddShip { get; set; } = null!;

	[Export] protected VBoxContainer? ShipsOverviewsPanel { get; set; }

	[Export] protected RichTextLabel ShipsCountLabel { get; set; } = null!;

	[Export] protected RichTextLabel FleetSizeLabel { get; set; } = null!;

	[Export] protected RichTextLabel FleetRaidLabel { get; set; } = null!;

	[Export] protected Node3D CameraArm { get; set; } = null!;
	[Export] protected Camera3D Camera { get; set; } = null!;

	[Export] protected SubViewport FleetPreview { get; set; } = null!;
	[Export] protected Node3D FleetStage { get; set; } = null!;


	public void Make(FleetData fleetData) {
		FleetNameEdit.Text = fleetData.Name;

		var flagship = SpaceshipFactory.Make(fleetData.Flagship, fleetData.LocalRepository);
		FleetStage.AddChild(flagship);

		foreach (var pair in fleetData.Ships) {
			var memShip = SpaceshipFactory.Make(pair.Key, fleetData.LocalRepository);
			memShip.Position = pair.Value.OffsetPosition;
			FleetStage.AddChild(memShip);
		}
	}

	public void Save() {
		hasEdit = false;
	}

	public override void _Input(InputEvent @event) {
		FleetPreview.PushInput(@event);
	}

	private void UpdateFlagOverviews() {
		if (editingFleetData is not null) {
			ShipsCountLabel.Text = $"[color=gold]舰船数量 [/color]{editingFleetData.ShipsCount} [color=gold]/ 20[/color]";
			FleetSizeLabel.Text = $"[color=gold]舰队规模 [/color]{editingFleetData.Size} [color=gold]/ 300[/color]";
			FleetRaidLabel.Text = $"[color=gold]舰队奇袭 [/color] {editingFleetData.Raid}";
		}
	}

	private void OnQuit() {
		if (hasEdit) {
			SaveDialog.PopupCentered();
		}
		else {
			PanelsStack.PopPanel(this);
		}
	}

	private void OnChangeFlagship() {

	}

	private void OnAddShip() {

	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		QuitButton.Pressed += OnQuit;
		ChangeFlagShip.Pressed += OnChangeFlagship;
		AddShip.Pressed += OnAddShip;
	}

	private FleetData? editingFleetData;

	private bool hasEdit = false;
}
