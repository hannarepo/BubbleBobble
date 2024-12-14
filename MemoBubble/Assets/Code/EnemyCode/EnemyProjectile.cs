using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoBubble
{
	public class EnemyProjectile : MonoBehaviour
	{
		[SerializeField] private float _speed;
		[SerializeField] private float _launchForce = 9f;
		private Rigidbody2D _rigidBody2D;
		private SpriteRenderer _spriteRenderer;
		
		private void Awake()
		{
			_rigidBody2D = GetComponent<Rigidbody2D>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
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
				_spriteRenderer.flipX = true;
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				DestroyProjectile();
			}
			else if (collision.gameObject.CompareTag("Wall"))
			{
				DestroyProjectile();
			}
			else if (collision.gameObject.CompareTag("Ground"))
			{
				DestroyProjectile();
			}
			else if (collision.gameObject.CompareTag("Platform"))
			{
				DestroyProjectile();
			}
		}

		private void DestroyProjectile()
		{
			Destroy(gameObject);
		}
	}
}
