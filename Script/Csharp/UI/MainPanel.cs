using Godot;
using System;
using Godot.Collections;
using System.Collections.ObjectModel;
namespace OursGalaxy.UI;

internal partial class MainPanel : Control {
	public static MainPanel Load() => (MainPanel)ResourceLoader.Load<PackedScene>("res://Script/Csharp/UI/MainPanel.cs").Instantiate();

	[Export]
	protected Button? QuitButton { get; set; }
	[Export]
	protected MessageBoxPanel? MsgBoxesListPanel { get; set; }
	[Export]
	protected MessageViewerPanel? MsgViewPanel { get; set; }

	// top bar
	[ExportGroup("Top Bar Overview")]
	[Export]
	protected Button? PersonalArchiveButton { get; set; }
	[Export]
	protected Button? SettingButton { get; set; }
	[Export]
	protected Button? ActionPointsButton { get; set; }
	[Export]
	protected RichTextLabel? ActionPointsLabel { get; set; }

	// Resource overviews
	[ExportGroup("Resource Overview")]
	[Export]
	protected RichTextLabel? ShipsCountLabel { get; set; }
	[Export]
	protected RichTextLabel? EquipsCountLabel { get; set; }
	[Export]
	protected RichTextLabel? SolidsLabel { get; set; }
	[Export]
	protected RichTextLabel? LiquidLabel { get; set; }
	[Export]
	protected RichTextLabel? IndustrialProductsLabel { get; set; }

	[Export]
	protected RichTextLabel? HonorLabel { get; set; }

	// function panel
	[ExportGroup("Function Panel Overview")]
	[Export]
	protected TabContainer? FuncPanelTab { get; set; }
	[Export]
	protected Button? FleetsEditButton { get; set; }
	[Export]
	protected Button? MissionButton { get; set; }
	[Export]
	protected Button? AllianceButton { get; set; }
	[Export]
	protected Button? SimulatedCombatButton { get; set; }
	[Export]
	protected Button? HangerButton { get; set; }
	[Export]
	protected Button? FactoryButton { get; set; }
	[Export]
	protected Button? AssignedTaskButton { get; set; }
	[Export]
	protected Button? CombatButton { get; set; }


	// bottom panels
	[ExportGroup("Bottom Panel Overview")]
	[Export]
	protected Button? TechnologyButton { get; set; }
	[Export]
	protected Button? StoreButton { get; set; }
	[Export]
	protected Button? ConnectionButton { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		MsgViewPanel!.Quit += OnMsgViewPanelQUit;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private void OnMsgViewPanelQUit(MessageViewerPanel messageViewerPanel) {
		// TODO: Switch tabs
	}
}
