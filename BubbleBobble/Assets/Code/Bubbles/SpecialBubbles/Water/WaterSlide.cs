using Unity.VisualScripting;
using UnityEngine;

namespace BubbleBobble
{
	public class WaterSlide : MonoBehaviour
	{
		[SerializeField] private float _speed;
		[SerializeField] private Vector2 _velocity;
		private Rigidbody2D _rigidBody;
		private bool _directionRandomized = false;
		[SerializeField] FloorChecker _floorChecker;
		[SerializeField] WallChecker _wallChecker;
		[SerializeField] float _destructionPosition = -8f;
		private bool _canGrabPlayer = true;
		private GameObject _player;
		private InputReader _inputReader;

	#region Unity Functions

		private void Awake()
		{
			_rigidBody = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			if (gameObject.transform.position.y < _destructionPosition)
			{
				// TODO: Release the player from the object first.
				Destroy(gameObject);
			}

			if (!_floorChecker.IsTouchingFloor)
			{
				_velocity = new Vector2(0, -_speed);
				_directionRandomized = false;
			}

			if (!_floorChecker.IsTouchingFloor && _wallChecker.IsTouchingWall)
			{
				_wallChecker.SwapCollider();
			}

			else if (_floorChecker.IsTouchingFloor && !_wallChecker.IsTouchingWall
					&& !_directionRandomized)
			{
				if (Random.Range(0, 2) == 0)
				{
					_velocity = new Vector2(_speed, 0);
					_wallChecker.RightColliderOn();
				}
				else
				{
					_velocity = new Vector2(-_speed, 0);
					_wallChecker.LeftColliderOn();
				}
				_directionRandomized = true;
			}

			else if (_floorChecker.IsTouchingFloor && _wallChecker.IsTouchingWall)
			{
				if (_velocity.x > 0)
				{
					_velocity = new Vector2(-_speed, 0);
					_wallChecker.LeftColliderOn();
				}
				else if (_velocity.x < 0)
				{
					_velocity = new Vector2(_speed, 0);
					_wallChecker.RightColliderOn();
				}
			}

			if (_player != null && _canGrabPlayer)
			{
				_player.transform.position = gameObject.transform.position;
				if (_inputReader.Jump)
				{
					EnablePlayerCollider();
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
				enemyManagement.LaunchAtDeath();
			}
			if (collision.gameObject.CompareTag(Tags._player) && _canGrabPlayer)
			{
				_player = collision.gameObject;
				_inputReader = _player.GetComponent<InputReader>();
				_player.GetComponent<Collider2D>().enabled = false;
			}
		}

		private void OnCollisionStay2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag(Tags._platform)
			|| collision.gameObject.CompareTag(Tags._ground))
			{
				_floorChecker.IsTouchingFloor = true;
			}
		}

		private void OnCollisionExit2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag(Tags._platform)
			|| collision.gameObject.CompareTag(Tags._ground))
			{
				_floorChecker.IsTouchingFloor = false;
			}
		}

		private void OnDestroy()
		{
			if (_player != null)
			{
				EnablePlayerCollider();
			}
		}

	#endregion Unity Functions

		private void EnablePlayerCollider()
		{
			_player.GetComponent<Collider2D>().enabled = true;
		}
	}
}
