using UnityEngine;

namespace BubbleBobble
{
	public class BubbleSizeBoost : PowerUp
	{
		[SerializeField] private float _powerUpTime = 20f;
		private ShootBubble _shoot;
		private float _timer = 0f;
		private bool _isActive;

		protected override void Start()
		{
			base.Start();
			_shoot = _player.GetComponent<ShootBubble>();
		}

		private void Update()
		{
			print($"bubble size: {_isActive}");
			_timer += Time.deltaTime;

			if (_isActive)
			{
				_statusImage.fillAmount -= 1.0f / _powerUpTime * Time.deltaTime;
			}

			if (_timer >= _powerUpTime)
			{
				_shoot.SizeBoostIsActive = false;
				_isActive = false;
				SetActiveStatus(false);
				_statusImage.fillAmount = 1;
			}
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

		public override void SetActiveStatus(bool isActive)
		{
			_activeStatus.SetActive(isActive);
		}
	}
}
