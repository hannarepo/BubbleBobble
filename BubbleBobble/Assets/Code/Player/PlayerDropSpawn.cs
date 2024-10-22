/// <remarks>
/// author: Hanna Repo
/// </remarks>
///
/// <summary>
/// When player drops out of the level, they will be moved to
/// a random spawn point at the top of the level.
/// </summary>

using UnityEngine;

namespace BubbleBobble
{
    public class PlayerDropSpawn : MonoBehaviour
    {
        private Vector3 _spawnPosition;
        private GameObject[] _spawnPointsInLevel;
        private Transform _transform;
        [SerializeField] private int _triggerYPos = -6;

        private void Awake()
        {
            
            _transform = GetComponent<Transform>();
        }

        private void Update()
        {
            if (_transform.position.y < _triggerYPos)
            {
                _spawnPointsInLevel = GameObject.FindGameObjectsWithTag("TopSpawnPoint");

                // If there are more than one spawn points in the level, choose a random one
                // Otherwise, use the only spawn point in the level
                if (_spawnPointsInLevel.Length > 1)
                {
                    int randomSpawnPos = Random.Range(0, _spawnPointsInLevel.Length);
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
