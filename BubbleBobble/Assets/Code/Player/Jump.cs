/// <remarks>
/// author: Hanna Repo
/// </remarks>
///
/// <summary>
/// Player jump script
/// Checks if the player is on the ground and if the player presses the jump button
/// </summary>

using Unity.VisualScripting;
using UnityEngine;

namespace BubbleBobble
{
    public class Jump : MonoBehaviour
    {
        private InputReader _inputReader;
        private Rigidbody2D _rb;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private LayerMask _jumpCheckLayers;
        [SerializeField] private LayerMask _dropDownLayer;
        [SerializeField] private float _circleCastRadius = 0.5f;
        [SerializeField] private float _circleCastDistance = 3f;
        private bool _canJump = false;
        private float _timer = 0;

        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Physics2D.CircleCast(new Vector2(transform.position.x, transform.position.y-1f), _circleCastRadius,
                new Vector2(transform.position.x, transform.position.y - 1), _circleCastDistance, _dropDownLayer))
            {
                if (_inputReader.Movement.y < 0 && _inputReader.Jump)
                {
                    Debug.Log("drop down");
                    Physics2D.IgnoreLayerCollision(3, 8, true);
                }
            }

            // Do a circle cast to check if the player is on the ground and set the _canJump variable accordingly
            if (Physics2D.CircleCast(new Vector2(transform.position.x, transform.position.y-1f), _circleCastRadius,
                new Vector2(transform.position.x, transform.position.y - 1), _circleCastDistance, _jumpCheckLayers))
            {
                _canJump = true;
            }
            else
            {
                _canJump = false;
            }

            if (_inputReader.Jump && _canJump)
            {
                _rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y-1f), _circleCastRadius);
        }
    }
}
