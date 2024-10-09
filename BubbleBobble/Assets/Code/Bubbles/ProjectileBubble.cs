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
    public class ProjectileBubble : Bubble
    {
        private Rigidbody2D _rb;
        private Vector2 _originalPosition;
        [SerializeField] private float _range;
        [SerializeField] private float _speed;
        [SerializeField] private float _floatingGravityScale;
        [SerializeField] private float _launchForce = 5f;
        [SerializeField] private float _trapWindow = 3f;
        private float _targetX;
        private Vector2 _direction;
        private bool _shootRight;
        [SerializeField] GameObject _trappedEnemyPrefab;
        [SerializeField] private float _lifeTime = 10f;
        private float _timer = 0;

        public Vector2 LaunchDirection
        {
            get { return _direction; }
            set { _direction = value; }
        }

        protected override BubbleType Type
        {
            get { return BubbleType.Projectile; }
        }

        protected override void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _originalPosition = transform.position;
            CanPop(true);     
        }

        protected override void Start()
        {
            base.Start();

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
            _timer += Time.deltaTime;

            // If player is facing right, move bubble right on x-axis.
            // If player is facing left, move bubble left on x-axis.
            // When bubble reaches target x position, set gravity scale to negative
            // so that bubble floats up.
            if (_shootRight)
            {
                if (transform.position.x >= _targetX)
                {
                    _rb.AddForce(transform.right*-1 * _launchForce, ForceMode2D.Impulse);
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
                    _rb.AddForce(transform.right * _launchForce, ForceMode2D.Impulse);
                }
                else
                {
                    _rb.gravityScale = _floatingGravityScale;
                }
            }

            if (_timer >= _lifeTime)
            {
                Destroy(gameObject);
            }

            if (_timer >= _trapWindow)
            {
                Physics2D.IgnoreLayerCollision(12, 13, true);
                transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }
            else
            {
                Physics2D.IgnoreLayerCollision(12, 13, false);
            }
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);

            if (collision.gameObject.CompareTag("Wall"))
            {
                _speed = 0;
                _rb.gravityScale = _floatingGravityScale;
            }

            if (collision.gameObject.CompareTag("Enemy") && _timer < _trapWindow)
            {
                if (!collision.gameObject.GetComponent<EnemyInvincibility>().IsInvincible)
                {
                    GameObject enemy = collision.gameObject;
                    GameObject trappedEnemy = Instantiate(_trappedEnemyPrefab, transform.position, Quaternion.identity);

                    TrappedEnemyBubble trappedEnemyBubble = trappedEnemy.GetComponent<TrappedEnemyBubble>();

                    if (trappedEnemyBubble != null)
                    {
                        trappedEnemyBubble.Enemy = enemy;
                    }

                    Destroy(gameObject);
                }
            }
        }
    }
}
