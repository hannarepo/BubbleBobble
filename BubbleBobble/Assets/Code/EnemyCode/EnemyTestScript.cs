using UnityEngine;

namespace BubbleBobble
{
	public class EnemyTestScript : MonoBehaviour
	{
		private GameManager _gameManager;
		public bool _kill = false;

		// Find the Game Manager and add this enemy object to the list of enemies
		void Start()
		{
			_gameManager = FindObjectOfType<GameManager>();
			_gameManager.AddEnemyToList(gameObject);
		}

		// Add enemy death events here
		public void Die()
		{
			_gameManager.RemoveEnemyFromList(gameObject);
		}

		void Update()
		{
			if(_kill)
			{
				Die();
			}
		}
	}
}
