using UnityEngine;

namespace BubbleBobble
{
	public class BubbleTeleportTrigger : MonoBehaviour
	{
		void OnTriggerEnter2D(Collider2D collider)
		{
			if (collider.gameObject.CompareTag(Tags.Bubble))
			{
				Transform bubblePos = collider.gameObject.transform;
				bubblePos.position = new Vector2(transform.position.x, transform.position.y + 3f);
			}
		}
	}
}
