using UnityEngine;

namespace BubbleBobble
{
/// <summary>
/// If enemies with "EnemyManagement" component collide with this trigger, they will die.
/// </summary>
///
/// <remarks>
/// author: Jose Mäntylä
/// </remarks>
	public class GroundFireTrigger : MonoBehaviour
	{

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag(Tags.Enemy))
			{
				other.gameObject.GetComponent<EnemyManagement>().LaunchAtDeath(true);
			}

		}
	}
}
