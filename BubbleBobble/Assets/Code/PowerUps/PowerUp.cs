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
		[SerializeField] protected GameObject _activeStatus;
		[SerializeField] protected Image _statusImage;

		public PowerUpData PowerUpData => _powerUpData;

		protected virtual void Start()
		{
			SetActiveStatus(false);
		}

		public virtual void ActivatePowerUp()
		{
			SetActiveStatus(true);
		}

		public void SetPriceColor(Color color)
		{
			_priceText.color = color;
		}

		public virtual void SetActiveStatus(bool isActive)
		{
			_activeStatus.SetActive(isActive);
		}
	}
}
