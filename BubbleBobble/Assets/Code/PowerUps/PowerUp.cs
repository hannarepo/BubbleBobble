using UnityEngine;
using TMPro;

namespace BubbleBobble
{
	public abstract class PowerUp : MonoBehaviour
	{
		[SerializeField] protected GameObject _player;
		[SerializeField] private PowerUpData _powerUpData;
		[SerializeField] private TextMeshProUGUI _priceText;
		[SerializeField] private GameObject _activeStatus;
		[SerializeField] protected float _powerUpTime = 20f;
		protected float _timer = 0f;

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
