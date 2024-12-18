using UnityEngine;

namespace MemoBubble
{
	public class DelayedDestruction : MonoBehaviour
	{
		[SerializeField] private float _timeBeforeDestroy = 1f;
		private void Start()
		{
			Destroy(gameObject, _timeBeforeDestroy);
		}
	}
}
