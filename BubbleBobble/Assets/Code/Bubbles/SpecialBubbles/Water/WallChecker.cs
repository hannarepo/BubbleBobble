using UnityEngine;

namespace BubbleBobble
{
	public class WallChecker : MonoBehaviour
	{
		[SerializeField] private bool _isTouchingWall = false;
		public bool IsTouchingWall => _isTouchingWall;

		[SerializeField] Collider2D _leftCollider;
		[SerializeField] Collider2D _rightCollider;

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if(collider.gameObject.CompareTag(Tags.Platform)
			|| collider.gameObject.CompareTag(Tags.Wall))
			{
				_isTouchingWall = true;
			}
		}

		private void OnTriggerExit2D(Collider2D collider)
		{
			if(collider.gameObject.CompareTag(Tags.Platform)
			|| collider.gameObject.CompareTag(Tags.Wall))
			{
				_isTouchingWall = false;
			}
		}

		public void LeftColliderOn()
		{
			_leftCollider.enabled = true;
			_rightCollider.enabled = false;
		}

		public void RightColliderOn()
		{
			_rightCollider.enabled = true;
			_leftCollider.enabled = false;
		}
		public void SwapCollider()
		{
			if (_rightCollider.enabled)
			{
				LeftColliderOn();
			}
			else
			{
				RightColliderOn();
			}
		}
	}
}
