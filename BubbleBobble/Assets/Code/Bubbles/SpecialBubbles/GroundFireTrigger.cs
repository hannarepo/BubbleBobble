using UnityEngine;

namespace BubbleBobble
{
	public class GroundFireTrigger : MonoBehaviour
	{

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Enemy"))
			{
				other.gameObject.GetComponent<EnemyTestScript>().Die(); // REFACTOR when enemy scripts are done
			}

		}
	}
}
