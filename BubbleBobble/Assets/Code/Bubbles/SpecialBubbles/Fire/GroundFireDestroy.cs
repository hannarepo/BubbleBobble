using UnityEngine;

namespace BubbleBobble
{
	public class GroundFireDestroy : MonoBehaviour
	{
		[SerializeField] private int _timeBeforeDestroy = 1;

		// TODO: Destroy fire if level is changing.
		private void Start()
		{
			Invoke("DestroyFire", _timeBeforeDestroy);
		}

		private void DestroyFire()
		{
			Destroy(gameObject);
		}
	}
}
