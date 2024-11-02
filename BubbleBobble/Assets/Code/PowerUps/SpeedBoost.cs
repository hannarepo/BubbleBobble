using UnityEngine;

namespace BubbleBobble
{
	public class SpeedBoost : PowerUp
	{
		[SerializeField] private float _powerUpTime = 20f;
		private PlayerMover _mover;
		private float _timer = 0f;
		private bool _isActive;

		protected override void Start()
		{
			base.Start();
			_mover = _player.GetComponent<PlayerMover>();
		}

		private void Update()
		{
			print($"speed: {_isActive}");
			_timer += Time.deltaTime;

			if (_isActive)
			{
				_statusImage.fillAmount -= 1.0f / _powerUpTime * Time.deltaTime;
			}

			if (_timer >= _powerUpTime)
			{
				_mover.SpeedBoostIsActive = false;
				_isActive = false;
				SetActiveStatus(false);
				_statusImage.fillAmount = 1;
			}
		}

		public override void ActivatePowerUp()
		{
			if (_mover != null)
			{
				_mover.SpeedBoostIsActive = true;
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
