using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class ProjectileBubble : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Vector2 _originalPosition;
        [SerializeField] private float _range;
        private float _targetX;
        [SerializeField] private float _speed;
        private Vector2 _direction;
        [SerializeField] private float _floatGravityScale;

        public Vector2 LaunchDirection
        {
            get { return _direction; }
            set  { _direction = value; }
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _originalPosition = transform.position;
            _targetX = _originalPosition.x + _range;
        }

        private void Update()
        {

            if (transform.position.x <= _targetX)
            {
                transform.position += new Vector3(_direction.x +1, _direction.y, 0) * _speed * Time.deltaTime;
            }
            else
            {
                _rb.gravityScale = _floatGravityScale;
            }
        }
    }
}
