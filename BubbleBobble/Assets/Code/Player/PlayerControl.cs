using UnityEditor.Rendering;
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
		private InputReader _inputReader;
		public Inventory _inventory;
		private PlayerMover _playerMover;
		private ShootBubble _shootBubble;
		private SpriteRenderer _spriteRenderer;
		private bool _lookRight = true;
		private PlayerAnimationController _playerAnimator;
		[SerializeField] private float _fireRate = 1f;
		[SerializeField] private float _fireRateWithBoost = 0.5f;
		[SerializeField] private SpriteRenderer _playerBubbleRenderer;
		private Rigidbody2D _rigidBody;
		private Collider2D _playerCollider;
		private float _originalFireRate = 0f;
		private float _timer = 0;
		private bool _canMove = true;
		public bool CanMove 
		{ 
			get { return _canMove; } 
			set { _canMove = value; } 
		}
		private bool _fireRateBoostIsActive = false;

		public bool LookingRight 
		{ 
			get { return _lookRight; } 
			set { _lookRight = value; }
		}

		public bool FireRateBoostIsActive
		{
			get { return _fireRateBoostIsActive; }
			set { _fireRateBoostIsActive = value; }
		}

		
		private void Awake()
		{
			_inputReader = GetComponent<InputReader>();
			_inventory = new Inventory();
			_playerMover = GetComponent<PlayerMover>();
			_shootBubble = GetComponent<ShootBubble>();
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
			bool shoot = _inputReader.ShootBubble;

			if (_fireRateBoostIsActive)
			{
				_fireRate = _fireRateWithBoost;
			}
			else
			{
				_fireRate = _originalFireRate;
			}

			if (_timer >= _fireRate && shoot)
			{
				_shootBubble.Shoot(shoot, _lookRight);
				_timer = 0;
			}
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
			if (item.ItemData.ItemType == ItemType.Shell)
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
			else
			{
				item.Collect();
				// TODO: Add points
			}
		}

		public bool CheckInventoryContent(ItemData item)
		{
			if (_inventory.ContainsKey(item))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public void RemoveFromInventory(ItemData item)
		{
			if (_inventory.Remove(item, 1))
			{
				if (_inventory != null)
				{
					// TODO: Update inventory UI
				}
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

		/// <summary>
		/// Restricts player movement, disables the collider and enables the bubble sprite.
		/// </summary>
		public void RestrainPlayer()
		{
			_lookRight = true;
			_rigidBody.bodyType = RigidbodyType2D.Static;
			_playerCollider.enabled = false;
			_canMove = false;
			_playerBubbleRenderer.enabled = true;
		}

		/// <summary>
		/// Unrestricts player movement, enables the collider and disables the bubble sprite.
		/// </summary>
		public void UnRestrainPlayer()
		{
			_rigidBody.bodyType = RigidbodyType2D.Dynamic;
			_playerCollider.enabled = true;
			_canMove = true;
			_playerBubbleRenderer.enabled = false;
		}

		#endregion
	}
}
