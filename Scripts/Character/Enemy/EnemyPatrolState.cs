using demo.Scripts.General;
using Godot;

namespace demo.Scripts.Character.Enemy;

public partial class EnemyPatrolState : EnemyState
{
    [Export] private Timer _idleTimerNode;

    [Export(PropertyHint.Range, "0,20,0.1")]
    private float _maxIdleTime = 4;

    private int _pointIndex = 0;

    protected override void EnterState()
    {
        CharacterNode.AnimPlayerNode.Play(GameConstants.AnimMove);

        _pointIndex = 1;
        Destination = GetPointGlobalPosition(_pointIndex);
        CharacterNode.AgentNode.TargetPosition = Destination;

        CharacterNode.AgentNode.NavigationFinished += HandleNavigationFinished;
        _idleTimerNode.Timeout += HandleIdleTimeout;
        CharacterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        CharacterNode.AgentNode.NavigationFinished -= HandleIdleTimeout;
        _idleTimerNode.Timeout -= HandleIdleTimeout;
        CharacterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;

    }

    public override void _PhysicsProcess(double delta)
    {
        if (!_idleTimerNode.IsStopped())
        {
            return;
        }

        Move();
    }

    private void HandleNavigationFinished()
    {
        CharacterNode.AnimPlayerNode.Play(GameConstants.AnimIdle);
        var rng = new RandomNumberGenerator();
        _idleTimerNode.WaitTime = rng.RandfRange(0, _maxIdleTime);

        _idleTimerNode.Start();
    }

    private void HandleIdleTimeout()
    {
        CharacterNode.AnimPlayerNode.Play(GameConstants.AnimMove);

        _pointIndex = Mathf.Wrap(_pointIndex + 1, 0, CharacterNode.PathNode.Curve.PointCount);
        Destination = GetPointGlobalPosition(_pointIndex);
        CharacterNode.AgentNode.TargetPosition = Destination;
    }
}