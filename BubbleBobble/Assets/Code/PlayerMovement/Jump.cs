/// <remarks>
/// auhtor: Hanna Repo
/// </remarks>
///
/// <summary>
/// Player jump script
/// Checks if the player is on the ground and if the player presses the jump button
/// </summary>

using UnityEngine;

namespace BubbleBobble
{
    public class Jump : MonoBehaviour
    {
        private InputReader _inputReader;
        private Rigidbody2D _rb;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _cisrcleCastRadius = 0.5f;
        [SerializeField] private float _circleCastDistance = 3f;
        private bool _canJump = false;

        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // Do a circle cast to check if the player is on the ground and set the _canJump variable accordingly
            if (Physics2D.CircleCast(new Vector2(transform.position.x, transform.position.y-0.5f), _cisrcleCastRadius,
                new Vector2(transform.position.x, transform.position.y - 1), _circleCastDistance, _groundLayer))
            {
                _canJump = true;
            }
            else
            {
                _canJump = false;
            }

            Debug.Log(_canJump);
            if (_inputReader.Jump && _canJump)
            {
                Debug.Log("Jump");
                _rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y-0.5f), _cisrcleCastRadius);
        }
    }
}
