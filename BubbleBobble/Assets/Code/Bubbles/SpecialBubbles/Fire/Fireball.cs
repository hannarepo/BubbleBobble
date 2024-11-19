using UnityEngine;

namespace BubbleBobble
{
	public class Fireball : MonoBehaviour
	{
		[SerializeField] private GameObject _groundFirePrefab;
		[SerializeField] private float _fallDistanceLimit = -8f;

		private void OnCollisionEnter2D(Collision2D collision)
		{
			SpreadFire();
			Destroy(gameObject);
		}

		private void Update()
		{
			if (gameObject.transform.position.y < _fallDistanceLimit)
			{
				Destroy(gameObject);
			}
		}

		private void SpreadFire()
		{
			Instantiate(_groundFirePrefab, gameObject.transform.position, Quaternion.identity);
		}
	}
}
