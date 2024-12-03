using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
	public class BouncingEnemyMovement : MonoBehaviour
	{
		[SerializeField] private float _speed = 5;
		[SerializeField] private bool _isFacingRight;
		[SerializeField] private bool _canFlip;
		private Rigidbody2D _rigidbody;
		private Vector2 _velocity = Vector2.zero;
		
		void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
		}
		
		void OnEnable()
		{
			_rigidbody.AddForce((Vector2.up + Vector2.left).normalized * _speed, ForceMode2D.Impulse);
		}

		void Update()
		{
			_velocity = _rigidbody.velocity;
			if (_canFlip) 
			{
				if (_velocity.x > 0 && !_isFacingRight)
				{
					Flip();
				}
				else if (_velocity.x < 0 && _isFacingRight)
				{
					Flip();
				}
			}
		}
		private void Flip()
		{
			_isFacingRight = !_isFacingRight;
			transform.Rotate(0f, 180f, 0f);
		}
	}
}
