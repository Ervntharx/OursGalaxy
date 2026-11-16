using Godot;
namespace OursGalaxy.UI;

internal sealed partial class MainPanel : MarginContainer {
	public static MainPanel Load() => (MainPanel)ResourceLoader.Load<PackedScene>("res://Scene/MainPanel.tscn").Instantiate();

	[Export] private Button QuitButton { get; set; } = null!;
	[Export] private MessageBoxPanel MsgBoxesListPanel { get; set; } = null!;
	[Export] private MessageViewerPanel MsgViewPanel { get; set; } = null!;

	// top bar
	[ExportGroup("Top Bar Overview")]
	[Export] private Button ProfileButton { get; set; } = null!;
	[Export] private Button SettingButton { get; set; } = null!;
	[Export] private Button ActionPointsButton { get; set; } = null!;
	[Export] private RichTextLabel ActionPointsLabel { get; set; } = null!;

	// Resource overviews
	[ExportGroup("Resource Overview")]
	[Export] private RichTextLabel ShipsCountLabel { get; set; } = null!;
	[Export] private RichTextLabel EquipsCountLabel { get; set; } = null!;
	[Export] private RichTextLabel SolidsLabel { get; set; } = null!;
	[Export] private RichTextLabel LiquidLabel { get; set; } = null!;
	[Export]
	private RichTextLabel IndustrialProductsLabel { get; set; } = null!;

	[Export] private RichTextLabel HonorLabel { get; set; } = null!;

	// function panel
	[ExportGroup("Function Panel Overview")]
	[Export] private TabContainer FuncPanelTab { get; set; } = null!;
	[Export] private Button FleetsEditButton { get; set; } = null!;
	[Export] private Button MissionButton { get; set; } = null!;
	[Export] private Button AllianceButton { get; set; } = null!;
	[Export] private Button SimulatedCombatButton { get; set; } = null!;
	[Export] private Button HangerButton { get; set; } = null!;
	[Export] private Button FactoryButton { get; set; } = null!;
	[Export] private Button AssignedTaskButton { get; set; } = null!;
	[Export] private Button CombatButton { get; set; } = null!;


	// bottom panels
	[ExportGroup("Bottom Panel Overview")]
	[Export] private Button TechnologyButton { get; set; } = null!;
	[Export] private Button StoreButton { get; set; } = null!;
	[Export] private Button? ConnectionButton { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		// UI init


		// base resource relatived to archive data
		// init base resource label
		UpdateBaseResourceLabel();
		GlobalDataSource.BaseResourceUpdatedEvent += UpdateBaseResourceLabel;
		// action points
		UpdateActionPointsLabel();
		GlobalDataSource.ActionPointsChangedEvent += UpdateActionPointsLabel;
		// Repository statics
		UpdateRepositoryStatics(GlobalDataSource.MainRepository);
		GlobalDataSource.MainRepository.ElementsChangedEvent += UpdateRepositoryStatics;
	}

	public override void _EnterTree() {
		base._EnterTree();
		// topbar 
		ProfileButton.Pressed += OnProfilePressed;
		ActionPointsButton.Pressed += OnActionPointPressed;
		SettingButton.Pressed += OnSettingsPressed;
		// func panel
		MissionButton.Pressed += OnMissionPressed;
		AllianceButton.Pressed += OnAlliancePressed;
		FleetsEditButton.Pressed += OnFleetsEditPressed;
		SimulatedCombatButton.Pressed += OnSimulateCombatPressed;
		AssignedTaskButton.Pressed += OnAssignedTaskPressed;
		FactoryButton.Pressed += OnFactoryPressed;
		CombatButton.Pressed += OnCombatPressed;
		HangerButton.Pressed += OnHangerPressed;
		// bottom bar
		TechnologyButton.Pressed += OnTechnologyPressed;
		StoreButton.Pressed += OnStorePressed;
		QuitButton.Pressed += OnQuitPressed;
		// msg panel
		MsgViewPanel.Quit += OnMsgViewPanelQuit;
		MsgBoxesListPanel!.OpenMessage += OnMsgOpen;
	}

	public override void _ExitTree() {
		// topbar
		ProfileButton.Pressed -= OnProfilePressed;
		ActionPointsButton.Pressed -= OnActionPointPressed;
		SettingButton.Pressed -= OnSettingsPressed;
		// func panel
		MissionButton.Pressed -= OnMissionPressed;
		AllianceButton.Pressed -= OnAlliancePressed;
		FleetsEditButton.Pressed -= OnFleetsEditPressed;
		SimulatedCombatButton.Pressed -= OnSimulateCombatPressed;
		AssignedTaskButton.Pressed -= OnAssignedTaskPressed;
		FactoryButton.Pressed -= OnFactoryPressed;
		CombatButton.Pressed -= OnCombatPressed;
		HangerButton.Pressed -= OnHangerPressed;
		// bottom panel
		TechnologyButton.Pressed -= OnTechnologyPressed;
		StoreButton.Pressed -= OnStorePressed;
		QuitButton.Pressed -= OnQuitPressed;
		// msg panel
		MsgBoxesListPanel!.OpenMessage -= OnMsgOpen;
		MsgViewPanel!.Quit -= OnMsgViewPanelQuit;
		base._ExitTree();
	}

