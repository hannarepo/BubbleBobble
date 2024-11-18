using UnityEngine;

namespace BubbleBobble
{
	public class GroundFireTrigger : MonoBehaviour
	{

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag(Tags._enemy))
			{
				other.gameObject.GetComponent<EnemyManagement>().SpawnItem(); // REFACTOR when enemy scripts are done
			}

		}
	}
}
