using UnityEngine;

namespace BubbleBobble
{
	public class GroundFireDestroy : MonoBehaviour
	{
		[SerializeField] private int _timeBeforeDestroy = 1;

		// TODO: Destroy fire if level is changing.
		private void Start()
		{
			Destroy(gameObject, _timeBeforeDestroy);
		}
	}
}
