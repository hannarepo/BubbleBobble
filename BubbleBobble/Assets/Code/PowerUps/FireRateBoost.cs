using UnityEngine;

namespace BubbleBobble
{
    public class FireRateBoost : PowerUp
    {
		private PlayerControl _playerControl;

        protected override void Start()
		{
			base.Start();
			_playerControl = _player.GetComponent<PlayerControl>();
		}

		public override void PowerUpTimer()
		{
			_statusImage.fillAmount -= 1.0f / _powerUpTime * Time.deltaTime;
		}

		public override void ActivatePowerUp()
		{
			if (_playerControl != null)
			{
				_playerControl.FireRateBoostIsActive = true;
				_isActive = true;
			}
			base.ActivatePowerUp();
		}

		public override void DeactivatePowerUp()
		{
			if (_playerControl != null)
			{
				_playerControl.FireRateBoostIsActive = false;
			}
			base.DeactivatePowerUp();
		}

		public override void SetActiveStatus(bool isActive)
		{
			_activeStatus.SetActive(isActive);
		}
	}
}
