using UnityEngine;

namespace BubbleBobble
{
    public class BubbleSpawner : MonoBehaviour
    {
        [SerializeField] private bool _spawnFromTop = false;
        public bool SpawnFromTop 
        {
            get { return _spawnFromTop; }
        }
        [SerializeField] private float _spawnRate = 5f;
        private float _timeToSpawn = 0f;



        #region Unity Functions
        private void Update()
        {
            _timeToSpawn += Time.deltaTime;
            if (_timeToSpawn >= _spawnRate)
            {
                SpawnBubble();
            }
        }

        #endregion

        #region Spawners
        private void SpawnBubble()
        {
            // To be reworked
            SpawnFireBubble();
            _timeToSpawn = 0f;
        }
        public void SpawnBomb()
        {
            GameObject bomb = Resources.Load("Prefabs/Bubbles/Special/BombBubble") as GameObject;
            Instantiate(bomb, gameObject.transform.position, Quaternion.identity);
        }
        private void SpawnFireBubble()
        {
            GameObject fireBubble = Resources.Load("Prefabs/Bubbles/Special/FireBubble") as GameObject;
            Rigidbody2D rb = fireBubble.GetComponent<Rigidbody2D>();
            if (!_spawnFromTop)
            {
                if (rb.gravityScale < 0)
                {
                    rb.gravityScale *= -1;
                }
                Debug.Log("Spawning fire bubble");
                Instantiate(fireBubble, gameObject.transform.position, Quaternion.identity);
            }
            else
            {
                if (rb.gravityScale > 0)
                {
                    rb.gravityScale *= -1;
                }
                Instantiate(fireBubble, gameObject.transform.position, Quaternion.identity);
            }
        }

        #endregion
    }
}
