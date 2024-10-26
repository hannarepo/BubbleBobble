using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class Bounceable : MonoBehaviour
    {
        
        [SerializeField] private GameObject _enemy;
        private SpriteRenderer _renderer;        

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (CollisionCheck(_enemy.GetComponent<BouncingEnemyMovement>()))
            {
                Debug.Log("Collision (BOMBA!)");
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
