using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class BubbleSpawner : MonoBehaviour
    {
        //[SerializeField] private bool _spawnFromTop = false;
        //[SerializeField] private float _spawnRate = 5f;

        #region Spawners
        public void SpawnBomb()
        {
            GameObject bomb = Resources.Load("Prefabs/Bubbles/Special/BombBubble") as GameObject;
            Instantiate(bomb);
            // TODO: Rework spawn position
            bomb.transform.position = new Vector3(0, 0, 0);
        }
        public void SpawnFireBubble()
        {
            
        }

        #endregion
    }
}
