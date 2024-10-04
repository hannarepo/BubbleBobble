using UnityEngine;

namespace BubbleBobble
{
    public abstract partial class Bubble : MonoBehaviour, IBubble
    {
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

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Bubble") && _gameManager.HasPopped)
            {
                PopBubble();
                _gameManager.HasPopped = true;
            }

            if (collision.gameObject.CompareTag("Player") && _canPop)
            {
                PopBubble();
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("PlayerFeet"))
            {
                CanPop(false);
            }
        }

        public virtual void PopBubble()
        {
            _gameManager.BubblePopped(Type);
            Destroy(gameObject);
        }

        public virtual void CanPop(bool canPop)
        {
            _canPop = canPop;
        }
        
    }
}
