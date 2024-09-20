using demo.Scripts.General;
using demo.Scripts.Resources;
using Godot;

namespace demo.Scripts.Reward;

public partial class TreasureChest : StaticBody3D
{
    [Export] private Area3D _areaNode;
    [Export] private Sprite3D _spriteNode;
    [Export] private RewardResource _reward;

    public override void _Ready()
    {
        _areaNode.BodyEntered += body => _spriteNode.Visible = true;
        _areaNode.BodyExited += body => _spriteNode.Visible = false;
    }

    public override void _Input(InputEvent @event)
    {
        if (
            !_areaNode.Monitoring ||
            !_areaNode.HasOverlappingBodies() ||
            !Input.IsActionJustPressed(GameConstants.InputInteract)
        )
        {
            return;
        }

        _areaNode.Monitoring = false;
        GameEvents.RaiseReward(_reward);
    }
}