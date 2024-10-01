using UnityEngine;

namespace BubbleBobble
{
    public class TrappedEnemyBubble : Bubble
    {
        private Transform _transform;
        private float _timer;
        [SerializeField] private float _timeLimit = 10f;
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
                print("times up");
                _enemy.SetActive(true);
                _enemy.transform.position = _transform.position;
            }
        }
    }
}
