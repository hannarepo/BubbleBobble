using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class Bounceable : MonoBehaviour
    {
        
        //[SerializeField] private GameObject _enemy;
        private SpriteRenderer _renderer;
		public List<GameObject> _enemyList = new List<GameObject>();

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
			for (int i = _enemyList.Count - 1; i >= 0; i--)
            {
                if (CollisionCheck(_enemyList[i].GetComponent<BouncingEnemyMovement>()))
            	{
                	Debug.Log("Collision (BOMBA!)");
            	}
            }

            
        }

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
