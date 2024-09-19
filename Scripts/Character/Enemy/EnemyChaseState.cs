using Godot;
using System;
using System.Linq;
using demo.Scripts.Character.Enemy;
using demo.Scripts.General;

public partial class EnemyChaseState : EnemyState
{
	[Export] private Timer _updateTimerNode;
	private CharacterBody3D _target;
	protected override void EnterState()
	{
		CharacterNode.AnimPlayerNode.Play(GameConstants.AnimMove);
		_target = CharacterNode.ChaseAreaNode
			.GetOverlappingBodies().First() as CharacterBody3D;
		_updateTimerNode.Timeout += HandleDestTimerTimeout;
		CharacterNode.AttackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
		CharacterNode.ChaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
	}

	protected override void ExitState()
	{
		_updateTimerNode.Timeout -= HandleDestTimerTimeout;
		CharacterNode.AttackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
		CharacterNode.ChaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;
	}

	public override void _PhysicsProcess(double delta)
	{
		Move();
	}

	private void HandleDestTimerTimeout()
	{
		Destination = _target.GlobalPosition;
		CharacterNode.AgentNode.TargetPosition = Destination;
	}
	
	protected void HandleAttackAreaBodyEntered(Node3D body)
	{
		CharacterNode.StateMachineNode.SwitchState<EnemyAttackState>();
	}

	protected void HandleChaseAreaBodyExited(Node3D body)
	{
		CharacterNode.StateMachineNode.SwitchState<EnemyReturnState>();
	}
}
