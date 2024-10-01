using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace BubbleBobble
{
    public class GameManager : MonoBehaviour
    {
        // [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private float _fireBubblesPopped = 0;
        private BubbleSpawner _bubbleSpawner;
        [SerializeField] private int _maxProjectiles = 10;
        private ShootBubble _projectileShot;
        private ProjectileBubble _projectile;
        private bool _hasPopped = false;

        public bool HasPopped
        {
            get { return  _hasPopped; }
            set { _hasPopped = value; }
        }

        private void Start()
        {
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

        public void BubblePopped(Bubble.BubbleType type)
        {
            switch (type)
            {
                case Bubble.BubbleType.Fire:
                    _fireBubblesPopped++;
                    CheckCounters();
                    break;
                case Bubble.BubbleType.Bomb:
                    DestroyEnemies();
                    break;
            }

            _hasPopped = true;
        }

        #region Counters
        private void CheckCounters()
        {
            if (_fireBubblesPopped == 3)
            {
                _bubbleSpawner.SpawnBomb();
                _fireBubblesPopped = 0;
            }
        }

        private void CounterReset()
        {
            // Reset counters here when loading a new level
            _fireBubblesPopped = 0;
        }
        #endregion

        private void DestroyEnemies()
        {
            // Destroy all enemies on screen
            // List<GameObject> enemyList = new List<GameObject>();
        }
    }
}
