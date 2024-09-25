using UnityEngine;

namespace BubbleBobble
{
    public class BombBubble : Bubble
    {
        protected override BubbleType Type
        {
            get { return BubbleType.Bomb; }
        }
    }
}
