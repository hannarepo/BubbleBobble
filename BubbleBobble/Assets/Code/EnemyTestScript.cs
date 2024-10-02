using UnityEngine;

namespace BubbleBobble
{
    public class EnemyTestScript : MonoBehaviour
    {
        private GameManager _gameManager;

        // Find the Game Manager and add this enemy object to the list of enemies
        void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _gameManager.AddEnemyToList(gameObject);
        }

        // Add enemy death events here
        public void Die()
        {
            Destroy(gameObject);
        }

        // Remove enemyobject from the gamemanager list when destroyed.
        private void OnDestroy()
        {
            _gameManager._enemyList.Remove(gameObject);
        }
    }
}
