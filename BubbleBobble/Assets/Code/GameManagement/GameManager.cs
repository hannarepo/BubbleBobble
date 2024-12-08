using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

namespace BubbleBobble
{
	/// <summary>
	/// Keeps track of most things that happen in-game,
	/// e.g. number of projectile bubbles in the level and
	/// number of enemies in the level.
	/// </summary>
	///
	/// <remarks>
	/// author: Jose Mäntylä, Hanna Repo, Juho Kokkonen
	/// </remarks>

	public class GameManager : MonoBehaviour
	{
		private LevelChanger _levelChanger;
		private bool _canChangeLevel = true;
		[SerializeField] private float _fireBubblesPopped = 0;
		private BubbleSpawner _bubbleSpawner;
		[SerializeField] private int _maxProjectiles = 10;
		[SerializeField] private GameObject _player;
		[SerializeField] private float _levelChangeDelay = 2f;
		[SerializeField] private float _bombSpawnThreshold = 4f;
		// List is serialized for debugging
		[SerializeField] private List<GameObject> _enemyList = new List<GameObject>();
		[SerializeField] private List<GameObject> _projectileList = new List<GameObject>();

		[SerializeField, Tooltip("This list should contain soap, camera, blue floppy disc and purple floppy disc")]
		private List<Item> _spawnableItemPrefabs = new List<Item>();
		[SerializeField] private PlayerControl _playerControl;
		[SerializeField] private int _mp3SpawnThreshold = 20;
		[SerializeField] private int _cdSpawnThreshold = 40;
		[SerializeField] private Item _soap;
		[SerializeField] private Item _purpleFloppy;
		[SerializeField] private Item _blueFloppy;
		[SerializeField] private Item _camera;
		[SerializeField] private Item _mp3;
		[SerializeField] private Item _cd;
		[SerializeField] private Item _blueShell;
		[SerializeField] private Item _pupleShell;
		[SerializeField] private Item _purpleBlueShell;
		[SerializeField] private Item _redShell;
		[SerializeField] private GameObject _hurryUpText;
		[SerializeField] ScoreText _scoreText;
		[SerializeField] ScoreText _scoreEndScreen;
		[SerializeField] TextMeshProUGUI _highscoreText;
		[SerializeField] private GameObject _undefeatableEnemy;
		[SerializeField] private ImageFade _creditFade;
		[SerializeField] private float _creditFadeDelay = 3f;
		[SerializeField] private float _creditLoadDelay = 5f;
		[SerializeField] private Audiomanager _audioManager;
		private bool _addedBlueShell = false;
		private bool _addedPurpleShell = false;
		private bool _addedPurpleBlueShell = false;
		private bool _addedUmbrella = false;
		private bool _addedRedShell = false;
		private LevelManager _levelManager;

		private int _scoreCount;

		public GameObject HurryUpText => _hurryUpText;
		public GameObject UndefeatableEnemy => _undefeatableEnemy;
		public List<Item> SpawnableItems => _spawnableItemPrefabs;
		public int Score
		{
			get { return _scoreCount; }
			set
			{
				_scoreCount = value;
				_scoreText.UpdateScore(_scoreCount);
			}
		}

		#region Unity Functions
		private void Start()
		{
			_scoreCount = 0;
			_levelChanger = GetComponent<LevelChanger>();
			UpdateHighScoreText();
		}

		private void Update()
		{
			if (_projectileList.Count == _maxProjectiles)
			{
				_projectileList[0].GetComponent<ProjectileBubble>().PopBubble();
			}
		}

		public void HandleItemPickup(int points)
		{
			_scoreCount += points;
			_scoreText.UpdateScore(_scoreCount);
			_scoreEndScreen.UpdateScore(_scoreCount);
			CheckHighScore();

		}

		public void HandleBubblePop(int points)
		{
			_scoreCount += points;
			_scoreText.UpdateScore(_scoreCount);
			_scoreEndScreen.UpdateScore(_scoreCount);
			CheckHighScore();
		}

		void CheckHighScore()
		{
			if (_scoreCount > PlayerPrefs.GetInt("HighScore", 0))
			{
				PlayerPrefs.SetInt("HighScore", _scoreCount);
			}
		}

		void UpdateHighScoreText()
		{
			_highscoreText.text = $"Highscore: {PlayerPrefs.GetInt("HighScore", 0)}";
		}

		#endregion Unity Functions

		/// <summary>
		/// This method is used remotely from bubble objects when they are popped.
		/// Checks what type of bubble was popped
		/// and either activates an action or keeps track of the count.
		/// </summary>
		/// <param name="type">Received from the bubble object</param>
		public void BubblePopped(BubbleType type)
		{
			switch (type)
			{
				case BubbleType.Fire:
					_fireBubblesPopped++;
					CheckCounters("Fire");
					break;
				case BubbleType.Bomb:
					DestroyEnemies();
					break;
			}
		}

		/// <summary>
		/// GameManager's method to start level change with Invoke.
		/// </summary>
		private void NextLevel()
		{
			_levelChanger.LoadLevel();
			CounterReset();
			_canChangeLevel = true;

			for (int i = 0; i < _projectileList.Count; i++)
			{
				_projectileList[i].GetComponent<ProjectileBubble>().PopBubble();
			}
		}

