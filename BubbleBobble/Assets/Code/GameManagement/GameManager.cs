using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float _fireBubblesPopped = 0;


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

        private void CheckCounters()
        {
            if (_fireBubblesPopped == 3)
            {
                SpawnBomb();
            }
        }

        private void CountReset()
        {
            // Reset counters here when loading a new level
            _fireBubblesPopped = 0;
        }

        private void SpawnBomb()
        {
            GameObject bomb = Resources.Load("Prefabs/Bubbles/Special/BombBubble") as GameObject;
            Instantiate(bomb);
            bomb.transform.position = new Vector3(0, 0, 0);
        }
    }
}
