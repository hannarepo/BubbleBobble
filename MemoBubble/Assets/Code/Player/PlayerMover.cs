
using UnityEngine;

namespace MemoBubble
{
	/// <summary>
	/// Moves the player based on the input from the PlayerControl script.
	/// </summary>
	///
	/// <remarks>
	/// author: Hanna Repo
	/// </remarks>

	[RequireComponent(typeof(Rigidbody2D))]
	public class PlayerMover : MonoBehaviour
	{
		[SerializeField] private float _speed = 5f;
		[SerializeField] private float _speedWithBoost = 10f;
		private float _originalSpeed = 5f;
		private Rigidbody2D _rb;
		private bool _speedBoostIsActive = false;

		public bool SpeedBoostIsActive
		{
			get { return _speedBoostIsActive; }
			set { _speedBoostIsActive = value; }
		}

		private void Start()
		{
			_rb = GetComponent<Rigidbody2D>();
			_originalSpeed = _speed;
		}

		public void Move(Vector2 movement)
		{
			Vector2 velocity = _rb.velocity;

			if (_speedBoostIsActive)
			{
				_speed = _speedWithBoost;
			}
			else
			{
				_speed = _originalSpeed;
			}

			velocity.x = movement.x * _speed;
			_rb.velocity = velocity;
		}
	}
}
