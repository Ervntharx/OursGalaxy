using Godot;
using System;
namespace OursGalaxy.UI;

internal partial class FleetEditorPanel : MarginContainer {
	public static FleetEditorPanel Load() => (FleetEditorPanel)ResourceLoader.Load<PackedScene>("res://Scene/FleetEditorPanel.tscn").Instantiate();

	[Export] private Button QuitButton { get; set; } = null!;

	[Export] private ConfirmationDialog SaveDialog { get; set; } = null!;

	[Export] private LineEdit FleetNameEdit { get; set; } = null!;

	[Export] private Button ChangeFlagShip { get; set; } = null!;

	[Export] private Button AddShip { get; set; } = null!;

	[Export] private VBoxContainer? ShipsOverviewsPanel { get; set; }

	[Export] private RichTextLabel ShipsCountLabel { get; set; } = null!;

	[Export] private RichTextLabel FleetSizeLabel { get; set; } = null!;

	[Export] private RichTextLabel FleetRaidLabel { get; set; } = null!;

	[Export] private Node3D CameraArm { get; set; } = null!;
	[Export] private Camera3D Camera { get; set; } = null!;

	[Export] private SubViewport FleetPreview { get; set; } = null!;
	[Export] private Node3D FleetStage { get; set; } = null!;

	public FleetEditorViewModel VM { get; private set; } = new();

	public void Render(FleetData fleetData) {
		VM.Read(fleetData);
		FleetNameEdit.Text = fleetData.Name;
	}

	public override void _Input(InputEvent @event) {
		FleetPreview.PushInput(@event);
	}

	private void UpdateFlagOverviews() {
		if (editingFleetData is not null) {
			ShipsCountLabel.Text = $"[color=gold]舰船数量 [/color]{VM.ShipsCount} [color=gold]/ 20[/color]";
			FleetSizeLabel.Text = $"[color=gold]舰队规模 [/color]{VM.FleetSize} [color=gold]/ 300[/color]";
			FleetRaidLabel.Text = $"[color=gold]舰队奇袭 [/color] {VM.FleetRaid}";
		}
	}

	private void OnChangeFlagship() {

	}

	private void OnAddShip() {

	}

	// Called when the node enters the scene tree for the first time.
	public override void _EnterTree() {
		base._EnterTree();
		QuitButton.Pressed += () => PanelsStack.PopPanel(this);
		ChangeFlagShip.Pressed += OnChangeFlagship;
		AddShip.Pressed += OnAddShip;
		VM.Load(this);
	}

	public override void _ExitTree() {
		ChangeFlagShip.Pressed -= OnChangeFlagship;
		AddShip.Pressed -= OnAddShip;
		VM.Unload();
		base._ExitTree();
	}

	private FleetData? editingFleetData;
}
