using UnityEngine;

namespace BubbleBobble
{
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
			_statusImage.fillAmount -= 1.0f / _powerUpTime * Time.deltaTime;
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
