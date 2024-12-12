using UnityEngine;

namespace BubbleBobble
{
	public class Shop : MonoBehaviour
	{
		/// <summary>
		/// Manages shop where power ups can be bought.
		/// Checks if player has enough points to buy power up and updates the price text.
		/// </summary>
		[SerializeField] private PowerUp[] _powerUps;
		[SerializeField] private ItemData[] _shells;
		[SerializeField] private PlayerControl _playerControl;
		[SerializeField] private UnityEngine.UI.Image[] _shellIcons;
		[SerializeField] private Color _grayPriceColor;
		[SerializeField] private ScoreText _scoreText;

		private Inventory _inventory;
		private GameManager _gameManager;

		private void Start()
		{
			_inventory = _playerControl.Inventory;
			_gameManager = FindObjectOfType<GameManager>();
		}

		private void Update()
		{
			CheckPoints();
			CheckInventory();
		}

		/// <summary>
		/// Check whether player has enough points for each power up.
		/// If not, price text of power up is set to red.
		/// </summary>
		private void CheckPoints()
		{
			foreach (PowerUp powerUp in _powerUps)
			{
				if (powerUp.PowerUpData.Price > _gameManager.Score)
				{
					powerUp.SetButtonColor(_grayPriceColor);
					powerUp.SetPriceColor(_grayPriceColor);
				}
				else
				{
					powerUp.SetButtonColor(Color.white);
					powerUp.SetPriceColor(Color.black);
				}
			}
		}

		/// <summary>
		/// Check whether all shells are in player's inventory.
		/// </summary>
		/// <returns> True if all shells are found in inventory, false if not. </returns>
		private void CheckInventory()
		{
			for (int i = 0; i < _shells.Length; i++)
			{
				if (!_inventory.CheckInventoryContent(_shells[i], 1))
				{
					_shellIcons[i].color = _grayPriceColor;
				}
				else
				{
					_shellIcons[i].color = Color.white;
				}
			}
		}

		/// <summary>
		/// Tied to shop buttons for each power up and extra life.
		/// If player has enough points, they can buy a power up.
		/// If player has all shells and doesn't have max lives, player can buy an extra life.
		/// Shells are removed from player inventory upon purchase.
		/// </summary>
		/// <param name="index"> Set in each button to pick correct power up. </param>
		public void Buy(int index)
		{
			if (index >= 0 && index <= 3)
			{
				if (_powerUps[index].PowerUpData.Price <= _gameManager.Score)
				{
					_powerUps[index].ActivatePowerUp();
					_gameManager.Score -= _powerUps[index].PowerUpData.Price;
					_scoreText.UpdateScore(_powerUps[index].PowerUpData.Price);
				}
			}
		}
	}
}
