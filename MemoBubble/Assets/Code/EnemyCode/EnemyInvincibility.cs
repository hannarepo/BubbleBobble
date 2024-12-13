using UnityEngine;

namespace MemoBubble
{
	/// <summary>
	/// Enemy can be set to invincible for a short time when it is freed from a bubble.
	/// </summary>
	public class EnemyInvincibility : MonoBehaviour
	{
		private bool _isInvincible = false;

		public bool IsInvincible { get { return _isInvincible; } }

		public void FreeEnemy()
		{
			_isInvincible = true;
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag(Tags.Platform) || collision.gameObject.CompareTag(Tags.Ground))
			{
				_isInvincible = false;
			}
		}
	}
}
