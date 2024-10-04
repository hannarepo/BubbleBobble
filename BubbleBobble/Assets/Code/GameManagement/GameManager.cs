/// <remarks>
/// author: Jose Mäntylä
/// </remarks>
/// 
/// <summary>
/// Keeps track of most things that happen in-game
/// </summary>
using System.Collections;
using System.Collections.Generic;
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
        // Serialized for debugging
        public List<GameObject> _enemyList = new List<GameObject>();
        private ProjectileBubble _projectile;
        private bool _hasPopped = false;

        public bool HasPopped
        {
            get { return  _hasPopped; }
            set { _hasPopped = value; }
        }

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

            _hasPopped = true;
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
                    _fireBubblesPopped = 0;
                    }
                    break; 
                case "Enemy":
                    print("Invoking level change");
                    Invoke("NextLevel", 4);
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
        }

        // Adds an enemy object to a list
        public void AddEnemyToList(GameObject enemyObject)
        {
            print("Enemies in list: " + _enemyList.Count);
            _enemyList.Add(enemyObject);
        }

        public void RemoveEnemyFromList(GameObject enemyObject)
        {
            _enemyList.Remove(enemyObject);
            print(_enemyList.Count);
            if (_enemyList.Count == 0)
                {
                    CheckCounters("Enemy");
                }
        }
        #endregion
    }
}