	private void OnProfilePressed() {
		PanelsStack.PushPanel(ProfilePanel.Load());
	}

	private void OnActionPointPressed() {
		PanelsStack.PushPanel(ActionPointsPanel.Load());
	}

	private void OnMsgOpen(MessageData msg) {
		MsgViewPanel.Render(msg);
		var msgViewerIdx = FuncPanelTab!.GetTabIdxFromControl(MsgViewPanel);
		FuncPanelTab.SetTabHidden(msgViewerIdx, false);
		FuncPanelTab.CurrentTab = msgViewerIdx;
		// FuncPanelTab.TabsVisible = true;
	}

	private void OnMsgViewPanelQuit(MessageViewerPanel messageViewerPanel) {
		FuncPanelTab.SetTabHidden(FuncPanelTab.GetTabIdxFromControl(messageViewerPanel), true);
		// FuncPanelTab.TabsVisible = false;
	}

	private void OnSettingsPressed() => PanelsStack.PushPanel(SettingPanel.Load());
	private void OnQuitPressed() => PanelsStack.RequireQuitGame();
	private void OnFleetsEditPressed() {
		var viewer = FleetsViewerPanel.Load();
		viewer.Render();
		PanelsStack.PushPanel(viewer);
	}

	private void OnSimulateCombatPressed() {
		var readypanel = ReadyCombatPanel.Load();
		PanelsStack.PushPanel(readypanel);
	}

	private void OnAssignedTaskPressed() => PanelsStack.PushPanel(AssignedTaskPanel.Load());
	private void OnFactoryPressed() => PanelsStack.PushPanel(FactoryPanel.Load());
	private void OnMissionPressed() => PanelsStack.PushPanel(MissionPanel.Load());
	private void OnCombatPressed() => PanelsStack.PushPanel(CombatsOverviewPanel.Load());
	private void OnHangerPressed() => PanelsStack.PushPanel(HangerPanel.Load());
	private void OnStorePressed() => PanelsStack.PushPanel(Storehouse.Load());
	private void OnAlliancePressed() => PanelsStack.PushPanel(AlliancePanel.Load());
	private void OnTechnologyPressed() => PanelsStack.PushPanel(TechnologyPanel.Load());

	private void UpdateRepositoryStatics(SubRepository repository, UniqueItem? _ = null) {
		ShipsCountLabel.Text = $"[x] [color=white]{repository.Ships.Count}[/color] [color=gold]/ 2000[/color]";
		EquipsCountLabel.Text = $"[x] [color=white]{repository.Equips.Count}[/color] [color=gold]/ 2000[/color]";
	}

	private void UpdateBaseResourceLabel() {
		SolidsLabel.Text = $"[x] [color={(GlobalDataSource.Solids == GlobalDataSource.SolidsLimit ? "gold" : "white")}]{GlobalDataSource.Solids}[/color] [color=gold]/ {GlobalDataSource.SolidsLimit}[/color]";
		LiquidLabel.Text = $"[x] [color={(GlobalDataSource.Liquid == GlobalDataSource.LiquidLimit ? "gold" : "white")}]{GlobalDataSource.Liquid}[/color] [color=gold]/ {GlobalDataSource.LiquidLimit}[/color]";
		IndustrialProductsLabel.Text = $"[x] [color={(GlobalDataSource.IndustrialProducts == GlobalDataSource.IndustralProductsLimit ? "gold" : "white")}]{GlobalDataSource.IndustrialProducts}[/color] [color=gold]/ {GlobalDataSource.IndustralProductsLimit}[/color]";
		HonorLabel.Text = $"[x] [color={(GlobalDataSource.Honors == GlobalDataSource.HonorsLimit ? "gold" : "white")}]{GlobalDataSource.Honors}[/color] [color=gold]/ {GlobalDataSource.HonorsLimit}[/color]";
	}

	private void UpdateActionPointsLabel() {
		ActionPointsLabel.Text = $"[color={(GlobalDataSource.ActionPoints == GlobalDataSource.ActionPointsLimit ? "gold" : "white")}]{GlobalDataSource.ActionPoints}[/color] [color=gold]/ {GlobalDataSource.ActionPointsLimit}[/color]";
	}
}
