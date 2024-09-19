using System.Collections.Generic;
using System.Linq;
using demo.Scripts.General;
using Godot;

namespace demo.Scripts.UI;

public partial class UIController : Control
{
    private Dictionary<ContainerType, UIContainer> _containers;

    public override void _Ready()
    {
        _containers = GetChildren().Where(
            element => element is UIContainer
        ).Cast<UIContainer>().ToDictionary(
            element => element.Container
        );
        _containers[ContainerType.Start].Visible = true;

        _containers[ContainerType.Start].ButtonNode.Pressed += HandleStartPressed;
    }

    private void HandleStartPressed()
    {
        GetTree().Paused = false;
        _containers[ContainerType.Start].Visible = false;
        GameEvents.RaiseStartGame();
    }
}