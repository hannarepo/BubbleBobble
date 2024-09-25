using UnityEngine;

namespace BubbleBobble
{
    public abstract class Bubble : MonoBehaviour, IBubble
    {
        public enum BubbleType
        {
            Fire,
            Bomb
        }
        [SerializeField] private bool _canPop = true;
        private GameManager _gameManager;
        protected abstract BubbleType Type
        {
            get;
        }

        protected virtual void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && _canPop)
            {
                PopBubble();
            }
        }

        public virtual void PopBubble()
        {
            _gameManager.BubblePopped(Type);
            Destroy(gameObject);
        }
        
    }
}
