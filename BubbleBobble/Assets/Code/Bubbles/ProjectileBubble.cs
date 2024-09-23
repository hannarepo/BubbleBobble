/// <remarks>
/// author: Hanna Repo
/// </remarks>
///
/// <summary>
/// After instantiating projectile bubble, check in which direction bubble
/// should be moving. Move bubble in correct direction for range distance 
/// after which set gravity scale to negative so that bubble floats up.
/// When bubble reaches the top of the level, destroy bubble after a short delay.
/// </summary>

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
            // Check which way player is facing from PlayerControl
            // and calculate target x position accordingly.
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
            // If player is facing right, move bubble right on x-axis.
            // If player is facing left, move bubble left on x-axis.
            // When bubble reaches target x position, set gravity scale to negative
            // so that bubble floats up.
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                _speed = 0;
                _rb.gravityScale = _floatingGravityScale;
            }
        }
    }
}
