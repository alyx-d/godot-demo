using demo.Scripts.General;
using Godot;

namespace demo.Scripts.Character.Enemy;

public partial class EnemyIdleState : EnemyState
{
	protected override void EnterState()
	{
		CharacterNode.AnimPlayerNode.Play(GameConstants.AnimIdle);
	}
}