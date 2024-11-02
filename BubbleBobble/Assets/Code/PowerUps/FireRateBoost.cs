using UnityEngine;

namespace BubbleBobble
{
    public class FireRateBoost : PowerUp
    {
		[SerializeField] private float _powerUpTime = 20f;
		private PlayerControl _playerControl;
		private float _timer = 0f;
		private bool _isActive;

        protected override void Start()
		{
			base.Start();
			_playerControl = _player.GetComponent<PlayerControl>();
		}

		private void Update()
		{
			print($"fire rate: {_isActive}");
			_timer += Time.deltaTime;

			if (_isActive)
			{
				_statusImage.fillAmount -= 1.0f / _powerUpTime * Time.deltaTime;
			}

			if (_timer >= _powerUpTime)
			{
				_playerControl.FireRateBoostIsActive = false;
				_isActive = false;
				SetActiveStatus(false);
				_statusImage.fillAmount = 1;
			}
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

		public override void SetActiveStatus(bool isActive)
		{
			_activeStatus.SetActive(isActive);
		}
	}
}
