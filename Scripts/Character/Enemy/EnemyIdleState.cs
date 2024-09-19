using demo.Scripts.General;
using Godot;

namespace demo.Scripts.Character.Enemy;

public partial class EnemyIdleState : EnemyState
{
	protected override void EnterState()
	{
		CharacterNode.AnimPlayerNode.Play(GameConstants.AnimIdle);
		CharacterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
	}

	protected override void ExitState()
	{
		CharacterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
	}

	public override void _PhysicsProcess(double delta)
	{
		CharacterNode.StateMachineNode.SwitchState<EnemyReturnState>();
	}
}