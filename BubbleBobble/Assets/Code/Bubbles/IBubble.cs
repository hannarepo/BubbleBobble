using UnityEngine;

namespace BubbleBobble
{
    public interface IBubble
    {   
        // [SerializeField] private bool _canPop;
        // private GameManager _gameManager;

        void Awake()
        {

        }

        // Check if the player has collided with the bubble
        void OnCollisionEnter2D(Collision2D collision)
        {
            /* if (collision.gameObject.CompareTag("Player") && _canPop)
            {
                PopBubble();
            } */
        }

        // Pop the bubble and call the GameManager to handle the bubble type (if needed)
        void PopBubble()
        {

        }
    }
}
