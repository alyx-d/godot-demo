using System.Collections.Generic;
using System.Linq;
using demo.Scripts.General;
using demo.Scripts.Resources;
using Godot;

namespace demo.Scripts.UI;

public partial class UIController : Control
{
    private Dictionary<ContainerType, UIContainer> _containers;

    private bool _canPause = false;

    public override void _Ready()
    {
        _containers = GetChildren().Where(
            element => element is UIContainer
        ).Cast<UIContainer>().ToDictionary(
            element => element.Container
        );
        _containers[ContainerType.Start].Visible = true;

        _containers[ContainerType.Start].ButtonNode.Pressed += HandleStartPressed;
        _containers[ContainerType.Pause].ButtonNode.Pressed += HandelPausePressed;
        _containers[ContainerType.Reward].ButtonNode.Pressed += HandleRewardPressed;

        GameEvents.OnEndGame += HandleEndGame;
        GameEvents.OnVictory += HandleVictory;
        GameEvents.OnReward += HandleReward;
    }

    public override void _Input(InputEvent @event)
    {
        if (!_canPause)
        {
            return;
        }

        if (!Input.IsActionJustPressed(GameConstants.InputPause))
        {
            return;
        }

        _containers[ContainerType.Stats].Visible = GetTree().Paused;
        GetTree().Paused = !GetTree().Paused;
        _containers[ContainerType.Pause].Visible = GetTree().Paused;
    }

    private void HandleVictory()
    {
        _canPause = false;
        _containers[ContainerType.Stats].Visible = false;
        _containers[ContainerType.Victory].Visible = true;
        GetTree().Paused = true;
    }

    private void HandleEndGame()
    {
        _canPause = false;
        _containers[ContainerType.Stats].Visible = false;
        _containers[ContainerType.Defeat].Visible = true;
    }

    private void HandleStartPressed()
    {
        GetTree().Paused = false;
        _containers[ContainerType.Start].Visible = false;
        _containers[ContainerType.Stats].Visible = true;
        GameEvents.RaiseStartGame();
        _canPause = true;
    }

    private void HandelPausePressed()
    {
        GetTree().Paused = false;
        _containers[ContainerType.Pause].Visible = false;
        _containers[ContainerType.Stats].Visible = true;
    }

    private void HandleReward(RewardResource reward)
    {
        _canPause = false;
        GetTree().Paused = true;
        _containers[ContainerType.Stats].Visible = false;
        _containers[ContainerType.Reward].Visible = true;
        _containers[ContainerType.Reward].TextureNode.Texture =
            reward.SpriteTexture;
        _containers[ContainerType.Reward].LabelNode.Text =
            reward.Description;
    }

    private void HandleRewardPressed()
    {
        GetTree().Paused = false;
        _containers[ContainerType.Reward].Visible = false;
        _containers[ContainerType.Stats].Visible = true;
        _canPause = true;
    }
}