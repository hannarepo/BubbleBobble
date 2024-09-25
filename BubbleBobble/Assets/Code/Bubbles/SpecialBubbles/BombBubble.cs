using UnityEngine;

namespace BubbleBobble
{
    public class BombBubble : MonoBehaviour, IBubble
    {
        [SerializeField] private bool _canPop = true;
        private GameManager _gameManager;
        private string _type = "Bomb";

        void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && _canPop)
            {
                PopBubble();
            }
        }

        void PopBubble()
        {
            // Missing: Explosion animation and sound
            _gameManager.BubblePopped(_type);
            Destroy(gameObject);
        }
    }
}
