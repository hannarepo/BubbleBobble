using UnityEngine;

namespace BubbleBobble
{
	public class WallChecker : MonoBehaviour
	{
		private bool _isTouchingWall = false;
		public bool IsTouchingWall => _isTouchingWall;

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if(collider.gameObject.CompareTag(Tags._platform)
			|| collider.gameObject.CompareTag(Tags._wall))
			{
				_isTouchingWall = true;
			}
		}

		private void OnTriggerExit2D(Collider2D collider)
		{
			if(collider.gameObject.CompareTag(Tags._platform)
			|| collider.gameObject.CompareTag(Tags._wall))
			{
				_isTouchingWall = false;
			}
		}
	}
}
