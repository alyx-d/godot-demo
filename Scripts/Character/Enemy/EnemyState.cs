using demo.Scripts.Resources;
using Godot;
using Vector3 = Godot.Vector3;

namespace demo.Scripts.Character.Enemy;

public abstract partial class EnemyState : CharacterState
{
    protected Vector3 Destination;

    public override void _Ready()
    {
        base._Ready();

        CharacterNode.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
    }

    [Export(PropertyHint.Range, "1,10,0.1")]
    private float _moveSpeed = 3.0f;

    protected Vector3 GetPointGlobalPosition(int index)
    {
        var localPosition = CharacterNode.PathNode.Curve.GetPointPosition(index);
        var globalPosition = CharacterNode.PathNode.GlobalPosition;
        return localPosition + globalPosition;
    }

    protected void Move()
    {
        CharacterNode.AgentNode.GetNextPathPosition();
        CharacterNode.Velocity = CharacterNode.GlobalPosition.DirectionTo(Destination);
        // CharacterNode.Velocity *= _moveSpeed;
        CharacterNode.MoveAndSlide();
        CharacterNode.Flip();
    }

    protected void HandleChaseAreaBodyEntered(Node3D body)
    {
        CharacterNode.StateMachineNode.SwitchState<EnemyChaseState>();
    }

    private void HandleZeroHealth()
    {
        CharacterNode.StateMachineNode.SwitchState<EnemyDeathState>();
    }
}