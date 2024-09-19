using demo.Scripts.General;
using Godot;

namespace demo.Scripts.Character.Player;

public abstract partial class PlayerState : CharacterState
{
    protected void CheckForAttackInput()
    {
        if (Input.IsActionJustPressed(GameConstants.InputAttack))
        {
            CharacterNode.StateMachineNode.SwitchState<PlayerAttackState>();
        }
    }
}