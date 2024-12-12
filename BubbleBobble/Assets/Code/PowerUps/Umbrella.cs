using UnityEngine;

namespace BubbleBobble
{
	/// <summary>
	/// Umbrella lets player skip one level.
	/// </summary>
	public class Umbrella : MonoBehaviour
	{
		private LevelChanger _levelChanger;
		private GameManager _gameManager;
		private LevelManager _levelManager;
		private CurrentLevelText _currentLevelText;

		private void Awake()
		{
			_levelChanger = FindObjectOfType<LevelChanger>();
			_gameManager = FindObjectOfType<GameManager>();
			_levelManager = FindObjectOfType<LevelManager>();
			_currentLevelText = FindObjectOfType<CurrentLevelText>();
		}

		private void Update()
		{
			if (!_gameManager.CanChangeLevel)
			{
				Destroy(gameObject);
			}
		}

		public void SkipLevels()
		{
			if (_levelManager.IsHurryUpActive)
			{
				_levelManager.ResetHurryUp();
				_levelManager.ResetHurryUpTimer();
			}

			_gameManager.ClearEnemyList();
			_currentLevelText.SkippedLevels = true;
			_levelChanger.LevelIndex++;
			_levelChanger.LoadLevel();
		}
	}
}
