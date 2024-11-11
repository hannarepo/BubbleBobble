using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace BubbleBobble
{
	/// <summary>
	/// Manages the spawning of items in the level.
	/// Keep track of time player has spent in the level for hurry up timer.
	/// Hurry up mode makes enemies move faster.
	/// </summary>
	///
	/// <remarks>
	/// author: Hanna Repo
	/// </remarks>
	public class LevelManager : MonoBehaviour
	{
		[SerializeField] private List<Item> _itemPrefabs = new List<Item>();
		[SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
		[SerializeField] private float _spawnInterval = 5f;
		[SerializeField] private int _maxItemCount = 3;
		[SerializeField] private Transform _levelParent;
		private float _spawnedItemCount;
		private float _spawnTimer = 0f;
		private bool _canSpawnItem = true;
		[SerializeField] private float _hurryUpTime = 30f;
		[SerializeField] private GameObject _hurryUpText;
		[SerializeField] private float _textFlashTime = 2f;
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
			_levelChanger = FindObjectOfType<LevelChanger>();
		}

		private void Update()
		{
			if (_levelChanger.IsLevelLoaded)
			{
				_spawnTimer += Time.deltaTime;
				_hurryUpTimer += Time.deltaTime;
			}

			SpawnItem();

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

		private void SpawnItem()
		{
			// Pick a random item from items list and spawn it at a random spawn point.
			// Remove spawn point from list after spawning item so that no two items spawn at the same point.
			// Keep track of spawned item count so that only a set number of items can be spawned.
			if (_spawnTimer > _spawnInterval && _spawnPoints.Count > 0 && _itemPrefabs.Count > 0 &&
				_spawnedItemCount < _maxItemCount && _canSpawnItem)
			{
				int randomItem = Random.Range(0, _itemPrefabs.Count);
				int randomSpawnPoint = Random.Range(0, _spawnPoints.Count);

				Item item = Instantiate(_itemPrefabs[randomItem], _spawnPoints[randomSpawnPoint].position, Quaternion.identity,
										_levelParent);
				_spawnedItemCount++;
				_spawnPoints.Remove(_spawnPoints[randomSpawnPoint]);
				_spawnTimer = 0;

				// If the spawned item is a shell, remove it from the item list so that no two shells spawn in one level.
				if (item.ItemData.ItemType == ItemType.Shell)
				{
					_itemPrefabs.Remove(_itemPrefabs[randomItem]);
				}
			}
		}

		private void HurryUp()
		{
			// TODO: Call enemy's angry mode
			SetHurryUpText();
		}

		public void ResetHurryUp()
		{
			// TODO: Reset enemy's angry mode
			_hurryUpTimer = 0;
		}

		private void SetHurryUpText()
		{
			bool isActive = _hurryUpText.activeInHierarchy;
			isActive = !isActive;
			_hurryUpText.SetActive(isActive);
			Invoke("SetHurryUpText", _textFlashTime);
		}
	}
}
