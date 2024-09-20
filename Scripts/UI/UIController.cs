using System.Collections.Generic;
using System.Linq;
using demo.Scripts.General;
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

        GameEvents.OnEndGame += HandleEndGame;
        GameEvents.OnVictory += HandleVictory;
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
        _canPause = true;
        GetTree().Paused = false;
        _containers[ContainerType.Start].Visible = false;
        _containers[ContainerType.Stats].Visible = true;
        GameEvents.RaiseStartGame();
    }

    private void HandelPausePressed()
    {
        GetTree().Paused = false;
        _containers[ContainerType.Pause].Visible = false;
        _containers[ContainerType.Stats].Visible = true;
    }
}