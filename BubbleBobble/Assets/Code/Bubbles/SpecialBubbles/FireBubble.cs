using System;
using UnityEngine;

namespace BubbleBobble
{
    public class FireBubble : Bubble
    {
        [SerializeField] private GameObject _fireBallPrefab;
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private bool _moveLeft = false;
        public bool MoveLeft { set { _moveLeft = value; }}
        protected override BubbleType Type
        {
            get { return BubbleType.Fire; }
        }

        protected override void Awake()
        {
            CanPop(true);
        }

        protected override void Start()
        {
            base.Start();
            if (_moveLeft)
            {
                ChangeXDirection();
            }
        }

        private void Update()
        {
            if (_canMoveBubble)
            {
                BubbleMovement();
            }
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
        }
        private void ChangeXDirection()
        {
            _moveSpeed *= -1;
        }

        private void BubbleMovement()
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * _moveSpeed, ForceMode2D.Force);
        }

        public override void PopBubble()
        {
            base.PopBubble();
            Instantiate(_fireBallPrefab, gameObject.transform.position, Quaternion.identity);
        }
    }
}
