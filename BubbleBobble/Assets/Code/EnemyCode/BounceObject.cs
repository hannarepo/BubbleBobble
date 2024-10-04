using UnityEngine;

namespace BubbleBobble
{
    public class BounceObject : MonoBehaviour
	{
		[SerializeField] private Vector2 _normal;

		private void Start()
		{
			_normal.Normalize();
		}

		public Vector2 Normal { get { return _normal; } }
	}
}
