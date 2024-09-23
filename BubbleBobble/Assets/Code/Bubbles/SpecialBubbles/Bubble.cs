using UnityEngine;

namespace BubbleBobble
{
    public class Bubble : MonoBehaviour
    {
        [SerializeField] private bool _canPop = true;
        private GameManager _gameManager;
        //private BubbleSpawner _bubbleSpawner;
        //private Rigidbody2D _rigidBody;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            //SetGravityScale();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && _canPop)
            {
                PopBubble();
            }
        }

        private void PopBubble()
        {
            //_gameManager.BubblePopped(_type);
            // TODO: Pop sound and animation
            Destroy(gameObject);
        }
        /* private void SetGravityScale()
        {
            BubbleSpawner _bubbleSpawner = FindObjectOfType<BubbleSpawner>();
            if (_bubbleSpawner.SpawnFromTop)
            {
                gameObject.GetComponent<Rigidbody2D>().gravityScale *= -1;
            }
        } */
    }
}
