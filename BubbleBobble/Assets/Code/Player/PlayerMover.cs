/// <remarks>
/// author: Hanna Repo
/// </remarks>
/// 
/// <summary>
/// Player movement script.
/// Moves the player based on the input from the InputReader script.
/// </summary>

using UnityEngine;

namespace BubbleBobble
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 movement)
        {
            Vector2 velocity = _rb.velocity;
            velocity.x = movement.x * _speed;
            _rb.velocity = velocity;   
        }
    }
}
