using demo.Scripts.General;
using Godot;

namespace demo.Scripts.Character.Player;

public partial class PlayerIdleState : PlayerState
{
    public override void _PhysicsProcess(double delta)
    {
        if (PlayerNode.Direction != Vector2.Zero)
        {
            PlayerNode.StateMachineNode.SwitchState<PlayerMoveState>();
        }
    }

    protected override void EnterState()
    {
        base.EnterState();
        PlayerNode.AnimPlayerNode.Play(GameConstants.AnimIdle);
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.InputMoveDash))
        {
            PlayerNode.StateMachineNode.SwitchState<PlayerDashState>();
        }
    }
}