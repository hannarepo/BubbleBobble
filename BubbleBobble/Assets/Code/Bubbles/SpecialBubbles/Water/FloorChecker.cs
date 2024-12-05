using Unity.VisualScripting;
using UnityEngine;

namespace BubbleBobble
{
	public class FloorChecker : MonoBehaviour
	{
		[SerializeField] private bool _isTouchingFloor = false;
		public bool IsTouchingFloor
		{
			get { return _isTouchingFloor; }
			set { _isTouchingFloor = value; }
		}

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if(collider.gameObject.CompareTag(Tags.Platform)
			|| collider.gameObject.CompareTag(Tags.Ground))
			{
				_isTouchingFloor = true;
			}
		}

		private void OnTriggerExit2D(Collider2D collider)
		{
			if(collider.gameObject.CompareTag(Tags.Platform)
			|| collider.gameObject.CompareTag(Tags.Ground))
			{
				_isTouchingFloor = false;
			}
		}
	}
}
