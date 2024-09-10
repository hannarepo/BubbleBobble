using UnityEngine;

namespace BubbleBobble
{
    public class PlayerDropSpawn : MonoBehaviour
    {
        private Vector3 _spawnPosition;
        private GameObject[] _spawnPointsInLevel;
        private Transform _transform;

        private void Awake()
        {
            _spawnPointsInLevel = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint");
            _transform = GetComponent<Transform>();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("DropTrigger"))
            {
                if (_spawnPointsInLevel.Length > 1)
                {
                    int randomSpawnPos = Random.Range(0, _spawnPointsInLevel.Length);
                    Debug.Log(_spawnPointsInLevel.Length);
                    _spawnPosition = _spawnPointsInLevel[randomSpawnPos].transform.position;
                }
                else
                {
                    _spawnPosition = _spawnPointsInLevel[0].transform.position;
                }

                _transform.position = _spawnPosition;
            }
        }
    }
}
