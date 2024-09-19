namespace demo.Scripts.General;

public abstract class GameConstants
{
    // Animations
    public const string AnimIdle = "Idle";
    public const string AnimMove = "Move";
    public const string AnimDash = "Dash";
    public const string AnimAttack = "Attack";
    public const string AnimDeath = "Death";
    
    // Inputs
    public const string InputMoveLeft = "MoveLeft";
    public const string InputMoveRight = "MoveRight";
    public const string InputMoveForward = "MoveForward";
    public const string InputMoveBackward = "MoveBackward";
    public const string InputDash = "Dash";
    public const string InputAttack = "Attack";
    
    
    // Notifications
    public const int NotificationEnterState = 5001;
    public const int NotificationExitState = 5002;
}