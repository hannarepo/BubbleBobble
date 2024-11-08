/*
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BubbleBobble
{
	public class Bounceable : MonoBehaviour
	{
		//[SerializeField] private GameObject _enemy;
		[SerializeField] private Vector2 _normal;
		private TilemapRenderer _renderer;
		public List<GameObject> _enemyList = new List<GameObject>();
		public Vector2 Normal { get { return _normal; } }

		private void Awake()
		{
			_renderer = GetComponentInParent<TilemapRenderer>();
		}

		private void Update()
		{
			for (int i = _enemyList.Count - 1; i >= 0; i--)
			{
				if (CollisionCheck(_enemyList[i].GetComponent<BouncingEnemyMovement>()))
				{
					//Debug.Log("Collision (BOMBA!)");
				}
			}
		}
		// Todo Vaihda Boundsit
		// ja rename physics
		private bool CollisionCheck(BouncingEnemyMovement enemy)
		{
			Bounds bounds = _renderer.bounds;
			Physics.Hit hit = Physics.Intersects(bounds, enemy.transform.position);
			if (hit == null)
			{
				return false;
			}

			//enemy.Bounce(hit.Normal);
			return true;
		}
	}
}
*/