using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace BubbleBobble
{
	/// <summary>
	/// Script for controlling various player actions.
	/// Keeps track of inputs and relays informations to other classes.
	/// </summary>
	///
	/// <remarks>
	/// author: Hanna Repo
	/// </remarks>

	[RequireComponent(typeof(InputReader))]
	public class PlayerControl : MonoBehaviour
	{
		private PlayerAnimationController _playerAnimator;
		[SerializeField] private float _fireRate = 1f;
		[SerializeField] private float _fireRateWithBoost = 0.5f;
		[SerializeField] private SpriteRenderer _playerBubbleRenderer;
		[SerializeField] private GameManager _gameManager;
		[SerializeField] private ItemData[] _shells;
		private InputReader _inputReader;
		private Inventory _inventory;
		private PlayerMover _playerMover;
		private ShootBubble _shootBubble;
		private Health _health;
		private SpriteRenderer _spriteRenderer;
		private bool _lookRight = true;
		private Rigidbody2D _rigidBody;
		private Collider2D _playerCollider;
		private float _originalFireRate = 0f;
		private float _timer = 0;
		private bool _canMove = true;
		private bool _shoot = false;
		private bool _canShoot = true;
		private bool _fireRateBoostIsActive = false;

		public bool CanMove
		{
			get { return _canMove; }
			set { _canMove = value; }
		}

		public bool LookingRight
		{
			get { return _lookRight; }
			set { _lookRight = value; }
		}

		public Inventory Inventory => _inventory;

		public bool Shoot => _shoot;

		public bool FireRateBoostIsActive
		{
			get { return _fireRateBoostIsActive; }
			set { _fireRateBoostIsActive = value; }
		}


		#region Unity Messages
		private void Awake()
		{
			_inputReader = GetComponent<InputReader>();
			_inventory = new Inventory();
			_playerMover = GetComponent<PlayerMover>();
			_shootBubble = GetComponent<ShootBubble>();
			_health = GetComponent<Health>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_playerAnimator = GetComponent<PlayerAnimationController>();
			_playerBubbleRenderer.GetComponent<SpriteRenderer>();
			_rigidBody = GetComponent<Rigidbody2D>();
			_playerCollider = GetComponent<Collider2D>();
		}

		private void Start()
		{
			_originalFireRate = _fireRate;
		}

		private void Update()
		{
			Vector2 movement = _inputReader.Movement;
			if (CanMove)
			{
				_playerMover.Move(movement);
			}
			else
			{
				movement = Vector2.zero;
			}

			if (movement.x < 0 || movement.x > 0)
			{
				_playerAnimator.IsMoving = true;
			}
			else
			{
				_playerAnimator.IsMoving = false;
			}

			LookRight(movement);

			_timer += Time.deltaTime;
			_shoot = _inputReader.ShootBubble;

			if (_fireRateBoostIsActive)
			{
				_fireRate = _fireRateWithBoost;
			}
			else
			{
				_fireRate = _originalFireRate;
			}

			if (_timer >= _fireRate && _shoot && _canShoot)
			{
				_shootBubble.Shoot(_shoot, _lookRight);
				_timer = 0;
			}
		}
		#endregion

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
				item.Collect();
				_gameManager.HandleItemPickup(item.ItemData.Points);
			}
			CheckInventory();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			Item item = other.GetComponent<Item>();
			if (item != null)
			{
				if (item.ItemData.ItemType == ItemType.Umbrella)
				{
					other.GetComponent<Umbrella>().SkipLevels();
				}
				Collect(item);
			}
		}

		/// <summary>
		/// Check whether all shells are in player's inventory. If they are, add an extra life to player.
		/// </summary>
		private void CheckInventory()
		{
			if (_health.CurrentLives > 0 && _health.CurrentLives < _health.MaxLives &&
				_inventory.CheckInventoryContent(_shells[0], 1) &&
				_inventory.CheckInventoryContent(_shells[1], 1) &&
				_inventory.CheckInventoryContent(_shells[2], 1) &&
				_inventory.CheckInventoryContent(_shells[3], 1))
			{
				_health.SetExtraLife(true);
				_inventory.Remove(_shells[0], 1);
				_inventory.Remove(_shells[1], 1);
				_inventory.Remove(_shells[2], 1);
				_inventory.Remove(_shells[3], 1);
			}
		}

		/// <summary>
		/// Restricts player movement, disables the collider and enables the bubble sprite.
		/// </summary>
		public void RestrainPlayer(bool toggleBubble)
		{
			_lookRight = true;
			_rigidBody.bodyType = RigidbodyType2D.Static;
			_playerCollider.enabled = false;
			_canMove = false;
			_canShoot = false;
			if (toggleBubble)
			{
				_playerBubbleRenderer.enabled = true;
			}
		}

		/// <summary>
		/// Unrestricts player movement, enables the collider and disables the bubble sprite.
		/// </summary>
		public void UnRestrainPlayer()
		{
			_rigidBody.bodyType = RigidbodyType2D.Dynamic;
			_playerCollider.enabled = true;
			_canMove = true;
			_canShoot = true;
			_playerBubbleRenderer.enabled = false;
		}

		#endregion
	}
}
