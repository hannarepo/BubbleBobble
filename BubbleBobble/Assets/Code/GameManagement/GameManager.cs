using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
	/// <summary>
	/// Keeps track of most things that happen in-game,
	/// e.g. number of projectile bubbles in the level and
	/// number of enemies in the level.
	/// </summary>
	/// 
	/// <remarks>
	/// author: Jose Mäntylä, Hanna Repo
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

		#region Unity Functions
		private void Start()
		{
			_levelChanger = GetComponent<LevelChanger>();;
		}

		private void Update()
		{
			if (_projectileList.Count == _maxProjectiles)
			{
				_projectileList[0].GetComponent<ProjectileBubble>().PopBubble();
			}
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

		private void NextLevel()
		{
			_levelChanger.LoadLevel();
			CounterReset();
			_canChangeLevel = true;

			foreach (GameObject projectile in _projectileList)
			{
				projectile.GetComponent<ProjectileBubble>().PopBubble();
			}

		}

		public void BubbleSpawnerInitialization()
		{
			_bubbleSpawner = FindObjectOfType<BubbleSpawner>();
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
						print("Invoking level change");
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
				_enemyList[0].GetComponent<EnemyManagement>().Die();
			}

			TrappedEnemyBubble[] trappedEnemies = FindObjectsOfType<TrappedEnemyBubble>();

			for (int i=0; i < trappedEnemies.Length; i++)
			{
				Destroy(trappedEnemies[i].gameObject);
			}
		}

		// Adds an enemy object to a list
		public void AddEnemyToList(GameObject enemyObject)
		{
			_enemyList.Add(enemyObject);
			// print("Enemies in list: " + _enemyList.Count);
		}

		public void RemoveEnemyFromList(GameObject enemyObject)
		{
			_enemyList.Remove(enemyObject);
			print("Enemies in list: " + _enemyList.Count);
			CheckCounters("Enemy");
			Destroy(enemyObject);
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
	}
}
