using System.Linq;
using demo.Scripts.General;
using Godot;

namespace demo.Scripts.Character;

public partial class StateMachine : Node
{
    [Export] private Node _currentState = null;

    [Export] private Node[] _states;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _currentState.Notification(GameConstants.NotificationEnterState);
    }

    public void SwitchState<T>()
    {
        var newState = _states.FirstOrDefault(state => state is T);
        if (newState == null)
        {
            GD.PrintErr($"未知状态 {typeof(T)}");
            return;
        }

        if (_currentState is T)
        {
            return;
        }
        _currentState.Notification(GameConstants.NotificationExitState);
        _currentState = newState;
        _currentState.Notification(GameConstants.NotificationEnterState);
    }
}