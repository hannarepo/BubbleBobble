using UnityEngine;

namespace BubbleBobble
{
/// <summary>
/// Fireballs that spawn from fire bubbles.
/// </summary>
///
/// <remarks>
/// author: Jose Mäntylä
/// </remarks>
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
			if (transform.position.y < _fallDistanceLimit)
			{
				Destroy(gameObject);
			}
		}

		private void SpreadFire()
		{
			Instantiate(_groundFirePrefab, transform.position, Quaternion.identity, transform.parent);
		}
	}
}
