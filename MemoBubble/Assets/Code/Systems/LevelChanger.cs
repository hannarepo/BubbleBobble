using System.Collections.Generic;
using UnityEngine;

namespace MemoBubble
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
		[SerializeField] private List<GameObject> _levelPrefabs = new List<GameObject>();
		[SerializeField] private GameObject _currentLevel;
		[SerializeField] private Transform _newLevelSpawnPoint;
		[SerializeField] private float _speed = 1.0f;
		[SerializeField] private GameObject _player;
		[SerializeField] private Transform _playerReturnPoint;
		[SerializeField] private float _levelChangeDelay = 1f;
		[SerializeField] private string _windowsLevelName;
		[SerializeField] private string _liminalLevelName;
		[SerializeField] private Audiomanager _audioManager;
		[SerializeField] private AudioClip _underwaterMusic;
		[SerializeField] private AudioClip _windowsMusic;
		[SerializeField] private AudioClip _liminalMusic;
		[SerializeField] private GameObject _intro;
		[SerializeField] private float _introDelay = 13f;
		[SerializeField] private SkipButton _skipIntroButton;
		[SerializeField] private CurrentLevelText _currentLevelText;
		[SerializeField] private SpriteFade _windowsTransitionPrefab;
		[SerializeField] private SpriteFade _liminalTransitionPrefab;
		private int _levelIndex = 0;
		private bool _isLevelLoaded = true;
		private bool _isLevelStarted = false;
		private GameObject _newLevel;
		private PlayerControl _playerControl;
		private GameObject _levelPrefab;
		private float _currentLevelMovePosY = 0f;
		private bool _startLevelChange = false;
		private bool _waitForFade = false;
		private SpriteFade _spriteFade;

		public int LevelIndex
		{
			get => _levelIndex;
			set { _levelIndex = value; }
		}
		public bool IsLevelLoaded => _isLevelLoaded;
		public bool IsLevelStarted => _isLevelStarted;
		public bool StartLevelChange => _startLevelChange;
		public int LevelCount => _levelPrefabs.Count;

		private void Start()
		{
			_currentLevelMovePosY = Mathf.Abs(_newLevelSpawnPoint.position.y);
			_playerControl = _player.GetComponent<PlayerControl>();
			_playerControl.RestrainPlayer(false);
			if (_currentLevel.name == "Level1")
			{
				// Call / invoke the intro thingies here
				Invoke("IntroDone", _introDelay);
			}
		}

		void Update()
		{
			// If the new level is loaded, move the current and the new level up until the new level is centered
			if (_startLevelChange && !_isLevelStarted && _levelIndex <= _levelPrefabs.Count)
			{
				LevelChangeMovement();

				// If the next level is in a new world,
				// wait for the fade in and fade out of the transition screen.
				if (_waitForFade)
				{
					if (_newLevel.transform.position == Vector3.zero
					&& _player.transform.position == _playerReturnPoint.position
					&& !_isLevelLoaded)
					{
						_spriteFade.StartFadeOut();
						SpriteFade.FadeComplete += OnFadeComplete;
						_isLevelLoaded = true;
					}
					else
					{
						return;
					}
				}

				// Finish level change when the player and new level are in place.
				if (_newLevel.transform.position == Vector3.zero
					&& _player.transform.position == _playerReturnPoint.position
					&& !_waitForFade)
				{
					LevelChangeComplete();
				}
			}
		}

		/// <summary>
		/// Loads the prefab of the next level below the current level
		/// and restrains the player
		/// </summary>
		public void LoadLevel()
		{
			_levelPrefab = _levelPrefabs[_levelIndex];
			_newLevel = Instantiate(_levelPrefab, _newLevelSpawnPoint.position, Quaternion.identity);
			_playerControl.RestrainPlayer(true);
			_isLevelLoaded = false;
			_isLevelStarted = false;
			if (_levelPrefab.name == _windowsLevelName
				|| _levelPrefab.name == _liminalLevelName)
			{
				StartTransition();
			}
			else
			{
				Invoke("DelayedLevelChange", _levelChangeDelay);
			}
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

		/// <summary>
		/// Method to initiate level change with a delay
		/// using Invoke.
		/// </summary>
		private void DelayedLevelChange()
		{
			_startLevelChange = true;

			if (_levelPrefab.name == _windowsLevelName)
			{
				_audioManager.ChangeMusic(_windowsMusic);
				_currentLevelText.UpdateWorldNumber();
			}
			else if (_levelPrefab.name == _liminalLevelName)
			{
				_audioManager.ChangeMusic(_liminalMusic);
				_currentLevelText.UpdateWorldNumber();
			}
		}

		/// <summary>
		/// Stop listening to the FadeComplete event and execute actions
		/// </summary>
		private void OnFadeComplete()
		{
			SpriteFade.FadeComplete -= OnFadeComplete;
			if (_waitForFade && !_startLevelChange)
			{
				DelayedLevelChange();
			}
			else
			{
				_waitForFade = false;
			}
		}

		/// <summary>
		/// This method contains all the variable changes and method calls
		/// needed when the level change is complete and playing can continue.
		/// </summary>
		private void LevelChangeComplete()
		{
			Destroy(_currentLevel);
			_currentLevel = _newLevel;
			_playerControl.UnRestrainPlayer();
			_levelIndex++;
			_isLevelLoaded = true;
			_isLevelStarted = true;
			_startLevelChange = false;
			_currentLevelText.UpdateLevelNumber();
		}

		/// <summary>
		/// Checks which transition screen is needed and instantiates it,
		/// then start fading it and listening for the FadeComplete event.
		/// </summary>
		private void StartTransition()
		{
			if (_levelPrefab.name == _windowsLevelName)
			{
				_spriteFade = Instantiate(_windowsTransitionPrefab);
			}
			else
			{
				_spriteFade = Instantiate(_liminalTransitionPrefab);
			}
			_waitForFade = true;
			_spriteFade.StartFadeIn();
			SpriteFade.FadeComplete += OnFadeComplete;
		}

		/// <summary>
		/// This method contains all the necessary actions
		/// done after the intro.
		/// </summary>
		private void IntroDone()
		{
			_playerControl.UnRestrainPlayer();
			_intro.SetActive(false);
			_skipIntroButton.SetButtonActive(false);
			_isLevelStarted = true;
			_audioManager.ChangeMusic(_underwaterMusic);
			_intro = null;
		}

		public void SkipIntro()
		{
			CancelInvoke("IntroDone");
			IntroDone();
		}
	}
}
