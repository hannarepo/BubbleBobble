using UnityEngine;

namespace BubbleBobble
{
    public class Bubble : MonoBehaviour
    {
        [SerializeField] private bool _canPop = true;
        private GameManager _gameManager;
        [SerializeField] private string _type;

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && _canPop)
            {
                if (_type == "Bomb")
                {
                    Explode();
                }
                else
                {
                    PopBubble();
                }
            }
        }

        private void PopBubble()
        {
            _gameManager.BubblePopped(_type);
            // TODO: Pop sound and animation
            Destroy(gameObject);
        }

        private void Explode()
        {
            Debug.Log("Boom!");
            Destroy(gameObject);
        }
    }
}
