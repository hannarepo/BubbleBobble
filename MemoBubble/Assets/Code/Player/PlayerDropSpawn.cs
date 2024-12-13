using UnityEngine;

namespace BubbleBobble
{
	/// <summary>
	/// When player drops out of the level, they will be moved to
	/// spawn point at the top of the level.
	/// </summary>
	///
	/// <remarks>
	/// author: Hanna Repo
	/// </remarks>

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
                GameObject topSpawnLeft = GameObject.FindGameObjectWithTag("TopSpawnLeft");
                GameObject topSpawnRight = GameObject.FindGameObjectWithTag("TopSpawnRight");

                if (_transform.position.x < 0 && topSpawnLeft != null)
                {
                    _spawnPosition = topSpawnLeft.transform.position;
                }
                else if (_transform.position.x >= 0 && topSpawnRight != null)
                {
                    _spawnPosition = topSpawnRight.transform.position;
                }

                _transform.position = _spawnPosition;
            }
        }
    }
}
