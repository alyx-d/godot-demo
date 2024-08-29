using demo.Scripts.General;
using Godot;

namespace demo.Scripts.Character.Player;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")] [Export]
    public AnimationPlayer AnimPlayerNode { get; private set; }

    [Export] public Sprite3D SpriteNode { get; private set; }
    [Export] public StateMachine StateMachineNode { get; private set; }
    public Vector2 Direction = Vector2.Zero;

    public override void _Input(InputEvent @event)
    {
        Direction = Input.GetVector(
            GameConstants.InputMoveLeft,
            GameConstants.InputMoveRight,
            GameConstants.InputMoveBackward,
            GameConstants.InputMoveForward);
    }

    public void Flip()
    {
        var isNotMovingHorizontally = Direction.X == 0;
        if (isNotMovingHorizontally) { return; }
        var isMovingLeft = Direction.X < 0;
        SpriteNode.FlipH = isMovingLeft;
    }
}