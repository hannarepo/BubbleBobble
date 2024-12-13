using UnityEngine;

namespace BubbleBobble
{
	/// <summary>
	/// Give projectile bubble a size boost.
	/// </summary>
	public class BubbleSizeBoost : PowerUp
	{
		private ShootBubble _shoot;

		protected override void Start()
		{
			base.Start();
			_shoot = _player.GetComponent<ShootBubble>();
		}

		public override void PowerUpTimer()
		{
			_timerImage.fillAmount -= 1.0f / _powerUpTime * Time.deltaTime;
			_timerText.text = $"{(int)_powerUpTime - (int)_timer}";
		}

		public override void ActivatePowerUp()
		{
			if (_shoot != null)
			{
				_shoot.SizeBoostIsActive = true;
				_isActive = true;
			}
			base.ActivatePowerUp();
		}

		public override void DeactivatePowerUp()
		{
			if (_shoot != null)
			{
				_shoot.SizeBoostIsActive = false;
			}
			base.DeactivatePowerUp();
		}

		public override void SetActiveStatus(bool isActive)
		{
			_activeStatus.SetActive(isActive);
		}
	}
}
