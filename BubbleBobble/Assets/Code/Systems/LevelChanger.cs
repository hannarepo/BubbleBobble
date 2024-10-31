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
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
	public class LevelChanger : MonoBehaviour
	{
		private int _levelIndex = 0;
		private bool _isLevelLoaded = true;
		public bool IsLevelLoaded { get { return _isLevelLoaded; } }
		private GameObject _newLevel;
		[SerializeField] private GameObject _currentLevel;
		[SerializeField] private Transform _newLevelSpawnPoint;
		[SerializeField] private float _speed = 1.0f;
		[SerializeField] private GameObject _player;
		[SerializeField] private Transform _playerReturnPoint;
		[SerializeField] private SpriteRenderer _playerBubble;
		[SerializeField] private List<GameObject> _levelPrefabs = new List<GameObject>();
		private float _currentLevelMovePosY = 0f;

		private void Start()
		{
			_currentLevelMovePosY = Mathf.Abs(_newLevelSpawnPoint.position.y);
			_playerBubble.GetComponent<SpriteRenderer>();
		}

		void Update()
		{
			// If the new level is loaded, move the current and the new level up until the new level is centered
			if (!_isLevelLoaded && _levelIndex <= _levelPrefabs.Count)
			{
				LevelChangeMovement();
				if (_newLevel.transform.position == new Vector3(0f, 0f, 0f))
				{
					Destroy(_currentLevel);
					_currentLevel = _newLevel;
					UnRestrainPlayer();
					_isLevelLoaded = true;
					_levelIndex++;
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
			RestrainPlayer();
			_isLevelLoaded = false;
		}

		/// <summary>
		/// Restricts player movement, disables the collider and enables the bubble sprite.
		/// </summary>
		private void RestrainPlayer()
		{
			_player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
			_player.GetComponent<Collider2D>().enabled = false;
			_player.GetComponent<PlayerControl>().enabled = false;
			_playerBubble.enabled = true;
		}

		/// <summary>
		/// Unrestricts player movement, enables the collider and disables the bubble sprite.
		/// </summary>
		private void UnRestrainPlayer()
		{
			_player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
			_player.GetComponent<Collider2D>().enabled = true;
			_player.GetComponent<PlayerControl>().enabled = true;
			_playerBubble.enabled = false;
		}

		/// <summary>
		/// Moves the current and the new level up until the new level is centered
		/// and moves the player to the return point.
		/// </summary>
		private void LevelChangeMovement()
		{
			_newLevel.transform.position = Vector3.MoveTowards(_newLevel.transform.position, new Vector3(0f, 0f, 0f), _speed * Time.deltaTime);
			_currentLevel.transform.position = Vector3.MoveTowards(_currentLevel.transform.position, new Vector3(0f, _currentLevelMovePosY, 0f), _speed * Time.deltaTime);
			_player.transform.position = Vector3.MoveTowards(_player.transform.position, new Vector3(_playerReturnPoint.position.x, _playerReturnPoint.position.y, 0), _speed * Time.deltaTime);
		}
	}
}
