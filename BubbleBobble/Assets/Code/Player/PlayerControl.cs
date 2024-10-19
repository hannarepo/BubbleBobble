/// <remarks>
/// author: Hanna Repo
/// </remarks>
/// 
/// <summary>
/// Script for controlling various player actions.
/// </summary>
using UnityEngine;

namespace BubbleBobble
{
	[RequireComponent(typeof(InputReader))]
	public class PlayerControl : MonoBehaviour
	{
		private InputReader _inputReader;
		private Inventory _inventory;
		private PlayerMover _playerMover;
		private ShootBubble _shootBubble;
		private SpriteRenderer _spriteRenderer;
		private bool _lookRight;
		private PlayerAnimationController _playerAnimator;

		public bool LookingRight => _lookRight;
		
		private void Awake()
		{
			_inputReader = GetComponent<InputReader>();
			_inventory = new Inventory();
			_playerMover = GetComponent<PlayerMover>();
			_shootBubble = GetComponent<ShootBubble>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_playerAnimator = GetComponent<PlayerAnimationController>();
		}

		private void Update()
		{
			Vector2 movement = _inputReader.Movement;
			_playerMover.Move(movement);

			if (movement.x < 0 || movement.x > 0)
			{
				_playerAnimator.IsMoving = true;
			}
			else
			{
				_playerAnimator.IsMoving = false;
			}

			LookRight(movement);
			bool shoot = _inputReader.ShootBubble;
			_shootBubble.Shoot(shoot, _lookRight);
		}

		#region Private Implementation
		private void LookRight(Vector2 movement)
		{
			if (movement.x > 0)
			{
				_lookRight = true;
			}
			else if (movement.x < 0)
			{
				_lookRight = false;
			}

			_spriteRenderer.flipX = !_lookRight;
		}

		private void Collect(Item item)
		{
			if (_inventory.Add(item.ItemData, 1))
			{
				if (_inventory != null)
				{
					// TODO: Update inventory UI
				}
				item.Collect();
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			Item item = other.GetComponent<Item>();
			if (item != null)
			{
				Collect(item);
			}
		}
		#endregion
	}
}
