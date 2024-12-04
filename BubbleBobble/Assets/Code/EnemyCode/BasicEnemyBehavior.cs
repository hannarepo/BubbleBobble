using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BubbleBobble
{
	public class BasicEnemyBehavior : MonoBehaviour
	{

		public enum EnemyState
		{
			Moving,
			Jumping,
			DroppingFromEdge,
			Falling,
			Shooting
		}

		[SerializeField] private EnemyState _currentState;
		[SerializeField] private float _speed;
		[SerializeField] private float _jumpForce = 5f;
		[SerializeField] private float _hurryUpSpeed;
		[SerializeField] private Transform _edgeCheck;
		[SerializeField] private Transform _groundCheck;
		[SerializeField] private LayerMask _groundLayer;
		[SerializeField] private Transform _wallCheck;
		[SerializeField] private LayerMask _wallLayer;
		[SerializeField] private Transform _platformAboveCheck;
		[SerializeField] private Vector2 _edgeBoxCastSize;
		[SerializeField] private Vector2 _groundBoxCastSize;
		[SerializeField] private Vector2 _wallBoxCastSize;
		[SerializeField] private Vector2 _platformAboveBoxCastSize;
		[SerializeField] private float _boxCastDistance;
		[SerializeField] private bool _isFacingRight;
		[SerializeField] private float _jumpStateTime;
		private GameObject _player;
		private Rigidbody2D _rigidbody2D;
		private Vector2 _direction;
		private Vector2 _enemyPosition;
		private Vector2 _playerPosition;
		private bool _isGrounded = false;
		private bool _isWallAhead = false;
		private bool _isGroundAhead = false;
		private bool _isPlatformAbove = false;
		private bool _isPlayerAbove = false;
		private bool _isPlayerBelow = false;
		private bool _isPlayerOnSameLevel = false;

		private void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_currentState = EnemyState.Moving;
			_player = GameObject.Find("Player");
		}

		private void Update()
		{
			_isGroundAhead = Physics2D.BoxCast(_edgeCheck.position, _edgeBoxCastSize, 0f, _direction, _boxCastDistance, _groundLayer);
			_isGrounded = Physics2D.BoxCast(_groundCheck.position, _groundBoxCastSize, 0f, Vector2.down, _boxCastDistance, _groundLayer);
			_isPlatformAbove = Physics2D.BoxCast(_platformAboveCheck.position, _platformAboveBoxCastSize, 0f, Vector2.up, _boxCastDistance, _groundLayer);
			_playerPosition = _player.transform.position;
			_enemyPosition = transform.position;

			PlayerYPosition();



			switch (_currentState)
			{
				case EnemyState.Moving:
					Move();
					break;
				case EnemyState.Jumping:
					Jump();
					break;
				case EnemyState.DroppingFromEdge:
					Drop();
					break;
				case EnemyState.Falling:
					Fall();
					break;
				case EnemyState.Shooting:
					Shoot();
					break;
			}

			CheckTransitions();
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			if (_groundCheck == null)
			{
				return;
			}

			Gizmos.DrawWireCube(_groundCheck.position - new Vector3(0, _boxCastDistance, 0), _groundBoxCastSize);

			if (_wallCheck == null)
			{
				return;
			}

			Gizmos.DrawWireCube(_wallCheck.position - new Vector3(_boxCastDistance, 0, 0), _wallBoxCastSize);

			if (_edgeCheck == null)
			{
				return;
			}

			Gizmos.DrawWireCube(_edgeCheck.position - new Vector3(0, _boxCastDistance, 0), _edgeBoxCastSize);

			if (_platformAboveCheck == null)
			{
				return;
			}

			Gizmos.DrawWireCube(_platformAboveCheck.position - new Vector3(0, _boxCastDistance, 0), _platformAboveBoxCastSize);
		}

		private void Move()
		{
			_direction = _isFacingRight ? Vector2.right : Vector2.left;
			_rigidbody2D.velocity = _direction * _speed;

			_isWallAhead = Physics2D.BoxCast(_wallCheck.position, _wallBoxCastSize, 0f, _direction, _boxCastDistance, _wallLayer);

			if (_isWallAhead || !_isGroundAhead && !_isPlayerBelow)
			{
				Flip();
			}
		}

		private void Jump()
		{
			_rigidbody2D.velocity = Vector2.left * 0;
			_rigidbody2D.velocity = Vector2.right * 0;
			_rigidbody2D.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
		}

		private void Fall()
		{
			_rigidbody2D.velocity = Vector2.left * 0;
			_rigidbody2D.velocity = Vector2.right * 0;
			_rigidbody2D.velocity = Vector2.down * _speed;
		}

		private void Drop()
		{
			_rigidbody2D.velocity = Vector2.down * 0;
			_rigidbody2D.velocity = _direction * _speed;
		}

		private void Shoot()
		{
			// Shooting logic
		}

		private void CheckTransitions()
		{
			if (_currentState == EnemyState.Moving && !_isGrounded)
			{
				_currentState = EnemyState.Falling;
			}
			else if (_currentState == EnemyState.Falling && _isGrounded)
			{
				_currentState = EnemyState.Moving;
			}
			else if (_currentState == EnemyState.Moving && _isPlayerBelow && !_isGroundAhead)
			{
				_currentState = EnemyState.DroppingFromEdge;
			}
			else if (_currentState == EnemyState.DroppingFromEdge && !_isGrounded)
			{
				_currentState = EnemyState.Falling;
			}
			
			else if (_currentState == EnemyState.Moving && _isPlayerAbove && _isPlatformAbove)
			{
				_currentState = EnemyState.Jumping;
			}
			else if (_currentState == EnemyState.Jumping && _isGrounded && !_isPlatformAbove)
			{
				_currentState = EnemyState.Moving;
			}
			
		}

		private void Flip()
		{
			_isFacingRight = !_isFacingRight;
			transform.Rotate(0f, 180f, 0f);
		}

		private void PlayerYPosition()
		{
			if (Vector2.Distance(new Vector2(0, _playerPosition.y), new Vector2(0, _enemyPosition.y)) < 0.3f)
			{
				_isPlayerOnSameLevel = true;
				_isPlayerAbove = false;
				_isPlayerBelow = false;
			} 
			else if (_playerPosition.y > _enemyPosition.y)
			{
				_isPlayerAbove = true;
				_isPlayerBelow = false;
				_isPlayerOnSameLevel = false;
			} 
			else if (_playerPosition.y < _enemyPosition.y)
			{
				_isPlayerBelow = true;
				_isPlayerAbove = false;
				_isPlayerOnSameLevel = false;
			}
		}
	}
}
