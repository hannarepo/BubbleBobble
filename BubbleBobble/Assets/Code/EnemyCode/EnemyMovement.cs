using System.Collections;
using System.Collections.Generic;
using BubbleBobble;
using Unity.VisualScripting;
using UnityEngine;
namespace BubbleBobble
{
	public class EnemyMovement : MonoBehaviour
	{
		public GameObject _pointA;
		public GameObject _pointB;
		private Rigidbody2D _rb;
		private Transform _currentPoint;
		[SerializeField] private float _speed;
		[SerializeField] private float _jumpForce = 5f;
		private bool _isGrounded;
		private static Vector2 _enemyPosition;

		void Start()
		{
			_rb = GetComponent<Rigidbody2D>();
			_currentPoint = _pointB.transform;
			_isGrounded = true;
		}

		void Update()
		{
			Vector2 point = _currentPoint.position - transform.position;
			if (_currentPoint == _pointB.transform)
			{
				_rb.velocity = new Vector2(_speed, 0);
			}
			else
			{
				_rb.velocity = new Vector2(-_speed, 0);
			}
			if (Vector2.Distance(transform.position, _currentPoint.position) < 0.5f && _currentPoint == _pointB.transform)
			{
				flip();
				_currentPoint = _pointA.transform;
			}
			if (Vector2.Distance(transform.position, _currentPoint.position) < 0.5f && _currentPoint == _pointA.transform)
			{
				flip();
				_currentPoint = _pointB.transform;
			}

			_enemyPosition = transform.position;

			if (PlayerPosition.playerPosition.y > _enemyPosition.y && _isGrounded == true)
			{
				Jump();
			}
		}

		void OnTriggerExit2D(Collider2D other)
		{
			if (other.gameObject.CompareTag(Tags._ground))
			{
				_isGrounded = false;
			}
		}

		private void Jump()
		{
			_rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
		}

		private void flip()
		{
			Vector3 localScale = transform.localScale;
			localScale.x = -1;
			transform.localScale = localScale;
		}

		private void OnDrawGizmos()
		{
			Gizmos.DrawWireSphere(_pointA.transform.position, 0.5f);
			Gizmos.DrawWireSphere(_pointB.transform.position, 0.5f);
			Gizmos.DrawLine(_pointA.transform.position, _pointB.transform.position);
		}
	}
}
