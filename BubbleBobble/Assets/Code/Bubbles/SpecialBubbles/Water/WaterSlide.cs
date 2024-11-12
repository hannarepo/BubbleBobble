using UnityEngine;

namespace BubbleBobble
{
	public class WaterSlide : MonoBehaviour
	{
		[SerializeField] private float _speed;
		private Vector2 _velocity;
		private Rigidbody2D _rigidBody;
		private bool _directionRandomized = false;
		private bool _directionChanged = false;
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
				_directionChanged = false;
			}
			else if (_floorChecker.IsTouchingFloor && !_wallChecker.IsTouchingWall
					&& !_directionRandomized)
			{
				_velocity = Random.Range(0, 2) == 0
				? new Vector2(_speed, 0)
				: new Vector2(-_speed, 0);
				_directionRandomized = true;
			}
			else if (_floorChecker.IsTouchingFloor && _wallChecker.IsTouchingWall
					&& !_directionChanged)
			{
				if (_velocity.x > 0)
				{
					_velocity = new Vector2(-_speed, 0);
				}
				else if (_velocity.x < 0)
				{
					_velocity = new Vector2(_speed, 0);
				}
				_directionChanged = true;
			}
		}

		private void FixedUpdate()
		{
			_rigidBody.MovePosition(_rigidBody.position + _velocity * Time.fixedDeltaTime);
		}

		private void OnCollisionStay2D(Collision2D collision)
		{
			if(collision.gameObject.CompareTag(Tags._platform)
			|| collision.gameObject.CompareTag(Tags._ground))
			{
				_floorChecker.IsTouchingFloor = true;
			}
		}

		private void OnCollisionExit2D(Collision2D collision)
		{
			if(collision.gameObject.CompareTag(Tags._platform)
			|| collision.gameObject.CompareTag(Tags._ground))
			{
				_floorChecker.IsTouchingFloor = false;
			}
		}

		#endregion Unity Functions
	}
}
