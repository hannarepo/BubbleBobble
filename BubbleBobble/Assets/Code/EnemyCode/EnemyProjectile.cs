using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
	public class EnemyProjectile : MonoBehaviour
	{
		[SerializeField] private float _speed;
		[SerializeField] private float _launchForce = 9f;
		private Rigidbody2D _rigidBody2D;
		
		private void Awake()
		{
			_rigidBody2D = GetComponent<Rigidbody2D>();
		}

		void Update()
		{
			
		}

		public void Launch(bool lookingRight)
		{
			if (lookingRight)
			{
				_rigidBody2D.AddForce(transform.right * _launchForce, ForceMode2D.Impulse);
			}
			else
			{
				_rigidBody2D.AddForce(-transform.right * _launchForce, ForceMode2D.Impulse);
			}
		}
	}
}
