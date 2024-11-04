using UnityEngine;

namespace BubbleBobble
{
	public class Fireball : MonoBehaviour
	{
		[SerializeField] private GameObject _groundFirePrefab;
		private void OnCollisionEnter2D(Collision2D collision)
		{
			SpreadFire();
			Destroy(gameObject);
		}

		private void SpreadFire()
		{
			Instantiate(_groundFirePrefab, gameObject.transform.position, Quaternion.identity);
		}
	}
}
