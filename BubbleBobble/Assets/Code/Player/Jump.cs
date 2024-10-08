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
        [SerializeField] private float _bubbleJumpForce = 1f;
        [SerializeField] private LayerMask _jumpCheckLayers;
        [SerializeField] private LayerMask _dropDownLayer;
        [SerializeField] private Vector2 _boxCastSize;
        [SerializeField] private float _boxCastDistance = 0.3f;
        [SerializeField] private Transform _groundCheckTarget;
        private float _timer = 0;
        private PlatformEffector2D _platformEffector;
        private bool _canDropDown;

        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            // Do a CircleCast and save the resulting collider into a variable for ground check
            RaycastHit2D hit = Physics2D.BoxCast(_groundCheckTarget.position, _boxCastSize, 0, new Vector2(_groundCheckTarget.position.x, _groundCheckTarget.position.y -0.4f),
                                                    _boxCastDistance, _jumpCheckLayers);

            if (hit.collider == null)
            {
                return;
            }

            // If platform effector is not null and player is pressing down and jump,
            // turn the platform effector 180 degrees so that player can pass down through the platform
            if (_platformEffector != null)
            {
                if (_inputReader.Movement.y < 0 && _inputReader.Jump && _canDropDown)
                {
                    _platformEffector.rotationalOffset = 180;
                }
                else if (!_canDropDown)
                {
                    _platformEffector.rotationalOffset = 0;
                }
            }

            // If the collider hit with CircleCast is either Ground, Platform or DropDownPlatform
            //  and player is not pressing down, player can jump
            if (hit.collider.CompareTag("Ground") ||
                hit.collider.CompareTag("Platform"))
            {
                if (_inputReader.Movement.y >= 0 && _inputReader.Jump)
                {
                    PlayerJump();
                }
            }

            // If collider hit with CircleCast is a bubble and player is holding down jump button,
            // do a bubble jump with less jump force due to bubble having bounciness
            if (hit.collider.CompareTag("Projectile") || hit.collider.CompareTag("Bubble"))
            {
                Bubble bubble = hit.collider.GetComponent<Bubble>();
                if (_inputReader.JumpOnBubble)
                {
                    bubble.CanPop(false);
                    BubbleJump();
                }
                else
                {
                    bubble.CanPop(true);
                }
            }
        }

        private void PlayerJump()
        {
            _rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        }

        private void BubbleJump()
        {
            _rb.AddForce(transform.up * _bubbleJumpForce, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Platform"))
            {
                _platformEffector = other.gameObject.GetComponent<PlatformEffector2D>();
            }

            if (_platformEffector != null)
            {
                _canDropDown = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Platform"))
            {
                _platformEffector = other.gameObject.GetComponent<PlatformEffector2D>();
            }

            if (_platformEffector != null)
            {
                _canDropDown = false;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            if (_groundCheckTarget == null)
            {
                return;
            }
    
            Gizmos.DrawWireCube(_groundCheckTarget.position - new Vector3(0, _boxCastDistance, 0), _boxCastSize);
        }
    }
}
