using UnityEngine;

namespace BubbleBobble
{
	public class EnemyBottomSpawn : MonoBehaviour
	{
		[SerializeField] private GameObject _spawnPoint;
		private Transform _transform;

		private void Awake()
		{
			_transform = GetComponent<Transform>();
		}

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag(Tags._enemyUp))
            {
                _transform.position = _spawnPoint.transform.position;
            }
        }
    }
}
