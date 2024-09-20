using demo.Scripts.General;
using Godot;

namespace demo.Scripts.Character.Enemy;

public partial class EnemiesContainer : Node3D
{
	public override void _Ready()
	{
		int totalEnemies = GetChildCount();
		GameEvents.RaiseNewEnemyCount(totalEnemies);
		ChildExitingTree += HandleChildExitingTree;
	}

	private void HandleChildExitingTree(Node node)
	{
		int totalEnemies = GetChildCount() - 1;
		GameEvents.RaiseNewEnemyCount(totalEnemies);
	}
}