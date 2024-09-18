using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float _fireBubblesPopped = 0;
        private BubbleSpawner _bubbleSpawner;

        private void Start()
        {
            _bubbleSpawner = FindObjectOfType<BubbleSpawner>();
        }

        public void BubblePopped(string type)
        {
            switch (type)
            {
                case "Fire":
                    _fireBubblesPopped++;
                    CheckCounters();
                    break;
            }
        }

        #region Counters
        private void CheckCounters()
        {
            if (_fireBubblesPopped == 3)
            {
                _bubbleSpawner.SpawnBomb();
            }
        }

        private void CounterReset()
        {
            // Reset counters here when loading a new level
            _fireBubblesPopped = 0;
        }
        #endregion
    }
}


// BubbleBobble notes: Enemies trapped in bubbles will float up through the roof and
// teleport to the bottom in the same X position.
// Special bubble spawns vary per level. Spawning from the roof and floating downward or
// spawning from the bottom and floating upward.
