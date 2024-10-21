/// <remarks>
/// author: Hanna Repo
/// </remarks>
///
/// <summary>
/// Player jump script
/// Checks if the player is on the ground or a platform and if the player presses the jump button.
/// If player is on a platform, player can drop down through the platform.
/// Player can jump on bubbles to bounce higher, but only once per bubble.
/// </summary>

using UnityEngine;

namespace BubbleBobble
{
	public class Jump : MonoBehaviour
	{
		private InputReader _inputReader;
		private Rigidbody2D _rb;
		[SerializeField] private float _jumpForce = 5f;
		[SerializeField] private float _defaultGravityScale = 3f;
		[SerializeField] private float _jumpGravityScale = 1.5f;
		[SerializeField] private float _dropDownGravityScale = 4f;
		[SerializeField] private float _bubbleJumpForce = 8f;
		[SerializeField] private LayerMask _jumpCheckLayers;
		[SerializeField] private Vector2 _boxCastSize;
		[SerializeField] private float _boxCastDistance = 0.3f;
		[SerializeField] private Transform _groundCheckTarget;
		private PlatformEffector2D _platformEffector;
		private bool _canDropDown = false;
		private bool _jumping = false;
		private bool _grounded = false;
		private bool _falling = false;
		private int _bubbleJumpCounter = 0;

		// Properties for animation controller to check if player is jumping, grounded or falling.
		public bool Jumping => _jumping;
		public bool Grounded => _grounded;
		public bool Falling => _falling;

		private void Awake()
		{
			_inputReader = GetComponent<InputReader>();
			_rb = GetComponent<Rigidbody2D>();
			_rb.gravityScale = _defaultGravityScale;
		}

		private void Update()
		{
			

			// print(hit.collider);

			// If player is moving down (falling), change gravity scale higher so player drops faster.
			if (_rb.velocity.y < -0.1f)
			{
				_rb.gravityScale = _dropDownGravityScale;
				_jumping = false;
				_falling = true;
				_grounded = false;
			}
			// If player is moving up (jumping), change gravity scale lower so player jumps faster.
			else if (_rb.velocity.y > 0)
			{
				_rb.gravityScale = _jumpGravityScale;
				_grounded = false;
				_jumping = true;
				_falling = false;
			}
			// If player is not moving up or down, set gravity scale to default.
			else
			{
				_rb.gravityScale = _defaultGravityScale;
				_jumping = false;
				_falling = false;
			}

			// print(_rb.velocity.y);

			// If platform effector is not null and player is pressing down and jump,
			// turn the platform effector 180 degrees so that player can pass down through the platform.
			if (_platformEffector != null)
			{
				if (_inputReader.Movement.y < 0 && _inputReader.Jump && _canDropDown)
				{
					_platformEffector.rotationalOffset = 180;
					_rb.gravityScale = _dropDownGravityScale;
					_falling = true;
				}
				else if (!_canDropDown)
				{
					_platformEffector.rotationalOffset = 0;
					_rb.gravityScale = _defaultGravityScale;
				}
			}

			// Do a BoxCast and save the resulting collider into a variable for ground check
			RaycastHit2D hit = Physics2D.BoxCast(_groundCheckTarget.position, _boxCastSize, 0, Vector2.down,
												_boxCastDistance, _jumpCheckLayers);

			if (hit.collider == null)
			{
				return;
			}

			// If the collider hit with BoxCast is Ground or Platform
			//  and player is not pressing down, player can jump.
			if (hit.collider.CompareTag("Ground") ||
				hit.collider.CompareTag("Platform"))
			{
				_grounded = true;

				if (_inputReader.Movement.y >= 0 && _inputReader.Jump)
				{
					GroundJump();
				}
			}

			// If collider hit with BoxCast is a bubble and player is holding down jump button.
			// Bubble jump counter needs to be under 2, so that bubbles can be jumped on only once before popping.
			// Do a bubble jump with less jump force due to bubble having bounciness.
			if (hit.collider.CompareTag("Projectile") || hit.collider.CompareTag("Bubble"))
			{
				Bubble bubble = hit.collider.GetComponent<Bubble>();
				if (bubble != null)
				{
					if (_inputReader.JumpOnBubble && _bubbleJumpCounter < 2)
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
			else
			{
				_bubbleJumpCounter = 0;
			}
		}

		private void GroundJump()
		{
			_rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
		}

		private void BubbleJump()
		{
			_rb.AddForce(transform.up * _bubbleJumpForce, ForceMode2D.Impulse);
			_bubbleJumpCounter++;
		}

		// Only set platform effector if player is on top of the platform to avoid null reference exceptions.
		// If the platform effector is not null, player can drop down through the platform.
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
			// Draw a wire cube to visualize the BoxCast.
			// WireCube center is the player position - the box cast distance.
			Gizmos.DrawWireCube(_groundCheckTarget.position - new Vector3(0, _boxCastDistance, 0), _boxCastSize);
		}
	}
}
