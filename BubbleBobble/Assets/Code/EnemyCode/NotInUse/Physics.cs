using UnityEngine;

namespace BubbleBobble
{
	public static class Physics
	{
		public class Hit 
		{
			// The collision point
			public Vector2 Point;
			// The normal of the surface where the collision happened
			public Vector2 Normal;
			// How far into the surface the collided object got.
			// This is useful for calculating the reflection 
			public Vector2 Delta;
		}

		public static Hit Intersects(Bounds AABB, Vector2 point) 
		{
			float deltaX = point.x - AABB.center.x;
			float pointX = AABB.extents.x - Mathf.Abs(deltaX);
			
			if (pointX < 0)
			{
				// No collision
				return null;
			}

			float deltaY = point.y - AABB.center.y;
			float pointY = AABB.extents.y - Mathf.Abs(deltaY);
			
			if (pointY < 0)
			{
				// No collision
				return null;
			}

			Hit hit = new Hit();
			
			if (pointX < pointY) 
			{
				float signX = Mathf.Sign(deltaX);
				hit.Normal = new Vector2(signX, 0);
				hit.Delta = new Vector2(pointX * signX, 0);
				hit.Point = new Vector2(AABB.center.x + (AABB.extents.x * signX), point.y);
			}
			else 
			{
				float signY = Mathf.Sign(deltaY);
				hit.Normal = new Vector2(0, signY);
				hit.Delta = new Vector2(0, pointY * signY);
				hit.Point = new Vector2(point.x, AABB.center.y + (AABB.extents.y * signY));
			}

			return hit;
		}
	}
}