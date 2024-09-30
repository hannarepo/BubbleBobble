using UnityEngine;

namespace BubbleBobble
{
    public class BombBubble : Bubble
    {
        protected override BubbleType Type
        {
            get { return BubbleType.Bomb; }
        }
        public override void PopBubble()
        {
            // Explosion animation/sprite flash
            _gameManager.BubblePopped(Type);
            Destroy(gameObject);
        }
    }
}
