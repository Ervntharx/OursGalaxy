using System.Runtime.CompilerServices;
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
	[Export] private Button? MissionButton { get; set; }
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
		GlobalViewModel.BaseResourceUpdatedEvent += UpdateBaseResourceLabel;
		// action points
		UpdateActionPointsLabel();
		GlobalViewModel.ActionPointsChangedEvent += UpdateActionPointsLabel;
		// Repository statics
		UpdateRepositoryStatics(GlobalViewModel.MainRepository);
		GlobalViewModel.MainRepository.ElementsChangedEvent += UpdateRepositoryStatics;
	}

	public override void _EnterTree() {
		base._EnterTree();
		ProfileButton.Pressed += OnProfilePressed;
		ActionPointsButton.Pressed += OnActionPointPressed;
		MsgViewPanel.Quit += OnMsgViewPanelQuit;
		MsgBoxesListPanel!.OpenMessage += OnMsgOpen;
		SettingButton.Pressed += OnSettingsPressed;
		QuitButton.Pressed += OnQuitPressed;
		FleetsEditButton.Pressed += OnFleetsEditPressed;
		SimulatedCombatButton.Pressed += OnSimulateCombatPressed;
		AssignedTaskButton.Pressed += OnAssignedTaskPressed;
		FactoryButton.Pressed += OnFactoryPressed;
		CombatButton.Pressed += OnCombatPressed;
		HangerButton.Pressed += OnHangerPressed;
		StoreButton.Pressed += OnStorePressed;
		AllianceButton.Pressed += OnAlliancePressed;
		TechnologyButton.Pressed += OnTechnologyPressed;
	}

	public override void _ExitTree() {
		ProfileButton.Pressed -= OnProfilePressed;
		ActionPointsButton.Pressed -= OnActionPointPressed;
		MsgBoxesListPanel!.OpenMessage -= OnMsgOpen;
		MsgViewPanel!.Quit -= OnMsgViewPanelQuit;
		SettingButton.Pressed -= OnSettingsPressed;
		QuitButton.Pressed -= OnQuitPressed;
		FleetsEditButton.Pressed -= OnFleetsEditPressed;
		SimulatedCombatButton.Pressed -= OnSimulateCombatPressed;
		AssignedTaskButton.Pressed -= OnAssignedTaskPressed;
		FactoryButton.Pressed -= OnFactoryPressed;
		CombatButton.Pressed -= OnCombatPressed;
		HangerButton.Pressed -= OnHangerPressed;
		StoreButton.Pressed -= OnStorePressed;
		AllianceButton.Pressed -= OnAlliancePressed;
		TechnologyButton.Pressed -= OnTechnologyPressed;
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
		SolidsLabel.Text = $"[x] [color={(GlobalViewModel.Solids == GlobalViewModel.SolidsLimit ? "gold" : "white")}]{GlobalViewModel.Solids}[/color] [color=gold]/ {GlobalViewModel.SolidsLimit}[/color]";
		LiquidLabel.Text = $"[x] [color={(GlobalViewModel.Liquid == GlobalViewModel.LiquidLimit ? "gold" : "white")}]{GlobalViewModel.Liquid}[/color] [color=gold]/ {GlobalViewModel.LiquidLimit}[/color]";
		IndustrialProductsLabel.Text = $"[x] [color={(GlobalViewModel.IndustrialProducts == GlobalViewModel.IndustralProductsLimit ? "gold" : "white")}]{GlobalViewModel.IndustrialProducts}[/color] [color=gold]/ {GlobalViewModel.IndustralProductsLimit}[/color]";
		HonorLabel.Text = $"[x] [color={(GlobalViewModel.Honors == GlobalViewModel.HonorsLimit ? "gold" : "white")}]{GlobalViewModel.Honors}[/color] [color=gold]/ {GlobalViewModel.HonorsLimit}[/color]";
	}

	private void UpdateActionPointsLabel() {
		ActionPointsLabel.Text = $"[color={(GlobalViewModel.ActionPoints == GlobalViewModel.ActionPointsLimit ? "gold" : "white")}]{GlobalViewModel.ActionPoints}[/color] [color=gold]/ {GlobalViewModel.ActionPointsLimit}[/color]";
	}
}
