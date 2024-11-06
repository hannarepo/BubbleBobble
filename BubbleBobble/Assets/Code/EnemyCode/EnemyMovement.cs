using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
	public class EnemyMovement : MonoBehaviour
	{
		public GameObject _pointA;
		public GameObject _pointB;
		private Rigidbody2D _rigidbody2D;
		private Transform _currentPoint;


		[SerializeField] private float _speed;

		void Start()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_currentPoint = _pointA.transform;
		}

		void Update()
		{
			Vector2 point = _currentPoint.position - transform.position;
			if (_currentPoint == _pointA.transform)
			{
				_rigidbody2D.velocity = new Vector2(-_speed, 0);
			}
			else
			{
				_rigidbody2D.velocity = new Vector2(_speed, 0);
			}
			if (Vector2.Distance(transform.position, _currentPoint.position) < 0.5f && _currentPoint == _pointA.transform)
			{
				_currentPoint = _pointB.transform;
			}
			if (Vector2.Distance(transform.position, _currentPoint.position) < 0.5f && _currentPoint == _pointB.transform)
			{
				_currentPoint = _pointA.transform;
			}
		}
	}
}
