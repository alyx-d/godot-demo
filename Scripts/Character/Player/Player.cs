using demo.Scripts.General;
using demo.Scripts.Resources;
using Godot;

namespace demo.Scripts.Character.Player;

public partial class Player : Character
{
    public override void _Ready()
    {
        base._Ready();
        GameEvents.OnReward += HandleReward;
    }

    public override void _Input(InputEvent @event)
    {
        Direction = Input.GetVector(
            GameConstants.InputMoveLeft,
            GameConstants.InputMoveRight,
            GameConstants.InputMoveForward,
            GameConstants.InputMoveBackward
        );
    }

    private void HandleReward(RewardResource reward)
    {
        StatResource targetResource = GetStatResource(reward.TargetStat);
        targetResource.StatValue += reward.Amount;
    }
}