using Godot;
using System;
using demo.Scripts.General;

public partial class Camera : Camera3D
{
    [Export] private Node _target;
    [Export] private Vector3 positionFromTarget;

    public override void _Ready()
    {
        GameEvents.OnStartGame += HandleStartGame;
    }

    private void HandleStartGame()
    {
        Reparent(_target);
        Position = positionFromTarget;
    }
}