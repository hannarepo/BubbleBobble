using UnityEngine;

namespace BubbleBobble
{
	public class Shop : MonoBehaviour
	{
		// Points serialized for testing
		[SerializeField] private int _points = 0;
		[SerializeField] private PowerUp[] _powerUps;
		[SerializeField] private ItemData[] _shells;
		[SerializeField] private PlayerControl _playerControl;
		[SerializeField] private Health _health;

		private void Update()
		{
			CheckPoints();
		}

		private void CheckPoints()
		{
			foreach (PowerUp powerUp in _powerUps)
			{
					if (powerUp.PowerUpData.Price > _points)
				{
					powerUp.SetPriceColor(Color.red);
				}
				else
				{
					powerUp.SetPriceColor(Color.black);
				}
			}
		}

		private bool CheckInventory()
		{
			if (_playerControl.CheckInventoryContent(_shells[0]) &&
				_playerControl.CheckInventoryContent(_shells[1]) &&
				_playerControl.CheckInventoryContent(_shells[2]) &&
				_playerControl.CheckInventoryContent(_shells[3]))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public void Buy(int index)
		{
			if (index >= 0 && index  <= 3)
			{
				if (_powerUps[index].PowerUpData.Price <= _points)
				{
					_powerUps[index].ActivatePowerUp();
				}
			}
			else if (index == 4)
			{
				if (CheckInventory() && _health != null && _health.CurrentLives < _health.MaxLives)
				{
					_health.SetExtraLife(true);
					_playerControl.RemoveFromInventory(_shells[0]);
					_playerControl.RemoveFromInventory(_shells[1]);
					_playerControl.RemoveFromInventory(_shells[2]);
					_playerControl.RemoveFromInventory(_shells[3]);
				}
			}
		}
	}
}
