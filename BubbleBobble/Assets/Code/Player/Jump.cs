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
		[SerializeField] private Transform _upCheckTarget;
		[SerializeField] private float _upCheckDistance = 1f;
		private float _timer = 0f;
		private bool _jumping = false;
		private bool _grounded = false;
		private bool _falling = false;

		// Properties for animation controller to check if player is jumping, grounded or falling.
		public bool Jumping => _jumping;
		public bool Grounded => _grounded;
		public bool Falling => _falling;

		#region Unity Messages
		private void Awake()
		{
			_inputReader = GetComponent<InputReader>();
			_rb = GetComponent<Rigidbody2D>();
			_rb.gravityScale = _defaultGravityScale;
		}

		private void Update()
		{
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

			_timer += Time.deltaTime;

			JumpCheck();
			DropDownCheck();
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
			Gizmos.DrawWireCube(_upCheckTarget.position, new Vector2(0.7f, 0.7f));
		}
		#endregion

		#region Private Implementations

		private void JumpThroughPlatform()
		{
			RaycastHit2D hit = Physics2D.BoxCast(_upCheckTarget.position, new Vector2(0.7f, 0.7f), 0, Vector2.up, _upCheckDistance);
			print(hit.collider);

			if (hit.collider == null)
			{
				return;
			}

			if(hit.collider.CompareTag("Platform"))
			{
				Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), true);
			}
			else if (_timer > 1f)
			{
				Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), false);
			}
		}

		private void JumpCheck()
		{
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
		}

		private void DropDownCheck()
		{
			// If player is pressing down and jump, ignore layer collision between player and platform
			//so that player can drop through platform. Increase gravity scale so dropping is faster.
			// After a short time, turn collision detection back on and reset gravity scale.
			if (_inputReader.Movement.y < 0 && _inputReader.Jump)
			{
				Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), true);
				_rb.gravityScale = _dropDownGravityScale;
				_falling = true;
				_timer = 0;
			}
			else if (_timer > 0.3f)
			{
				Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), false);
				_rb.gravityScale = _defaultGravityScale;
			}
		}

		private void GroundJump()
		{
			_timer = 0;
			JumpThroughPlatform();
			_rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
		}

		private void BubbleJump()
		{
			_timer = 0;
			JumpThroughPlatform();
			_rb.AddForce(transform.up * _bubbleJumpForce, ForceMode2D.Impulse);
		}
		#endregion
	}
}
