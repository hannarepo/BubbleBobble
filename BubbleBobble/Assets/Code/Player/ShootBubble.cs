using UnityEngine;

namespace BubbleBobble
{
    public class ShootBubble : MonoBehaviour
    {
        private GameObject _bubble;
        [SerializeField] private ProjectileBubble _projectileBubble;

        private void Awake()
        {
            _bubble = Resources.Load("Prefabs/Bubbles/ProjectileBubble") as GameObject;
        }

        public void Shoot(bool shoot, Vector2 direction)
        {
            if (shoot)
            {
                Instantiate(_bubble, transform.position, Quaternion.identity);
                _projectileBubble.LaunchDirection = direction;
            }
        }
    }
}
