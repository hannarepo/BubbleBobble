using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
/// <remarks>
/// author: Jose Mäntylä
/// </remarks>
/// 
/// <summary>
/// Used to change the level.
/// Loads the prefab of the next level below the current level
/// and moves the current and next level up together until
/// the next level is in place and then destroys the previous level.
/// </summary>
	public class LevelChanger : MonoBehaviour
	{
		private int _levelIndex = 0;
		private bool _isLevelLoaded = true;
		public bool IsLevelLoaded { get { return _isLevelLoaded; } }
		private GameObject _newLevel;
		private PlayerControl _playerControl;
		[SerializeField] private GameObject _currentLevel;
		[SerializeField] private Transform _newLevelSpawnPoint;
		[SerializeField] private float _speed = 1.0f;
		[SerializeField] private GameObject _player;
		[SerializeField] private Transform _playerReturnPoint;
		[SerializeField] private float _levelChangeDelay = 1f;
		[SerializeField] private List<GameObject> _levelPrefabs = new List<GameObject>();
		[SerializeField] private int _windowsEnvironmentIndex;
		[SerializeField] private int _liminalEnvironmentIndex;
		private float _currentLevelMovePosY = 0f;
		private bool _startLevelChange = false;

		public int LevelIndex => _levelIndex;

		private void Start()
		{
			_currentLevelMovePosY = Mathf.Abs(_newLevelSpawnPoint.position.y);
			_playerControl = _player.GetComponent<PlayerControl>();
		}

		void Update()
		{
			// If the new level is loaded, move the current and the new level up until the new level is centered
			if (_startLevelChange && !_isLevelLoaded && _levelIndex <= _levelPrefabs.Count)
			{
				LevelChangeMovement();
				if (_newLevel.transform.position == Vector3.zero
					&& _player.transform.position == _playerReturnPoint.position)
				{
					Destroy(_currentLevel);
					_currentLevel = _newLevel;
					_playerControl.UnRestrainPlayer();
					//FindObjectOfType<LevelManager>().CanSpawnItem = true;
					_levelIndex++;
					_isLevelLoaded = true;
					_startLevelChange = false;
				}
			}
		}

		/// <summary>
		/// Loads the prefab of the next level below the current level
		/// and restrains the player
		/// </summary>
		public void LoadLevel()
		{
			GameObject _levelPrefab = _levelPrefabs[_levelIndex];
			_newLevel = Instantiate(_levelPrefab, _newLevelSpawnPoint.position, Quaternion.identity);
			_playerControl.RestrainPlayer();
			_isLevelLoaded = false;
			Invoke("DelayedBoolSwitch", _levelChangeDelay);
		}

		/// <summary>
		/// Moves the current and the new level up until the new level is centered
		/// and moves the player to the return point.
		/// </summary>
		private void LevelChangeMovement()
		{
			_newLevel.transform.position = Vector3.MoveTowards(_newLevel.transform.position, Vector3.zero, _speed * Time.deltaTime);
			_currentLevel.transform.position = Vector3.MoveTowards(_currentLevel.transform.position, new Vector3(0f, _currentLevelMovePosY, 0f), _speed * Time.deltaTime);
			_player.transform.position = Vector3.MoveTowards(_player.transform.position, new Vector3(_playerReturnPoint.position.x, _playerReturnPoint.position.y, 0), _speed * Time.deltaTime);
		}

		private void DelayedBoolSwitch()
		{
			_startLevelChange = !_startLevelChange;
		}
	}
}
