using UnityEngine;

namespace BubbleBobble
{
	public class Umbrella : MonoBehaviour
	{
		private LevelChanger _levelChanger;
		private GameManager _gameManager;

		private void Awake()
		{
			_levelChanger = FindObjectOfType<LevelChanger>();
			_gameManager = FindObjectOfType<GameManager>();
		}

		public void SkipLevels()
		{
			_gameManager.ClearEnemyList();
			_levelChanger.LevelIndex++;
			_levelChanger.LoadLevel();
		}
	}
}
