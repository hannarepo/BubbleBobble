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
        private float _timer = 0;

        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Vector2 down = new Vector2(transform.position.x, transform.position.y-1);
            _timer += Time.deltaTime;

            // TODO : Fix: Should not jump when dropping down from a platform
            //

            // Do a circle cast to check if the player is on the a platform that can be dropped down from
            if (Physics2D.CircleCast(transform.position, _circleCastRadius, down, _circleCastDistance, _dropDownLayer))
            {
                // If player is pressing down and jump, drop through the platform
                if (_inputReader.Movement.y < 0 && _inputReader.Jump)
                {
                    DropDown();
                }
                else if (_timer > 0.4f)
                {
                    Physics2D.IgnoreLayerCollision(3, 8, false);
                }
            }

            // Do a circle cast to check if the player is on the ground and can jump
            if (Physics2D.CircleCast(transform.position, _circleCastRadius, down, _circleCastDistance, _jumpCheckLayers))
            {
                if (_inputReader.Jump)
                {
                    PlayerJump();
                }
            }
        }

        private void PlayerJump()
        {
            _rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        }

        private void DropDown()
        {
            Debug.Log("drop down");
            Physics2D.IgnoreLayerCollision(3, 8, true);
            _timer = 0;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y-1f), _circleCastRadius);
        }
    }
}
