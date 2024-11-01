using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class BouncingEnemyMovement : MonoBehaviour
    {

        [SerializeField] private float _speed = 5;
        private Transform _transform;
        private Vector2 _velocity = Vector2.zero;

        // Start is called before the first frame update
        void Start()
        {
            _transform = transform;
            _velocity = (Vector2.left + Vector2.up * 1.5f).normalized;
        }

        // Update is called once per frame
        void Update()
        {
            _transform.position += new Vector3(_velocity.x, _velocity.y, 0) * _speed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            BounceObject _object = other.GetComponent<BounceObject>();
            if (_object == null)
            {
                return;
            }

            Vector2 normal = _object.Normal;
            Bounce(normal);
        }

        public void Bounce(Vector2 normal)
        {
            Vector2 u = Vector2.Dot(_velocity, normal) * normal;
            Vector2 w = _velocity - u;
            _velocity = w - u;
        }
    }

}
