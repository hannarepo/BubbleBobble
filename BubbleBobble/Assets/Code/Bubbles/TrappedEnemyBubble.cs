using UnityEngine;

namespace BubbleBobble
{
    public class TrappedEnemyBubble : Bubble
    {
        private Transform _transform;
        private Rigidbody2D _rb;
        private float _timer;
        [SerializeField] private float _timeLimit = 10f;
        [SerializeField] private float _floatingGravityScale = -0.5f;
        private GameObject _enemy;

        public GameObject Enemy
        {
            get { return _enemy; }
            set { _enemy = value; }
        }

        protected override void Awake()
        {
            _transform = transform;
            CanPop(true);
            _rb = GetComponent<Rigidbody2D>();
            _rb.gravityScale = _floatingGravityScale;
        }

        protected override void Start()
        {
            base.Start();
            _enemy.SetActive(false);

        }

        protected override BubbleType Type
        {
            get { return BubbleType.TrappedEnemy; }
        }

        private void Update()
        {
            //print(_enemy);
            _timer += Time.deltaTime;

            if (_timer >= _timeLimit)
            {
                _enemy.SetActive(true);
                _enemy.transform.position = _transform.position;
                Destroy(gameObject);
            }
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PopBubble();
                _enemy.GetComponent<EnemyTestScript>().Die();
            }
        }
    }
}
