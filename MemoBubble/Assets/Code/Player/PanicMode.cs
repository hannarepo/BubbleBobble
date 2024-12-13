using Unity.Mathematics;
using UnityEngine;

namespace BubbleBobble
{
	/// <summary>
	/// This script activates panic mode when the player is standing in fire.
	/// </summary>
	///
	/// <remarks>
	/// author: Jose Mäntylä
	/// </remarks>
	public class PanicMode : MonoBehaviour
	{
		[SerializeField] private float _panicTime = 2f;
		[SerializeField] private float _panicImmuneTime = 1f;
		private bool _canPanic = false;
		private InputReader _inputReader;
		private float _timer = 0f;
		private PlayerControl _playerControl;
		private float _flipInterval = 0.5f;
		private float _flipTimer = 0f;
		private Rigidbody2D _rigidBody;
		private bool _panicOver = false;
		private bool _isPanicking = false;
		public bool IsPanicking => _isPanicking;

		#region Unity Functions
		private void Start()
		{
			_inputReader = GetComponent<InputReader>();
			_playerControl = GetComponent<PlayerControl>();
			_rigidBody = GetComponent<Rigidbody2D>();
		}

		/// <summary>
		/// Starts running the timer when _canPanic is true and runs different methods
		/// based on the timers value.
		/// </summary>
		private void Update()
		{
			if (_canPanic)
			{
				_timer += Time.deltaTime;
				_flipTimer += Time.deltaTime;
				// Activate panic in the player if the timer is less or equal to the panic time.
				if (_timer <= _panicTime)
				{
					_isPanicking = true;
					ActivatePanic();
					return;
				}

				_panicOver = true;

				// Deactivate panic if the timer is greater than the panic time.
				// This is to give the player a chance to move or jump out of the fire.
				if (_timer > _panicTime && _panicOver)
				{
					DeactivatePanic();
					_panicOver = false;
				}

				// Reset the timer when the timer is greater than the panic time and panic immune time combined.
				// If the player is still standing in the fire after the panic immune time, the timer is reset.
				if (_timer > _panicTime + _panicImmuneTime)
				{
					_timer = 0f;
				}
			}
		}

		private void OnTriggerEnter2D(Collider2D trigger)
		{
			if (trigger.gameObject.GetComponent<GroundFireTrigger>())
			{
				if (!_canPanic && trigger.gameObject.GetComponent<GroundFireTrigger>())
				{
					_timer = 0f;
				}
			}
		}

		/// <summary>
		/// This method checks if the player is standing in fire.
		/// </summary>
		/// <param name="trigger">Collider2D which is checked for validity</param>
		private void OnTriggerStay2D(Collider2D trigger)
		{
			if (trigger.gameObject.GetComponent<GroundFireTrigger>())
			{
				_canPanic = true;
			}
		}

		/// <summary>
		/// This method is called when the player exits the fire.
		/// </summary>
		/// <param name="trigger">Collider2D which is checked for validity</param>
		private void OnTriggerExit2D(Collider2D trigger)
		{
			if (trigger.gameObject.GetComponent<GroundFireTrigger>())
			{
				_canPanic = false;
				DeactivatePanic();
			}
		}

		#endregion Unity Functions

		/// <summary>
		/// This method activated panic mode, disabling the player's movement and input.
		/// </summary>
		private void ActivatePanic()
		{
			
			_inputReader.enabled = false;
			_playerControl.CanMove = false;
			_rigidBody.velocity = Vector2.zero;
			if (_flipTimer >= _flipInterval)
			{
				_playerControl.LookingRight = !_playerControl.LookingRight;
				_flipTimer = 0f;
			}
		}

		/// <summary>
		/// This method deactivates panic mode, enabling the player's movement and input.
		/// </summary>
		private void DeactivatePanic()
		{
			_isPanicking = false;
			_inputReader.enabled = true;
			_playerControl.CanMove = true;
		}
	}
}
