using Unity.VisualScripting;
using UnityEngine;

namespace BubbleBobble
{
    public class ShootBubble : MonoBehaviour
    {
        private GameObject _bubble;
        [SerializeField] private ProjectileBubble _projectileBubble;
        private int _projectileCount;

        public int ProjectileCount => _projectileCount;

        private void Awake()
        {
            _bubble = Resources.Load("Prefabs/Bubbles/ProjectileBubble") as GameObject;
        }

        public void Shoot(bool shoot, Vector2 direction, bool lookingRight)
        {
            if (shoot)
            {
                if (lookingRight)
                {
                    Instantiate(_bubble, new Vector3(transform.position.x -0.8f, transform.position.y, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(_bubble, new Vector3(transform.position.x +0.8f, transform.position.y, 0), Quaternion.identity);
                }
                
                _projectileBubble.LaunchDirection = direction;
                _projectileCount ++;
            }
        }
    }
}
