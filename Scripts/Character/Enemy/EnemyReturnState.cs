using demo.Scripts.General;
using Godot;

namespace demo.Scripts.Character.Enemy;

public partial class EnemyReturnState : EnemyState
{
    private Vector3 _direction = Vector3.Zero;
    [Export(PropertyHint.Range, "1, 10, 0.1")] private float _moveSpeed = 5f;

    public override void _Ready()
    {
        base._Ready();
        var localPosition = CharacterNode.PathNode.Curve.GetPointPosition(0);
        var globalPosition = CharacterNode.GetGlobalPosition();
        _direction = localPosition + globalPosition;
    }

    protected override void EnterState()
    {
        CharacterNode.AnimPlayerNode.Play(GameConstants.AnimMove);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (CharacterNode.GlobalPosition == _direction)
        {
            return;
        }
        CharacterNode.Velocity = CharacterNode.GlobalPosition.DirectionTo(_direction);
        CharacterNode.Velocity *= _moveSpeed;
        CharacterNode.MoveAndSlide();
    }
}