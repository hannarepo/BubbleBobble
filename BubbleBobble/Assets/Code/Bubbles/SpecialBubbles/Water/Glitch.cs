using UnityEngine;

namespace BubbleBobble
{
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
		private bool _isTouchingFloor = false;
		private bool _isTouchingWall = false;

	#region Unity Functions

		private void Awake()
		{
			_rigidBody = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			if (transform.position.y < _destructionPosition)
			{
				Destroy(gameObject);
			}

			if (!_isTouchingFloor)
			{
				_velocity = new Vector2(0, -_speed);
				_directionRandomized = false;
			}

			if (!_isTouchingFloor && _isTouchingWall)
			{
				SwapWallCollider();
			}

			else if (_isTouchingFloor && !_isTouchingWall
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

			else if (_isTouchingFloor && _isTouchingWall)
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

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.TryGetComponent<EnemyManagement>(out EnemyManagement enemyManagement))
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

		private void OnCollisionStay2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag(Tags._platform)
			|| collision.gameObject.CompareTag(Tags._ground))
			{
				_isTouchingFloor = true;
			}
		}

		private void OnCollisionExit2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag(Tags.Platform)
			|| collision.gameObject.CompareTag(Tags.Ground))
			{
				_isTouchingFloor = false;
			}
		}

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if(collider.gameObject.CompareTag(Tags.Platform)
			|| collider.gameObject.CompareTag(Tags.Wall))
			{
				_isTouchingWall = true;
			}
		}

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

		public void LeftColliderOn()
		{
			_leftCollider.enabled = true;
			_rightCollider.enabled = false;
		}

		public void RightColliderOn()
		{
			_rightCollider.enabled = true;
			_leftCollider.enabled = false;
		}

		public void SwapWallCollider()
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
