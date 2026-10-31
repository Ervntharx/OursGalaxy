using Godot;
namespace OursGalaxy;

/// <summary>
/// 炮台基
/// </summary>
internal partial class TurretBase : Node3D {
	public const ulong DEFAULT_CDMISC = 3000;
	public const float DEFAULT_ROTATE_ANGLE_VELOCITY = 45;

	/// <summary>
	/// 尺寸
	/// </summary>
	public OutletSizeType SizeType { get; protected set; } = new(OutletSizeType.SizeTypeEnum.S);

	/// <summary>
	/// 炮台状态机枚举
	/// </summary>
	public enum StateEnum { IsReady, OnFire, CoolDown }
	public StateEnum State { get; set; } = StateEnum.IsReady;

	/// <summary>
	/// 驱动状态机。由Turret宿主驱动
	/// </summary>
	/// <param name="delta"></param>
	/// <param name="from">Turret宿主</param>
	/// <param name="battleEntity"></param>
	public virtual void DriveState(double delta, IBattleEntity from, IBattleEntity? battleEntity) {
		switch (State) {
			case TurretBase.StateEnum.IsReady:
				if (GodotObject.IsInstanceValid(battleEntity?.AsNode3D()) && IsEntityInRange(battleEntity)) {
					State = TurretBase.StateEnum.OnFire;
				}
				break;
			case TurretBase.StateEnum.OnFire:
				// 已打出所有伤害段，进入冷却
				if (HasMadeDamageCount >= MakeDamageCount) {
					State = TurretBase.StateEnum.CoolDown;
					break;
				}
				// 继续打出伤害段
				if (Time.GetTicksMsec() - LastMakeDamageTime >= MakeDamageTimeGapMisc) {
					BulletServer.Add(from, LoadBullet(), GetShootTransform(), GetShootVelocity());
					LastMakeDamageTime = Time.GetTicksMsec();
					HasMadeDamageCount += 1;
					break;
				}
				break;
			case TurretBase.StateEnum.CoolDown:
				if (Time.GetTicksMsec() - EnterCDTime >= CDMisc) {
					State = TurretBase.StateEnum.IsReady;
				}
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// 射程
	/// </summary>
	public virtual float Range { get; set; }

	public float LeftClampAngle { get; set; }
	public float RightClampAngle { get; set; }

	/// <summary>
	/// 是否可旋转。按是否存在旋转构件判断
	/// </summary>
	public bool IsRotatble => RotatableNode is not null;

	/// <summary>
	/// 旋转构件
	/// </summary>
	[Export]
	public Node3D? RotatableNode { get; set; }

	/// <summary>
	/// 旋转角速度
	/// </summary>
	public float RotateAngleVelocity { get; set; } = DEFAULT_ROTATE_ANGLE_VELOCITY;

	/// <summary>
	/// 瞄准判定区域。以目标是否在此范围内为标准判定开不开火
	/// </summary>
	[Export]
	protected Area3D DamageArea { get; set; } = null!;

	/// <summary>
	/// 目标是否在可击范围内
	/// </summary>
	/// <param name="battleEntity"></param>
	/// <returns></returns>
	public bool IsEntityInRange(IBattleEntity battleEntity) => DamageArea.OverlapsArea(battleEntity.AttackableArea);

	/// <summary>
	/// 多段伤害段时间间隙
	/// </summary>
	public ulong MakeDamageTimeGapMisc { get; set; } = 200;

	/// <summary>
	/// 总伤害段数。对应激光的连续伤害判定次数和射弹武器的连续射击次数
	/// </summary>
	public int MakeDamageCount { get; set; } = 1;

	/// <summary>
	/// 已造成伤害段数
	/// </summary>
	public int HasMadeDamageCount { get; set; }

	/// <summary>
	/// 上次造成伤害时间
	/// </summary>
	public ulong LastMakeDamageTime { get; set; }

	/// <summary>
	/// 默认伤害
	/// </summary>
	public DamageSet Damage { get; set; }

	/// <summary>
	/// 子弹出膛速度标量
	/// </summary>
	protected float BulletInitVelocity { get; set; }

	/// <summary>
	/// 实例化子弹资源，并已初始化伤害
	/// </summary>
	/// <returns></returns>
	protected virtual BulletBase LoadBullet() {
		var bullet = BulletBase.Load();
		bullet.Damage = Damage;
		return bullet;
	}

	/// <summary>
	/// 获得初始射击时子弹的变换
	/// </summary>
	/// <returns></returns>
	protected virtual Transform3D GetShootTransform() => new(RotatableNode?.GlobalBasis ?? GlobalBasis, DamageArea.GlobalTransform.Origin);

	/// <summary>
	/// 得到设计子弹出膛速度矢量
	/// </summary>
	/// <returns></returns>
	protected virtual Vector3 GetShootVelocity() => GetShootTransform().Basis.Z * BulletInitVelocity;

	/// <summary>
	/// 冷却时间
	/// </summary>
	protected ulong CDMisc { get; set; } = DEFAULT_CDMISC;

	/// <summary>
	/// 进入冷却的时间
	/// </summary>
	protected ulong EnterCDTime { get; set; }
}
