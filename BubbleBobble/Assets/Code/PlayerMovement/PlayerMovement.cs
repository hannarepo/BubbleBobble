/// <remarks>
/// auhtor: Hanna Repo
/// </remarks>
/// 
/// <summary>
/// Player movement script.
/// Moves the player based on the input from the InputReader script.
/// </summary>

using UnityEngine;

namespace BubbleBobble
{
    [RequireComponent(typeof(InputReader))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        private InputReader _inputReader;
        private Rigidbody2D _rb;
        private Vector2 _movement;
        [SerializeField] private float _speed = 5f;

        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector2 movement = _inputReader.Movement;
            Vector2 velocity = _rb.velocity;
            velocity.x = movement.x * _speed;
            _rb.velocity = velocity;
        }
    }
}
