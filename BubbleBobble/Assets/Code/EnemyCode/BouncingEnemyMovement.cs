using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
	public class BouncingEnemyMovement : MonoBehaviour
	{

		[SerializeField] private float _speed = 5;
		private Rigidbody2D _rigidbody;
		private Transform _transform;
		private Vector2 _velocity = Vector2.zero;
		
		void Start()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_rigidbody.AddForce((Vector2.up + Vector2.right).normalized * _speed, ForceMode2D.Impulse);
			//_transform = transform;
			//_velocity = (Vector2.left + Vector2.up * 1.5f).normalized;
		}

		/*
		void Update()
		{
			_transform.position += new Vector3(_velocity.x, _velocity.y, 0) * _speed * Time.deltaTime;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			BounceObject wall = other.GetComponent<BounceObject>();
			if (wall == null)
			{
				return;
			}

			Vector2 normal = wall.Normal;
			Bounce(normal);
		}

		public void Bounce(Vector2 normal)
		{
			Vector2 u = Vector2.Dot(_velocity, normal) * normal;
			Vector2 w = _velocity - u;
			_velocity = w - u;
		}
		*/
	}
}
