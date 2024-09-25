using UnityEngine;

namespace BubbleBobble
{
    public class FireBubble : MonoBehaviour, IBubble
    {
        [SerializeField] private bool _canPop = true;
        private GameManager _gameManager;
        private string _type = "Fire";

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
            _gameManager.BubblePopped(_type);
            Destroy(gameObject);
        }
    }
}
