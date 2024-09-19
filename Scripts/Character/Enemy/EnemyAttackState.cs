using demo.Scripts.General;
using Godot;

namespace demo.Scripts.Character.Enemy;

public partial class EnemyAttackState : EnemyState
{
	protected override void EnterState()
	{
		CharacterNode.AnimPlayerNode.Play(GameConstants.AnimAttack);
		CharacterNode.AttackAreaNode.BodyExited += HandleAttackAreaNodeExited;
	}

	protected override void ExitState()
	{
		CharacterNode.AttackAreaNode.BodyExited -= HandleAttackAreaNodeExited;
	}

	private void HandleAttackAreaNodeExited(Node3D body)
	{
		CharacterNode.StateMachineNode.SwitchState<EnemyChaseState>();
	}
}