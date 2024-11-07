using UnityEngine;
using System.Collections.Generic;

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
		private float _timer = 0f;

		// TODO: Add hurry up timer	

		private void Update()
		{
			_timer += Time.deltaTime;

			// Pick a random item from items list and spawn it at a random spawn point.
			// Remove spawn point from list after spawning item so that no two items spawn at the same point.
			// Keep track of spawned item count so that only a set number of items can be spawned.
			if (_timer > _spawnInterval && _spawnPoints.Count > 0 && _itemPrefabs.Count > 0 &&
				_spawnedItemCount < _maxItemCount)
			{
				int randomItem = Random.Range(0, _itemPrefabs.Count);
				int randomSpawnPoint = Random.Range(0, _spawnPoints.Count);

				Item item = Instantiate(_itemPrefabs[randomItem], _spawnPoints[randomSpawnPoint].position, Quaternion.identity,
										_levelParent);	
				_spawnedItemCount++;
				_spawnPoints.Remove(_spawnPoints[randomSpawnPoint]);
				_timer = 0;

				// If the spawned item is a shell, remove it from the item list so that no two shells spawn in one level.
				if (item.ItemData.ItemType == ItemType.Shell)
				{
					_itemPrefabs.Remove(_itemPrefabs[randomItem]);
				}
			}
		}
	}
}
