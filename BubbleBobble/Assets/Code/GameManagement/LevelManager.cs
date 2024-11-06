using UnityEngine;

namespace BubbleBobble
{
	public class LevelManager : MonoBehaviour
	{
		[SerializeField] private Item[] _itemPrefabs;
		[SerializeField] private Transform[] _spawnPoints;
		[SerializeField] private float _spawnInterval = 5f;
		private float _timer = 0f;

		// TODO: Limit number of items spawned in level. Remove spawnpoint from list after it's used.
		// TODO: Add hurry up timer	

		private void Update()
		{
			_timer += Time.deltaTime;
			if (_timer > _spawnInterval)
			{
				int randomItem = Random.Range(0, _itemPrefabs.Length);
				int randomSpawnPoint = Random.Range(0, _spawnPoints.Length);

				Item item = Instantiate(_itemPrefabs[randomItem], _spawnPoints[randomSpawnPoint].position, Quaternion.identity);
				_timer = 0;
			}
		}
	}
}
