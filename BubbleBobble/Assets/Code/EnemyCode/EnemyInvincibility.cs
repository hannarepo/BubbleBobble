using UnityEngine;

namespace BubbleBobble
{
    public class EnemyInvincibility : MonoBehaviour
    {
        private bool _isInvincible = false;

        public bool IsInvincible { get { return _isInvincible; } }

        public void FreeEnemy()
        {
            _isInvincible = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(Tags._platform) || collision.gameObject.CompareTag(Tags._ground))
            {
                _isInvincible = false;
            }
        }
    }
}
