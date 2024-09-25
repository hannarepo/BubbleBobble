using UnityEngine;

namespace BubbleBobble
{
    public interface IBubble
    {   
        // Check if the player has collided with the bubble
        void OnCollisionEnter2D(Collision2D collision);

        // Pop the bubble and call the GameManager to handle the bubble type (if needed)
        void PopBubble();
    }
}
