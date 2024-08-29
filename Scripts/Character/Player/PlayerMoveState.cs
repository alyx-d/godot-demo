using demo.Scripts.General;
using Godot;

namespace demo.Scripts.Character.Player;

public partial class PlayerMoveState : PlayerState
{
    [Export(PropertyHint.Range, "0, 10, 0.1")]
    private float _speed = 5.0f;

    public override void _PhysicsProcess(double delta)
    {
        if (PlayerNode.Direction == Vector2.Zero)
        {
            PlayerNode.StateMachineNode.SwitchState<PlayerIdleState>();
            return;
        }

        PlayerNode.Velocity = new Vector3(PlayerNode.Direction.X, 0, PlayerNode.Direction.Y);
        PlayerNode.Velocity *= _speed;
        PlayerNode.MoveAndSlide();
        PlayerNode.Flip();
    }

    protected override void EnterState()
    {
        base.EnterState();
        PlayerNode.AnimPlayerNode.Play(GameConstants.AnimMove);
    }


    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.InputMoveDash))
        {
            PlayerNode.StateMachineNode.SwitchState<PlayerDashState>();
        }
    }
}