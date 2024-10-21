using UnityEngine;

namespace BubbleBobble
{
    public class FireBubble : Bubble
    {
        [SerializeField] private GameObject _fireBallPrefab;
        protected override BubbleType Type
        {
            get { return BubbleType.Fire; }
        }

        protected override void Awake()
        {
            CanPop(true);
        }

        private void OnDestroy()
        {

        }
    }
}
