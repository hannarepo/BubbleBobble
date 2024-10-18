/// <remarks>
/// author: Jose Mäntylä, Hanna Repo
/// </remarks>
/// 
/// <summary>
/// Keeps track of most things that happen in-game
/// </summary>
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BubbleBobble
{
	public class GameManager : MonoBehaviour
	{
		private LevelChanger _levelChanger;
		[SerializeField] private float _fireBubblesPopped = 0;
		private BubbleSpawner _bubbleSpawner;
		[SerializeField] private int _maxProjectiles = 10;
		private ShootBubble _projectileShot;
		private ProjectileBubble _projectile;
		[SerializeField] private float _levelChangeDelay = 2f;
		// List is serialized for debugging
		[SerializeField] private List<GameObject> _enemyList = new List<GameObject>();
		private List<GameObject> _projectileList = new List<GameObject>();

		#region Unity Functions
		private void Start()
		{
			_levelChanger = GetComponent<LevelChanger>();
			_bubbleSpawner = FindObjectOfType<BubbleSpawner>();
			_projectileShot = FindObjectOfType<ShootBubble>();
		}

		private void Update()
		{
			GameObject[] projectilesInLevel;
			projectilesInLevel = GameObject.FindGameObjectsWithTag("Projectile");

			if (projectilesInLevel.Length == _maxProjectiles)
			{
				Destroy(projectilesInLevel[0]);
			}
		}

		#endregion

		/// <summary>
		/// This method is used remotely from bubble objects when they are popped.
		/// Checks what type of bubble was popped
		/// and either activates an action or keeps track of the count.
		/// </summary>
		/// <param name="type">Received from the bubble object</param>
		public void BubblePopped(Bubble.BubbleType type)
		{
			switch (type)
			{
				case Bubble.BubbleType.Fire:
					_fireBubblesPopped++;
					CheckCounters("Fire");
					break;
				case Bubble.BubbleType.Bomb:
					DestroyEnemies();
					break;
			}
		}

		private void NextLevel()
		{
			_levelChanger.LoadLevel();
		}

		#region Counters
		private void CheckCounters(string name)
		{
			switch (name)
			{
				case "Fire":
					if (_fireBubblesPopped == 3)
					{
						_bubbleSpawner.SpawnBomb();
					}
					break; 
				case "Enemy":
					if (_enemyList.Count == 0)
					{
						print("Invoking level change");
						Invoke("NextLevel", _levelChangeDelay);
					}
					break;
				case "Projectile":
					if (_projectileList.Count == _maxProjectiles)
					{
						RemoveProjectileFromList(_projectileList[0]);
					}
					break;
			}
		}

		private void CounterReset()
		{
			// Reset counters here when loading a new level
			_fireBubblesPopped = 0;
		}
		#endregion

		#region EnemyRelated
		private void DestroyEnemies()
		{
			// Destroy all enemies on screen at index 0
			for (int i = _enemyList.Count - 1; i >= 0; i--)
			{
				_enemyList[0].GetComponent<EnemyTestScript>().Die();
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
		#endregion

		#region ProjectileRelated
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
			Destroy(projectileObject);
		}
		#endregion
	}
}
