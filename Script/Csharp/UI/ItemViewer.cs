using Godot;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
namespace OursGalaxy.UI;

internal partial class ItemViewer : Window {
	[Export]
	protected HFlowContainer? ItemsPanel { get; set; }

	public ObserveItemCollection Items {
		get => items; set {
			items = value;
			Update(true);
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {

	}

	/// <summary>
	/// 更新UI.
	/// </summary>
	/// <param name="hard">硬重绘，true时会强行删除所有结果重新计算</param>
	public void Update(bool hard = false) {

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	private ObserveItemCollection items = [];
}
