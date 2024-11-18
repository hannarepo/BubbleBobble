using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
	public class BasicEnemyBehavior : MonoBehaviour
	{

		public enum EnemyState
		{
			Moving,
			Jumping,
			Falling,
			Shooting
		}

		[SerializeField] private EnemyState _currentState;
		[SerializeField] private float _speed;
		[SerializeField] private Transform _groundCheck;
		[SerializeField] private LayerMask _groundLayer;
		[SerializeField] private Transform _wallCheck;
		[SerializeField] private LayerMask _wallLayer;
		[SerializeField] private Vector2 _groundBoxCastSize;
		[SerializeField] private Vector2 _wallBoxCastSize;
		[SerializeField] private float _boxCastDistance;
		[SerializeField] private bool _isFacingRight;
		private Rigidbody2D _rigidbody2D;
		private SpriteRenderer _spriteRenderer;
		private Vector2 _direction;
		private bool _isGrounded = false;

		private void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_currentState = EnemyState.Moving;
		}

		private void Update()
		{
			switch (_currentState)
			{
				case EnemyState.Moving:
					Move();
					break;
				case EnemyState.Jumping:
					Jump();
					break;
				case EnemyState.Falling:
					Fall();
					break;
				case EnemyState.Shooting:
					Shoot();
					break;
			}

			CheckTransitions();
			_isGrounded = Physics2D.BoxCast(_groundCheck.position, _groundBoxCastSize, 0f, Vector2.down, _boxCastDistance, _groundLayer);
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
		}

		private void Move()
		{
			_direction = _isFacingRight ? Vector2.right : Vector2.left;
			_rigidbody2D.velocity = _direction * _speed;

			// Check for platform edge or wall
			bool isGroundAhead = Physics2D.Raycast(_groundCheck.position, Vector2.down, 1f, _groundLayer);
			bool isWallAhead = Physics2D.Raycast(_wallCheck.position, _direction, 0.5f, _wallLayer);

			// If no ground ahead or a wall is detected, turn around
			if (isWallAhead || !isGroundAhead)
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
			// Falling logic
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
		}

		private void Flip()
		{
			_isFacingRight = !_isFacingRight;
			transform.Rotate(0f, 180f, 0f);
		}
	}
}
