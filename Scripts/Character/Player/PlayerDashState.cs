using demo.Scripts.General;
using Godot;

namespace demo.Scripts.Character.Player;

public partial class PlayerDashState : PlayerState
{
    [Export(PropertyHint.Range, "0, 20, 0.1")]
    private float _speed = 10f;

    [Export] private Timer _dashTimerNode;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        _dashTimerNode.Timeout += HandleDashTimerTimeout;
    }

    public override void _PhysicsProcess(double delta)
    {
        PlayerNode.MoveAndSlide();
        PlayerNode.Flip();
    }

    protected override void EnterState()
    {
        base.EnterState();
        PlayerNode.AnimPlayerNode.Play(GameConstants.AnimDash);
        PlayerNode.Velocity = new Vector3(
            PlayerNode.Direction.X,
            0,
            PlayerNode.Direction.Y
        );
        if (PlayerNode.Velocity == Vector3.Zero)
        {
            PlayerNode.Velocity = PlayerNode.SpriteNode.FlipH ? Vector3.Left : Vector3.Right;
        }

        PlayerNode.Velocity *= _speed;
        _dashTimerNode.Start();
    }

    private void HandleDashTimerTimeout()
    {
        PlayerNode.Velocity = Vector3.Zero;
        PlayerNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }
}