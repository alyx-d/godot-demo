using demo.Scripts.General;
using Godot;

namespace demo.Scripts.Character.Player;

public partial class PlayerDeathState : PlayerState
{
	protected override void EnterState()
	{
		CharacterNode.AnimPlayerNode.Play(GameConstants.AnimDeath);
		CharacterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
	}

	private void HandleAnimationFinished(StringName animName)
	{
		GameEvents.RaiseEndGame();
		CharacterNode.QueueFree();
	}
}