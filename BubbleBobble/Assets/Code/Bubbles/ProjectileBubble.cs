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
        [SerializeField] private float _floatingGravityScale;
        private bool _shootRight;

        public Vector2 LaunchDirection
        {
            get { return _direction; }
            set { _direction = value; }
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _originalPosition = transform.position;
        }

        private void Start()
        {
            _shootRight = FindObjectOfType<PlayerControl>().LookingRight;

            if (_shootRight)
            {
                _targetX = _originalPosition.x - _range;
            }
            else
            {
                _targetX = _originalPosition.x + _range;
            }
        }

        private void Update()
        {
            if (_shootRight)
            {
                if (transform.position.x >= _targetX)
                {
                    transform.position += new Vector3(_direction.x - 1, _direction.y, 0) * _speed * Time.deltaTime;
                }
                else
                {
                    _rb.gravityScale = _floatingGravityScale;
                }
            }
            else
            {
                if (transform.position.x <= _targetX)
                {
                    transform.position += new Vector3(_direction.x + 1, _direction.y, 0) * _speed * Time.deltaTime;
                }
                else
                {
                    _rb.gravityScale = _floatingGravityScale;
                }
            }

        }
    }
}