		private void AddItemToList()
		{
			// If inventory contains three soap bottles, add a blue shell to the item list.
			if (_playerControl.Inventory.CheckInventoryContent(_soap.ItemData, 3) && !_addedBlueShell)
			{
				_spawnableItemPrefabs.Add(_blueShell);
				_addedBlueShell = true;
			}

			// If inventory contains three purple floppy discs, add a purple shell to the item list.
			if (_playerControl.Inventory.CheckInventoryContent(_purpleFloppy.ItemData, 3) && !_addedPurpleShell)
			{
				_spawnableItemPrefabs.Add(_pupleShell);
				_addedPurpleShell = true;
			}

			// If inventory contains three blue floppy discs, add purpleblue shell to the item list.
			if (_playerControl.Inventory.CheckInventoryContent(_blueFloppy.ItemData, 3) && !_addedPurpleBlueShell)
			{
				_spawnableItemPrefabs.Add(_purpleBlueShell);
				_addedPurpleBlueShell = true;
			}

			// If inventory contains three cameras, add a red shell to the item list.
			if (_playerControl.Inventory.CheckInventoryContent(_camera.ItemData, 3) && !_addedRedShell)
			{
				_spawnableItemPrefabs.Add(_redShell);
				_addedRedShell = true;
			}

			// If inventory contains 20 number of items, add an mp3 player to the item list.
			if (_playerControl.Inventory.Count(_mp3SpawnThreshold))
			{
				_spawnableItemPrefabs.Add(_mp3);
			}

			// If inventory contains x number of items, add a cd to the item list.
			if (_playerControl.Inventory.Count(_cdSpawnThreshold))
			{
				_spawnableItemPrefabs.Add(_cd);
			}
		}

		/// <summary>
		/// Sets the BubbleSpawner object for the GameManager.
		/// </summary>
		/// <param name="spawner">BubbleSpawner gameobject</param>
		public void BubbleSpawnerInitialization(BubbleSpawner spawner)
		{
			_bubbleSpawner = spawner;
		}

		#region Counters
		private void CheckCounters(string name)
		{
			switch (name)
			{
				case "Fire":
					if (_fireBubblesPopped == _bombSpawnThreshold)
					{
						_bubbleSpawner.SpawnBomb();
					}
					break;
				case "Enemy":
					if (_enemyList.Count == 0 && _canChangeLevel)
					{
						_levelManager = FindObjectOfType<LevelManager>();
						_levelManager.ResetHurryUpTimer();
						if (_levelManager.IsHurryUpActive)
						{
							_levelManager.ResetHurryUp();
						}
						if (_levelChanger.LevelIndex == _levelChanger.LevelCount)
						{
							Invoke("DelayedFade", _creditFadeDelay);
							Invoke("LoadCredits", _creditLoadDelay);
							_audioManager.FadeOut();
							if (_levelManager.IsHurryUpActive)
							{
								_levelManager.ResetHurryUp();
							}
							break;
						}
						//print("Invoking level change");
						_levelManager.CanSpawnItem = false;

						AddItemToList();
						Invoke("NextLevel", _levelChangeDelay);
						_canChangeLevel = false;
					}
					break;
			}
		}

		private void CounterReset()
		{
			// Reset counters here when loading a new level
			_fireBubblesPopped = 0;
		}
		#endregion Counters

		#region Enemy Related
		private void DestroyEnemies()
		{
			// Destroy all enemies on screen at index 0
			for (int i = _enemyList.Count - 1; i >= 0; i--)
			{
				_enemyList[0].GetComponent<EnemyManagement>().LaunchAtDeath(false);
			}

			TrappedEnemyBubble[] trappedEnemies = FindObjectsOfType<TrappedEnemyBubble>();

			for (int i = 0; i < trappedEnemies.Length; i++)
			{
				Destroy(trappedEnemies[i].gameObject);
			}
		}

		public void ClearEnemyList()
		{
			_enemyList.Clear();
		}

		/// <summary>
		/// Adds an enemy object to a list to keep track of their amount.
		/// </summary>
		/// <param name="enemyObject">Enemy gameobject</param>
		public void AddEnemyToList(GameObject enemyObject)
		{
			_enemyList.Add(enemyObject);
		}

		/// <summary>
		/// Removes an enemy object from the list and checks how many are left.
		/// </summary>
		/// <param name="enemyObject">Enemy gameobject</param>
		public void RemoveEnemyFromList(GameObject enemyObject)
		{
			_enemyList.Remove(enemyObject);
			CheckCounters("Enemy");
		}
		#endregion Enemy Related

		#region Projectile Related
		// Adds a projectile object to a list for keeping track of amount.
		public void AddProjectileToList(GameObject projectileObject)
		{
			CheckCounters("Projectile");
			_projectileList.Add(projectileObject);
		}

		// Removes a projectile object from the list when it is destroyed.
		public void RemoveProjectileFromList(GameObject projectileObject)
		{
			_projectileList.Remove(projectileObject);
		}
		#endregion Projectile Related

		private void DelayedFade()
		{
			_creditFade.StartFadeIn();
		}

		private void LoadCredits()
		{
			SceneManager.LoadSceneAsync("Credits");
		}
	}
}
