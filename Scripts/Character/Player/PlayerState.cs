using demo.Scripts.General;
using Godot;

namespace demo.Scripts.Character.Player;

public abstract partial class PlayerState : Node
{
    protected Player PlayerNode;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        PlayerNode = GetOwner<Player>();
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        switch (what)
        {
            case GameConstants.NotificationEnterState:
                EnterState();
                SetPhysicsProcess(true);
                SetProcessInput(true);
                break;
            case GameConstants.NotificationExitState:
                SetPhysicsProcess(false);
                SetProcessInput(false);
                break;
        }
    }

    protected virtual void EnterState()
    {
    }
}