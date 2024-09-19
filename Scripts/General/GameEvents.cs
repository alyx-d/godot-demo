using System;

namespace demo.Scripts.General;

public class GameEvents
{
    public static Action OnStartGame;
    public static void RaiseStartGame() => OnStartGame?.Invoke();
}