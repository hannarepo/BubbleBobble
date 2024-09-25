using UnityEngine;

namespace BubbleBobble
{
    public class FireBubble : Bubble
    {
        protected override BubbleType Type
        {
            get { return BubbleType.Fire; }
        }
    }
}
