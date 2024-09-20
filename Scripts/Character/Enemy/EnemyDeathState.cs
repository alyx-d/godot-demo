using demo.Scripts.General;
using Godot;

namespace demo.Scripts.Character.Enemy;

public partial class EnemyDeathState : EnemyState
{
	protected override void EnterState()
	{
		CharacterNode.AnimPlayerNode.Play(GameConstants.AnimDeath);

		CharacterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
	}

	private void HandleAnimationFinished(StringName animName)
	{
		CharacterNode.PathNode.QueueFree();
	}
}