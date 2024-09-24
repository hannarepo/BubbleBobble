using UnityEngine;

namespace BubbleBobble
{
    public class Bubble : MonoBehaviour
    {
        [SerializeField] private bool _canPop = true;
        private GameManager _gameManager;

        private void Awake()
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

        private void PopBubble()
        {
            // TODO: Pop sound and animation
            Destroy(gameObject);
        }
    }
}
