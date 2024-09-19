using demo.Scripts.General;
using Godot;

namespace demo.Scripts.Character.Enemy;

public partial class EnemyReturnState : EnemyState
{

    public override void _Ready()
    {
        base._Ready();
        Destination = GetPointGlobalPosition(0);
    }

    protected override void EnterState()
    {
        CharacterNode.AnimPlayerNode.Play(GameConstants.AnimMove);

        CharacterNode.AgentNode.TargetPosition = Destination;
        
        CharacterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        CharacterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (CharacterNode.AgentNode.IsNavigationFinished())
        {
            // GD.Print("Reached destination");
            CharacterNode.StateMachineNode.SwitchState<EnemyPatrolState>();
            return;
        }

        Move();
    }
}