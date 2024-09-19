using demo.Scripts.General;
using Godot;

namespace demo.Scripts.Character.Player;

public partial class PlayerDashState : PlayerState
{
    [Export(PropertyHint.Range, "0,20,0.1")]
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
        CharacterNode.MoveAndSlide();
        CharacterNode.Flip();
    }

    protected override void EnterState()
    {
        CharacterNode.AnimPlayerNode.Play(GameConstants.AnimDash);
        CharacterNode.Velocity = new Vector3(
            CharacterNode.Direction.X,
            0,
            CharacterNode.Direction.Y
        );
        if (CharacterNode.Velocity == Vector3.Zero)
        {
            CharacterNode.Velocity = CharacterNode.SpriteNode.FlipH ? Vector3.Left : Vector3.Right;
        }

        CharacterNode.Velocity *= _speed;
        _dashTimerNode.Start();
    }

    private void HandleDashTimerTimeout()
    {
        CharacterNode.Velocity = Vector3.Zero;
        CharacterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }
}