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
        private Collider2D _playerCollider;
        private Collider2D _groundCollider;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _bubbleJumpForce = 1f;
        [SerializeField] private LayerMask _jumpCheckLayers;
        [SerializeField] private LayerMask _dropDownLayer;
        [SerializeField] private float _circleCastRadius = 0.5f;
        [SerializeField] private float _circleCastDistance = 0.5f;
        [SerializeField] private Transform _groundCheckTarget;
        private float _timer = 0;

        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
            _rb = GetComponent<Rigidbody2D>();
            _playerCollider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            // Do a CircleCast and save the resulting collider into a variable for ground check
            RaycastHit2D hit = Physics2D.CircleCast(_groundCheckTarget.position, _circleCastRadius, Vector2.down,
                                                    _circleCastDistance, _jumpCheckLayers);

            _groundCollider = hit.collider;

            if (hit.collider == null)
            {
                return;
            }

            // If the collider hit with CircleCast is DropDownPlatform 
            // and player is pressing down and jump, drop through the platform
            if (_groundCollider.CompareTag("DropDownPlatform"))
            {
                if (_inputReader.Movement.y < 0 && _inputReader.Jump)
                {
                    DropDown();
                }
                else if (_timer > 0.3f)
                {
                    Physics2D.IgnoreCollision(_playerCollider, _groundCollider, false);
                }
            }

            // If the collider hit with CircleCast is either Ground, Platform or DropDownPlatform
            //  and player is not pressing down, player can jump
            if (hit.collider.CompareTag("Ground") ||
                hit.collider.CompareTag("Platform") ||
                hit.collider.CompareTag("DropDownPlatform"))
            {
                if (_inputReader.Movement.y >= 0 && _inputReader.Jump)
                {
                    PlayerJump();
                }
            }

            // If collider hit with CircleCast is a bubble and player is holding down jump button,
            // do a bubble jump with less jump forve due to bubble having bounciness
            if (hit.collider.CompareTag("Bubble"))
            {
                if (_inputReader.JumpOnBubble)
                {
                    BubbleJump();
                }
            }
        }

        private void PlayerJump()
        {
            _rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        }

        private void DropDown()
        {
            Physics2D.IgnoreCollision(_playerCollider, _groundCollider, true);
            _timer = 0;
        }

        private void BubbleJump()
        {
            _rb.AddForce(transform.up * _bubbleJumpForce, ForceMode2D.Impulse);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            if (_groundCheckTarget == null)
            {
                return;
            }
    
            Gizmos.DrawWireSphere(_groundCheckTarget.position + new Vector3(0, _circleCastDistance, 0), _circleCastRadius);
        }
    }
}
