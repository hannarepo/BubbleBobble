using UnityEngine;
using System.Collections.Generic;

namespace BubbleBobble
{
	/// <summary>
	/// Manages the spawning of items in the level.
	/// Keeps track of time player has spent in the level for hurry up timer.
	/// Hurry up mode makes enemies move faster.
	/// </summary>
	///
	/// <remarks>
	/// author: Hanna Repo
	/// </remarks>
	public class LevelManager : MonoBehaviour
	{
		[SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
		[SerializeField] private float _spawnInterval = 5f;
		[SerializeField] private int _maxItemCount = 3;
		[SerializeField] private float _hurryUpTime = 30f;
		[SerializeField] private float _textFlashTime = 2f;
		[SerializeField] private bool _canSpawnShell = true;
		[SerializeField] private bool _canSpawnUmbrella = false;
		[SerializeField] GameObject _enemies;
		[SerializeField] private float _spawnUndefeatableTime = 35f;
		[SerializeField] private AudioClip _bossSFX;
		private GameManager _gameManager;
		private List<Item> _spawnableItemPrefabs;
		private float _spawnedItemCount;
		private float _spawnTimer = 0f;
		private bool _canSpawnItem = true;
		private float _hurryUpTimer = 0f;
		private bool _hurryUp = false;
		private LevelChanger _levelChanger;
		private Audiomanager _audioManager;
		private GameObject _undefeatableEnemy;
		private Health _playerHealth;
		private bool _spawnedUndefeatable = false;

		public bool CanSpawnItem
		{
			get => _canSpawnItem;
			set => _canSpawnItem = value;
		}

		public bool IsHurryUpActive => _hurryUp;

		private void Start()
		{
			_gameManager = FindObjectOfType<GameManager>();
			_levelChanger = FindObjectOfType<LevelChanger>();
			_spawnableItemPrefabs = _gameManager.SpawnableItems;
			_audioManager = FindObjectOfType<Audiomanager>();
			_undefeatableEnemy = _gameManager.UndefeatableEnemy;
			_playerHealth = FindObjectOfType<Health>();
		}

		private void Update()
		{
			if (_levelChanger.IsLevelStarted)
			{
				_spawnTimer += Time.deltaTime;
				_hurryUpTimer += Time.deltaTime;
				_enemies.SetActive(true);
			}

			if (_spawnTimer > _spawnInterval)
			{
				SpawnItemAtInterval(Random.Range(0, _spawnableItemPrefabs.Count));
			}

			if (_hurryUpTimer >= _hurryUpTime && !_hurryUp)
			{
				HurryUp();
				_hurryUp = true;
			}

			if (_hurryUpTimer >= _spawnUndefeatableTime && !_spawnedUndefeatable)
			{
				_undefeatableEnemy.SetActive(true);
				_audioManager.PlaySFX(_bossSFX);
				_spawnedUndefeatable = true;
			}

			if (_hurryUp && _playerHealth.LostLife)
			{
				ResetHurryUp();
			}
		}

		private void SpawnItemAtInterval(int index)
		{
			// Pick a random item from items list and spawn it at a random spawn point.
			// Remove spawn point from list after spawning item so that no two items spawn at the same point.
			// Keep track of spawned item count so that only a set number of items can be spawned.
			if (_spawnPoints.Count > 0 && _spawnableItemPrefabs.Count > 0 &&
				_spawnedItemCount < _maxItemCount && _canSpawnItem)
			{
				int randomSpawnPoint = Random.Range(0, _spawnPoints.Count);
				Item item = null;

				if ((_spawnableItemPrefabs[index].ItemData.ItemType == ItemType.Shell && !_canSpawnShell) ||
					 (_spawnableItemPrefabs[index]. ItemData.ItemType == ItemType.Umbrella && !_canSpawnUmbrella))
				{
					index = Random.Range(0, _spawnableItemPrefabs.Count);
					item = Instantiate(_spawnableItemPrefabs[index], _spawnPoints[randomSpawnPoint].position, Quaternion.identity,
										transform);
				}
				else if (_spawnableItemPrefabs[index].ItemData.ItemType == ItemType.Umbrella && _canSpawnUmbrella)
				{
					int spawnChance = Random.Range(0, 2);
					if (spawnChance == 1)
					{
						Instantiate(_spawnableItemPrefabs[index], _spawnPoints[randomSpawnPoint].position, Quaternion.identity);
						_canSpawnUmbrella = false;
					}
				}
				else if (_spawnableItemPrefabs[index].ItemData.ItemType != ItemType.Umbrella)
				{
					item = Instantiate(_spawnableItemPrefabs[index], _spawnPoints[randomSpawnPoint].position, Quaternion.identity,
										transform);
					if (_spawnableItemPrefabs[index].ItemData.ItemType == ItemType.Shell)
					{
						_canSpawnShell = false;
					}
				}

				_spawnedItemCount++;
				_spawnPoints.Remove(_spawnPoints[randomSpawnPoint]);
				_spawnTimer = 0;
			}
		}

		private void HurryUp()
		{
			FlashHurryUpText();
			_audioManager.SpeedUpMusic();
		}

		public void ResetHurryUpTimer()
		{
			_hurryUpTimer = 0;
		}

		public void ResetHurryUp()
		{
			_audioManager.SlowDownMusic();
			ResetHurryUpTimer();
			_hurryUp = false;
			_gameManager.HurryUpText.SetActive(false);
			CancelInvoke("FlashHurryUpText");
			_undefeatableEnemy.SetActive(false);
			_spawnedUndefeatable = false;
		}

		private void FlashHurryUpText()
		{
			bool isActive = _gameManager.HurryUpText.activeInHierarchy;
			isActive = !isActive;
			_gameManager.HurryUpText.SetActive(isActive);
			Invoke("FlashHurryUpText", _textFlashTime);
		}
	}
}
