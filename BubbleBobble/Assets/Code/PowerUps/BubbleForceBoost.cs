using UnityEngine;

namespace BubbleBobble
{
    public class BubbleForceBoost : PowerUp
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
				_shoot.ForceBoostIsActive = true;
				_isActive = true;
			}
			base.ActivatePowerUp();
		}

		public override void DeactivatePowerUp()
		{
			if (_shoot != null)
			{
				_shoot.ForceBoostIsActive = false;
			}
			base.DeactivatePowerUp();
		}

		public override void SetActiveStatus(bool isActive)
		{
			_activeStatus.SetActive(isActive);
		}
	}
}
