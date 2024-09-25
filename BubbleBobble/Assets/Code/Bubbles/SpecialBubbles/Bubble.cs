using UnityEngine;

namespace BubbleBobble
{
    public abstract class Bubble : MonoBehaviour, IBubble
    {
        public enum BubbleType
        {
            Fire,
            Bomb,
            Projectile
        }
        private bool _canPop = false;
        protected GameManager _gameManager;
        protected abstract BubbleType Type
        {
            get;
        }

        protected virtual void Awake()
        {
        }

        protected virtual void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            if (CanPop())
            {
                PopBubble();
            }
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Bubble") && _gameManager.HasPopped)
            {
                PopBubble();
                _gameManager.HasPopped = true;
            }

            if (collision.gameObject.CompareTag("Player") && CanPop())
            {
                PopBubble();
            }
        }

        public virtual void PopBubble()
        {
            _gameManager.BubblePopped(Type);
            Destroy(gameObject);
        }

        public virtual bool CanPop()
        {
            return _canPop;
        }
        
    }
}
