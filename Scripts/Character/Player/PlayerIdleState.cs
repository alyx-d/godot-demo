using demo.Scripts.General;
using Godot;

namespace demo.Scripts.Character.Player;

public partial class PlayerIdleState : PlayerState
{
    public override void _PhysicsProcess(double delta)
    {
        if (CharacterNode.Direction != Vector2.Zero)
        {
            CharacterNode.StateMachineNode.SwitchState<PlayerMoveState>();
        }
    }

    protected override void EnterState()
    {
        CharacterNode.AnimPlayerNode.Play(GameConstants.AnimIdle);
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.InputMoveDash))
        {
            CharacterNode.StateMachineNode.SwitchState<PlayerDashState>();
        }
    }
}