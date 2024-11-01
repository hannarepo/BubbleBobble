using UnityEngine;

namespace BubbleBobble
{
	/// <remarks>
	/// author: Jose Mäntylä
	/// </remarks>
	/// 
	/// <summary>
	/// This script activates panic mode when the player is standing in fire.
	/// </summary>
	public class PanicMode : MonoBehaviour
	{
		private bool _canPanic = false;
		[SerializeField] private float _panicTime = 2f;
		[SerializeField] private float _panicImmuneTime = 1f;
		private InputReader _inputReader;
		private Rigidbody2D _rb;
		private float _timer = 0f;
		private PlayerControl _playerControl;

		#region Unity Functions
		private void Start()
		{
			_inputReader = GetComponent<InputReader>();
			_rb = GetComponent<Rigidbody2D>();
			_playerControl = GetComponent<PlayerControl>();
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
				if (_timer <= _panicTime)
				{
					ActivatePanic();
				}

				if (_timer > _panicTime)
				{
					DeactivatePanic();
				}

				if (_timer >= _panicTime + _panicImmuneTime)
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
				_timer = 0f;
			}
		}

		#endregion Unity Functions

		/// <summary>
		/// This method activated panic mode, disabling the player's movement and input.
		/// </summary>
		/// 
		// Tutki debuggerilla miksi constraintit ei toimi
		private void ActivatePanic()
		{
			_inputReader.enabled = false;
			_playerControl.CanMove = false;
			_rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		}

		/// <summary>
		/// This method deactivates panic mode, enabling the player's movement and input.
		/// </summary>
		private void DeactivatePanic()
		{
			_inputReader.enabled = true;
			_playerControl.CanMove = true;
		}
	}
}
