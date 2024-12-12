using System.Collections;
using UnityEngine;

namespace BubbleBobble
{
	/// <summary>
	/// Contains all the behaviour of the Glitches
	/// spawned from Glitch bubbles.
	/// </summary>
	/// <remarks>
	/// author: Jose Mäntylä
	/// </remarks>
	public class Glitch : MonoBehaviour
	{
		[SerializeField] private float _speed;
		[SerializeField] private Vector2 _velocity;
		[SerializeField] float _destructionPosition = -8f;
		[SerializeField] Collider2D _leftCollider;
		[SerializeField] Collider2D _rightCollider;
		private Rigidbody2D _rigidBody;
		private bool _directionRandomized = false;
		private bool _canGrabPlayer = true;
		private GameObject _player;
		private PlayerControl _playerControl;
		private InputReader _inputReader;
		private bool _isTouchingGround = false;
		private bool _isTouchingWall = false;

	#region Unity Functions

		private void Awake()
		{
			_rigidBody = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			// If the object falls too far down, destroy it.
			if (transform.position.y < _destructionPosition)
			{
				Destroy(gameObject);
			}

			// If the object falls off a ledge, add downward velocity
			// and allow direction to be randomized when landing.
			if (!_isTouchingGround)
			{
				_velocity = new Vector2(0, -_speed);
				_directionRandomized = false;
			}

			// If the object falls off a ledge and touches a wall,
			// swap to the other wall check collider.
			if (!_isTouchingGround && _isTouchingWall)
			{
				SwapWallCollider();
			}

			// "Flip a coin" to choose if the object will move left or right
			// when landing on ground.
			else if (_isTouchingGround && !_isTouchingWall
					&& !_directionRandomized)
			{
				if (Random.Range(0, 2) == 0)
				{
					_velocity = new Vector2(_speed, 0);
					RightColliderOn();
				}
				else
				{
					_velocity = new Vector2(-_speed, 0);
					LeftColliderOn();
				}
				_directionRandomized = true;
			}

			// Turn around when hitting a wall
			else if (_isTouchingGround && _isTouchingWall)
			{
				if (_velocity.x > 0)
				{
					_velocity = new Vector2(-_speed, 0);
					LeftColliderOn();
				}
				else if (_velocity.x < 0)
				{
					_velocity = new Vector2(_speed, 0);
					RightColliderOn();
				}
			}

			// If the object can grab the player, try to take
			// the player character down with it.
			// Canceled if the player jumps or player movement is restricted by something else.
			if (_player != null && _canGrabPlayer)
			{
				_player.transform.position = transform.position;
				if (_inputReader.Jump || !_playerControl.CanMove)
				{
					_canGrabPlayer = false;
				}
			}
		}

		private void FixedUpdate()
		{
			_rigidBody.MovePosition(_rigidBody.position + _velocity * Time.fixedDeltaTime);
		}

		/// <summary>
		/// Kill enemies when colliding with them.
		/// When colliding with the player, get components and change layer
		/// to prevent future collisions with this object.
		/// </summary>
		/// <param name="collision">Detected collision</param>
		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.TryGetComponent<EnemyDeath>(out EnemyDeath enemyManagement))
			{
				enemyManagement.LaunchAtDeath(true);
			}
			if (collision.gameObject.CompareTag(Tags.Player) && _canGrabPlayer)
			{
				_player = collision.gameObject;
				_playerControl = _player.GetComponent<PlayerControl>();
				_inputReader = _player.GetComponent<InputReader>();
				gameObject.layer = LayerMask.NameToLayer("Water");
			}
		}

		/// <summary>
		/// Check if the object is touching the ground.
		/// Done as OnCollisionStay because OnCollisionExit
		/// is sometimes called when this object hits enemies or platform walls.
		/// </summary>
		/// <param name="collision">Detected collision</param>
		private void OnCollisionStay2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag(Tags.Platform)
			|| collision.gameObject.CompareTag(Tags.Ground))
			{
				_isTouchingGround = true;
			}
		}

		/// <summary>
		/// Flip ground check boolean when the object exits collision with the ground.
		/// </summary>
		/// <param name="collision">Detected collision</param>
		private void OnCollisionExit2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag(Tags.Platform)
			|| collision.gameObject.CompareTag(Tags.Ground))
			{
				_isTouchingGround = false;
			}
		}

		/// <summary>
		/// Detect if the object's trigger collider touches a wall.
		/// </summary>
		/// <param name="collider"></param>
		private void OnTriggerEnter2D(Collider2D collider)
		{
			if(collider.gameObject.CompareTag(Tags.Platform)
			|| collider.gameObject.CompareTag(Tags.Wall))
			{
				_isTouchingWall = true;
			}
		}

		/// <summary>
		/// Detect if the object's trigger collider is no longer touching a wall.
		/// </summary>
		/// <param name="collider"></param>
		private void OnTriggerExit2D(Collider2D collider)
		{
			if(collider.gameObject.CompareTag(Tags.Platform)
			|| collider.gameObject.CompareTag(Tags.Wall))
			{
				_isTouchingWall = false;
			}
		}

	#endregion Unity Functions

	#region WallCheck

		/// <summary>
		/// Enable the left side collider and disable the right side collider.
		/// </summary>
		private void LeftColliderOn()
		{
			_leftCollider.enabled = true;
			_rightCollider.enabled = false;
		}

		/// <summary>
		/// Enable the right side collider and disable the left side collider.
		/// </summary>
		private void RightColliderOn()
		{
			_rightCollider.enabled = true;
			_leftCollider.enabled = false;
		}

		/// <summary>
		/// Check which side collider is enabled and which one is disabled
		/// and invert them.
		/// </summary>
		private void SwapWallCollider()
		{
			if (_rightCollider.enabled)
			{
				LeftColliderOn();
			}
			else
			{
				RightColliderOn();
			}
		}

	#endregion WallCheck

	}
}
