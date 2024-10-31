using UnityEngine;

namespace BubbleBobble
{
    public class BounceObject : MonoBehaviour
	{
		[SerializeField] private Vector2 _normal;
		private SpriteRenderer _renderer;

		private void Start()
		{
			_normal.Normalize();
		}

		public Vector2 Normal { get { return _normal; } }

		private bool CollisionCheck(BouncingEnemyMovement enemy)
        {
            Bounds bounds = _renderer.bounds;
            Physics.Hit hit = Physics.Intersects(bounds, enemy.transform.position);
            if (hit == null)
            {
                return false;
            }


            enemy.Bounce(hit.Normal);
            return true;
        }
	}
}
