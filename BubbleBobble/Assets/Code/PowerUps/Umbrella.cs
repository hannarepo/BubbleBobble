using UnityEngine;

namespace BubbleBobble
{
	public class Umbrella : MonoBehaviour
	{
		private LevelChanger _levelChanger;
		private GameManager _gameManager;
		private CurrentLevelText _currentLevelText;

		private void Awake()
		{
			_levelChanger = FindObjectOfType<LevelChanger>();
			_gameManager = FindObjectOfType<GameManager>();
			_currentLevelText = FindObjectOfType<CurrentLevelText>();
		}

		public void SkipLevels()
		{
			_gameManager.ClearEnemyList();
			_currentLevelText.SkippedLevels = true;
			_levelChanger.LevelIndex++;
			_levelChanger.LoadLevel();
		}
	}
}
