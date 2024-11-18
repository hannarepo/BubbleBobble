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
		[SerializeField] private Transform _levelParent;
		[SerializeField] private float _hurryUpTime = 30f;
		[SerializeField] private GameObject _hurryUpText;
		[SerializeField] private float _textFlashTime = 2f;
		[SerializeField] private bool _canSpawnShell = true;
		private GameManager _gameManager;
		private List<Item> _spawnableItemPrefabs;
		private float _spawnedItemCount;
		private float _spawnTimer = 0f;
		private bool _canSpawnItem = true;
		private float _hurryUpTimer = 0f;
		private bool _hurryUp = false;
		private LevelChanger _levelChanger;

		public bool CanSpawnItem
		{
			get => _canSpawnItem;
			set => _canSpawnItem = value;
		}

		private void Start()
		{
			_gameManager = FindObjectOfType<GameManager>();
			_levelChanger = FindObjectOfType<LevelChanger>();
			_spawnableItemPrefabs = _gameManager._spawnableItemPrefabs;
		}

		private void Update()
		{
			if (_levelChanger.IsLevelLoaded)
			{
				_spawnTimer += Time.deltaTime;
				_hurryUpTimer += Time.deltaTime;
			}

			if (_spawnTimer > _spawnInterval)
			{
				SpawnItemAtInterval(Random.Range(0, _gameManager._spawnableItemPrefabs.Count));
			}

			if (_hurryUpTimer >= _hurryUpTime && !_hurryUp)
			{
				HurryUp();
				_hurryUp = true;
			}

			if (_levelChanger.StartLevelChange)
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

				if (_spawnableItemPrefabs[index].ItemData.ItemType == ItemType.Shell && !_canSpawnShell)
				{
					index = Random.Range(0, _spawnableItemPrefabs.Count);
					item = Instantiate(_spawnableItemPrefabs[index], _spawnPoints[randomSpawnPoint].position, Quaternion.identity,
										_levelParent);
				}
				else
				{
					item = Instantiate(_spawnableItemPrefabs[index], _spawnPoints[randomSpawnPoint].position, Quaternion.identity,
											_levelParent);
				}

				_spawnedItemCount++;
				_spawnPoints.Remove(_spawnPoints[randomSpawnPoint]);
				_spawnTimer = 0;

				// If the spawned item is a shell, remove it from the item list so that no two shells spawn in one level.
				if (item.ItemData.ItemType == ItemType.Shell)
				{
					_canSpawnShell = false;
				}
			}
		}

		private void HurryUp()
		{
			// TODO: Call enemy's angry mode
			FlashHurryUpText();
		}

		public void ResetHurryUp()
		{
			// TODO: Reset enemy's angry mode
			_hurryUpTimer = 0;
			_hurryUpText.SetActive(false);
		}

		private void FlashHurryUpText()
		{
			bool isActive = _hurryUpText.activeInHierarchy;
			isActive = !isActive;
			_hurryUpText.SetActive(isActive);
			Invoke("FlashHurryUpText", _textFlashTime);
		}
	}
}
