using Godot;
using System;
namespace OursGalaxy.UI;

/// <summary>
/// 单个飞船的编辑UI.该UI设计用于快速操作,编辑操作内嵌代理,包括编辑自定义名称,设置旗舰,装配方案,移出舰队,战术设置
/// </summary>
internal partial class ShipEditorBox : Button {
	[Export] private Label FlagSignLabel { get; set; } = null!;
	[Export] private LineEdit ShipNameEditor { get; set; } = null!;
	[Export] private RichTextLabel ShipTypeNameLabel { get; set; } = null!;

	[Export] private Button SetAsFlagShip { get; set; } = null!;
	[Export] private Button EditAssemble { get; set; } = null!;
	[Export] private Button Remove { get; set; } = null!;

	//战术设置内容
	[Export] private OptionButton TacticsModeOpt { get; set; } = null!;
	[Export] private OptionButton RoundabotDirOpt { get; set; } = null!;
	[Export] private OptionButton ProtectedOpt { get; set; } = null!;
	[Export] private OptionButton TargetingOpt { get; set; } = null!;
	[Export] private OptionButton VilliangeOpt { get; set; } = null!;

	[Export] private HBoxContainer RoundabotLine { get; set; } = null!;
	[Export] private HBoxContainer ProtectLine { get; set; } = null!;

	public void Make(FleetData fleet, SpaceshipReference shipRefer) {
		var ship = fleet.LocalRepository.GetSpaceshipData(shipRefer);
		FlagSignLabel.Text = shipRefer == fleet.Flagship ? "⭐" : string.Empty;
		Remove.Disabled = shipRefer == fleet.Flagship;
		ShipNameEditor.Text = ship.CustomName;
		ShipTypeNameLabel.Text = ship.GetShipTypeName();

		ShipNameEditor.TextSubmitted += (newName) => {
			var shipData = fleet.LocalRepository.GetSpaceshipData(shipRefer);
			shipData.CustomName = newName;
		};

		SetAsFlagShip.Pressed += () => {
			fleet.Flagship = shipRefer;
			Remove.Disabled = true;
		};

		EditAssemble.Pressed += () => {
			var configPanel = ShipConfigPanel.Load();
			PanelsStack.PushPanel(configPanel);
			configPanel.Render(fleet, shipRefer);
		};

		Remove.Pressed += () => {
			// 先移除Reference,后从Fleet的LocalRepository移动到游戏全局MainRepository
			fleet.Ships.Remove(shipRefer);
			var data = fleet.LocalRepository.GetSpaceshipData(shipRefer);
			data.RecycleReference(shipRefer);
			RepositoryMap.Move(data, GlobalViewModel.MainRepository.ID);
		};

		var tactics = fleet.Ships[shipRefer];

		TacticsModeOpt.ItemSelected += (idx) => {
			tactics.Mode = (TacticsSet.ModeEnum)idx;
			switch (tactics.Mode) {
				case TacticsSet.ModeEnum.Forward:
					RoundabotLine.Visible = false;
					ProtectLine.Visible = false;
					break;
				case TacticsSet.ModeEnum.Roundabot:
					RoundabotLine.Visible = true;
					ProtectLine.Visible = false;
					break;
				case TacticsSet.ModeEnum.Protect:
					RoundabotLine.Visible = false;
					ProtectLine.Visible = true;
					break;
				default:
					break;
			}
		};
	}
}
