using Godot;
namespace OursGalaxy.UI;

internal partial class MainPanel : Control {
	public static MainPanel Load() => (MainPanel)ResourceLoader.Load<PackedScene>("res://Scene/MainPanel.tscn").Instantiate();

	[Export]
	protected Button QuitButton { get; set; } = null!;
	[Export]
	protected MessageBoxPanel MsgBoxesListPanel { get; set; } = null!;
	[Export]
	protected MessageViewerPanel MsgViewPanel { get; set; } = null!;

	// top bar
	[ExportGroup("Top Bar Overview")]
	[Export]
	protected Button? PersonalArchiveButton { get; set; }
	[Export]
	protected Button SettingButton { get; set; } = null!;
	[Export]
	protected Button? ActionPointsButton { get; set; }
	[Export]
	protected RichTextLabel ActionPointsLabel { get; set; } = null!;

	// Resource overviews
	[ExportGroup("Resource Overview")]
	[Export]
	protected RichTextLabel? ShipsCountLabel { get; set; }
	[Export]
	protected RichTextLabel? EquipsCountLabel { get; set; }
	[Export]
	protected RichTextLabel SolidsLabel { get; set; } = null!;
	[Export]
	protected RichTextLabel LiquidLabel { get; set; } = null!;
	[Export]
	protected RichTextLabel IndustrialProductsLabel { get; set; } = null!;

	[Export]
	protected RichTextLabel HonorLabel { get; set; } = null!;

	// function panel
	[ExportGroup("Function Panel Overview")]
	[Export]
	protected TabContainer FuncPanelTab { get; set; } = null!;
	[Export]
	protected Button FleetsEditButton { get; set; } = null!;
	[Export]
	protected Button? MissionButton { get; set; }
	[Export]
	protected Button AllianceButton { get; set; } = null!;
	[Export]
	protected Button SimulatedCombatButton { get; set; } = null!;
	[Export]
	protected Button HangerButton { get; set; } = null!;
	[Export]
	protected Button FactoryButton { get; set; } = null!;
	[Export]
	protected Button AssignedTaskButton { get; set; } = null!;
	[Export]
	protected Button CombatButton { get; set; } = null!;


	// bottom panels
	[ExportGroup("Bottom Panel Overview")]
	[Export]
	protected Button TechnologyButton { get; set; } = null!;
	[Export]
	protected Button StoreButton { get; set; } = null!;
	[Export]
	protected Button? ConnectionButton { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		// UI init
		MsgViewPanel!.Quit += OnMsgViewPanelQuit;
		MsgBoxesListPanel!.OpenMessage += OnMsgOpen;

		SettingButton.Pressed += () => PanelsStack.PushPanel(SettingPanel.Load());
		QuitButton.Pressed += PanelsStack.RequireQuitGame;
		FleetsEditButton.Pressed += () => PanelsStack.PushPanel(FleetEditorPanel.Load());
		SimulatedCombatButton.Pressed += () => PanelsStack.PushPanel(ReadyCombatPanel.Load());
		AssignedTaskButton.Pressed += () => PanelsStack.PushPanel(AssignedTaskPanel.Load());
		FactoryButton.Pressed += () => PanelsStack.PushPanel(FactoryPanel.Load());
		CombatButton.Pressed += () => PanelsStack.PushPanel(CombatsOverviewPanel.Load());
		HangerButton.Pressed += () => PanelsStack.PushPanel(ItemViewerPanel.Load());
		StoreButton.Pressed += () => PanelsStack.PushPanel(ItemViewerPanel.Load());
		AllianceButton.Pressed += () => PanelsStack.PushPanel(AlliancePanel.Load());

		TechnologyButton.Pressed += () => PanelsStack.PushPanel(TechnologyPanel.Load());

		// base resource relatived to archive data
		// init base resource label
		UpdateBaseResourceLabel(Archive.BaseResourceEnum.Solids);
		UpdateBaseResourceLabel(Archive.BaseResourceEnum.Liquid);
		UpdateBaseResourceLabel(Archive.BaseResourceEnum.IndustralProducts);
		UpdateBaseResourceLabel(Archive.BaseResourceEnum.Honors);
		Archive.BaseResourceChangedEvent += UpdateBaseResourceLabel;
		Archive.BaseResourceLimitChangedEvent += UpdateBaseResourceLabel;

		//action points
		UpdateActionPointsLabel();
	}

	public override void _ExitTree() {
		// MsgBoxesListPanel!.OpenMessage -= OnMsgOpen;
		// MsgViewPanel!.Quit -= OnMsgViewPanelQuit;
		base._ExitTree();
	}

	private void OnMsgOpen(MessageData msg) {
		MsgViewPanel.Make(msg);
		var msgViewerIdx = FuncPanelTab!.GetTabIdxFromControl(MsgViewPanel);
		FuncPanelTab.SetTabHidden(msgViewerIdx, false);
		FuncPanelTab.CurrentTab = msgViewerIdx;
		FuncPanelTab.TabsVisible = true;
	}

	private void OnMsgViewPanelQuit(MessageViewerPanel messageViewerPanel) {
		FuncPanelTab.SetTabHidden(FuncPanelTab.GetTabIdxFromControl(messageViewerPanel), true);
		FuncPanelTab.TabsVisible = false;
	}

	private void UpdateBaseResourceLabel(Archive.BaseResourceEnum _) {
		SolidsLabel.Text = $"[x] [color={(Archive.Data.Solids == Archive.Data.SolidsLimit ? "gold" : "white")}]{Archive.Data.Solids}[/color] [color=gold]/ {Archive.Data.SolidsLimit}[/color]";
		LiquidLabel.Text = $"[x] [color={(Archive.Data.Liquid == Archive.Data.LiquidLimit ? "gold" : "white")}]{Archive.Data.Liquid}[/color] [color=gold]/ {Archive.Data.LiquidLimit}[/color]";
		IndustrialProductsLabel.Text = $"[x] [color={(Archive.Data.IndustrialProducts == Archive.Data.IndustralProductsLimit ? "gold" : "white")}]{Archive.Data.IndustrialProducts}[/color] [color=gold]/ {Archive.Data.IndustralProductsLimit}[/color]";
		HonorLabel.Text = $"[x] [color={(Archive.Data.Honors == Archive.Data.HonorsLimit ? "gold" : "white")}]{Archive.Data.Honors}[/color] [color=gold]/ {Archive.Data.HonorsLimit}[/color]";
	}

	private void UpdateActionPointsLabel() {
		ActionPointsLabel.Text = $"[color={(Archive.Data.ActionPoints == Archive.Data.ActionPointsLimit ? "gold" : "white")}]{Archive.Data.ActionPoints}[/color] [color=gold]/ {Archive.Data.ActionPointsLimit}[/color]";
	}
}
