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

		#region Unity Functions

		private void Awake()
		{
			_rigidBody = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
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
		}

		private void FixedUpdate()
		{
			_rigidBody.MovePosition(_rigidBody.position + _velocity * Time.fixedDeltaTime);
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

		#endregion Unity Functions
	}
}
