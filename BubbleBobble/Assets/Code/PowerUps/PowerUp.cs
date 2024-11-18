using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace BubbleBobble
{
	/// <summary>
	/// Base class for all power ups.
	/// Includes setting a color for the price text in the shop,
	/// activating power up on purchase, and activating an image on screen
	/// to inform player when power up is active.
	/// </summary>
	/// 
	/// <remarks>
	/// author: Hanna Repo
	/// </remarks>

	public abstract class PowerUp : MonoBehaviour, IPowerUp
	{
		[SerializeField] protected GameObject _player;
		[SerializeField] private PowerUpData _powerUpData;
		[SerializeField] private TextMeshProUGUI _priceText;
		[SerializeField] private Image _buttonImage;
		[SerializeField] protected GameObject _activeStatus;
		[SerializeField] protected Image _timerImage;
		[SerializeField] protected float _powerUpTime = 20f;
		[SerializeField] protected TMP_Text _timerText;
		protected bool _isActive;
		protected float _timer;

		public PowerUpData PowerUpData => _powerUpData;

		protected virtual void Start()
		{
			SetActiveStatus(false);
		}

		private void Update()
		{
			if (_isActive)
			{
				_timer += Time.deltaTime;
				PowerUpTimer();
			}

			if (_timer >= _powerUpTime)
			{
				DeactivatePowerUp();
			}
		}

		public virtual void DeactivatePowerUp()
		{
			_isActive = false;
			SetActiveStatus(false);
			_timer = 0f;
			_timerImage.fillAmount = 1f;
		}

		/// <summary>
		/// Deplete status image fill amount over time, to indicate to player
		/// how much time is left on the power up.
		/// </summary>
		public virtual void PowerUpTimer()
		{
			// Implementation in child classes
		}

		public virtual void ActivatePowerUp()
		{
			_isActive = true;
			SetActiveStatus(true);
		}

		public void SetPriceColor(Color color)
		{
			_priceText.color = color;
		}

		public void SetButtonColor(Color color)
		{
			_buttonImage.color = color;
		}

		/// <summary>
		/// Sets the active status of the power up to let player know when power up is active.
		/// </summary>
		/// <param name="isActive"></param>
		public virtual void SetActiveStatus(bool isActive)
		{
			_activeStatus.SetActive(isActive);
		}
	}
}
