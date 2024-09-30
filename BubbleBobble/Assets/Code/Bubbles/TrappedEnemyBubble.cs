using UnityEngine;

namespace BubbleBobble
{
    public class TrappedEnemyBubble : Bubble
    {
        private Transform _transform;

        protected override void Awake()
        {
            _transform = transform;
        }

        protected override BubbleType Type
        {
            get { return BubbleType.TrappedEnemy; }
        }

        private void Update()
        {
            CanPop(true);
        }
    }
}
