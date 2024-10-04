/// <remarks>
/// author: Jose Mäntylä
/// </remarks>
/// 
/// <summary>
/// Used to spawn special bubbles.
/// </summary>
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
                SpawnSpecialBubble();
            }
        }

        #endregion

        #region Spawners
        private void SpawnSpecialBubble()
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

        // Spawns a fire bubble prefab and changes its' gravity
        // so it floats up or down
        // depending on the spawn location boolean.
        private void SpawnFireBubble()
        {
            GameObject fireBubble = Resources.Load("Prefabs/Bubbles/Special/FireBubble") as GameObject;
            FloatDirection(fireBubble);
            Instantiate(fireBubble, gameObject.transform.position, Quaternion.identity);
        }

        #endregion

        private GameObject FloatDirection(GameObject bubble)
        {
            Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();
            if (_spawnFromTop && rb.gravityScale < 0)
            {
                rb.gravityScale *= -1;
            }
            else if (!_spawnFromTop && rb.gravityScale > 0)
            {
                rb.gravityScale *= -1;
            }
            return bubble;
        }
    }
}
