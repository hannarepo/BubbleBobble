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
		[SerializeField] private float _hurryUpSpeed;
		[SerializeField] private Transform _edgeCheck;
		[SerializeField] private Transform _groundCheck;
		[SerializeField] private LayerMask _groundLayer;
		[SerializeField] private Transform _wallCheck;
		[SerializeField] private LayerMask _wallLayer;
		[SerializeField] private Vector2 _edgeBoxCastSize;
		[SerializeField] private Vector2 _groundBoxCastSize;
		[SerializeField] private Vector2 _wallBoxCastSize;
		[SerializeField] private float _boxCastDistance;
		[SerializeField] private bool _isFacingRight;
		[SerializeField] private Transform _player;
		private Rigidbody2D _rigidbody2D;
		private Vector2 _direction;
		private Vector2 _enemyPosition;
		private Vector2 _playerPosition;
		private bool _isGrounded = false;
		private bool _isWallAhead = false;
		private bool _isGroundAhead = false;

		private void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_currentState = EnemyState.Moving;
		}

		private void Update()
		{
			_isGroundAhead = Physics2D.BoxCast(_edgeCheck.position, _edgeBoxCastSize, 0f, _direction, _boxCastDistance, _groundLayer);
			_isGrounded = Physics2D.BoxCast(_groundCheck.position, _groundBoxCastSize, 0f, Vector2.down, _boxCastDistance, _groundLayer);
			_playerPosition = _player.position;
			_enemyPosition = transform.position;

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
		}

		private void Move()
		{
			_rigidbody2D.constraints = RigidbodyConstraints2D.None;
			_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
			
			_direction = _isFacingRight ? Vector2.right : Vector2.left;
			_rigidbody2D.velocity = _direction * _speed;

			_isWallAhead = Physics2D.BoxCast(_wallCheck.position, _wallBoxCastSize, 0f, _direction, _boxCastDistance, _wallLayer);

			if (_isWallAhead || !_isGroundAhead)
			{
				Flip();
			}
		}

		private void Jump()
		{
			// Jumping logic
		}

		private void Fall()
		{
			_rigidbody2D.constraints = RigidbodyConstraints2D.None;
			_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
			_rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
		}

		private void Drop()
		{
			_rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
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
				Debug.Log("Falling");
				_currentState = EnemyState.Falling;
			}
			else if (_currentState == EnemyState.Falling && _isGrounded)
			{
				_currentState = EnemyState.Moving;
			}
			else if (_currentState == EnemyState.Moving && _playerPosition.y < _enemyPosition.y)
			{
				_currentState = EnemyState.DroppingFromEdge;
			}
			else if (_currentState == EnemyState.DroppingFromEdge && !_isGrounded)
			{
				_currentState = EnemyState.Falling;
			}
			
		}

		private void Flip()
		{
			_isFacingRight = !_isFacingRight;
			transform.Rotate(0f, 180f, 0f);
		}
	}
}
