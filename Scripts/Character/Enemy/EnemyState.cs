using Godot;
using Vector3 = Godot.Vector3;

namespace demo.Scripts.Character.Enemy;

public abstract partial class EnemyState : CharacterState
{
    protected Vector3 Destination;

    [Export(PropertyHint.Range, "1,10,0.1")]
    protected float _moveSpeed = 3.0f;

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
}